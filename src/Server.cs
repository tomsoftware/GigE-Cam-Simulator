using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GigE_Cam_Simulator
{
    public enum PackageCommandType
    {
        DISCOVERY_CMD = 0x0002,
        DISCOVERY_ACK = 0x0003,
        FORCEIP_CMD = 0x0004,
        FORCEIP_ACK = 0x0005,
        //Streaming Protocol Control
        PACKETRESEND_CMD = 0x0040,
        //Device Memory Access
        READREG_CMD = 0x0080,
        READREG_ACK = 0x0081,
        WRITEREG_CMD = 0x0082,
        WRITEREG_ACK = 0x0083,
        READMEM_CMD = 0x0084,
        READMEM_ACK = 0x0085,
        WRITEMEM_CMD = 0x0086,
        WRITEMEM_ACK = 0x0087,
        PENDING_ACK = 0x0089,
    }

    internal class Server
    {
        private static readonly int CONTROL_PORT = 3956;

        string Address { get; set; }

        readonly RegisterMemory registers;
        readonly byte[] xml;
        private NetworkInterface iface;
        private UdpClient server;

        public Server(string address, string camXmlFileName, RegisterConfig preSetMemory)
        {
            this.Address = address;

            this.GetAllNic(address);

            this.FillTestImage();

            this.registers = new RegisterMemory(1024 * 1024 * 68); // 68MB Register

            this.xml = File.ReadAllBytes(camXmlFileName);

            this.InitRegisters(preSetMemory);
        }

        private void InitRegisters(RegisterConfig preSetMemory)
        {
            this.registers.SetIntBE(RegisterTypes.Version, 0x0100);

            this.registers.SetBit(RegisterTypes.Device_Mode, 0, true);
            this.registers.SetBit(RegisterTypes.Device_Mode, 1, true);
            this.registers.SetByte(RegisterTypes.Device_Mode, 0, 1);
            this.registers.SetByte(RegisterTypes.Device_Mode, 2, 1);

            // set MAC
            var ipInfo = this.GetIpInfo();
            var macAddress = this.iface.GetPhysicalAddress().GetAddressBytes();
            for (var i = 0; i < 2; i++)
            {
                this.registers.SetByte(RegisterTypes.Device_MAC_address_High_Network_interface_0, i + 2, macAddress[i]);
            }
            for (var i = 2; i < 6; i++)
            {
                this.registers.SetByte(RegisterTypes.Device_MAC_address_Low_Network_interface_0, i - 2, macAddress[i]);
            }

            // set IP and network addresses
            this.registers.SetBytes(RegisterTypes.Current_IP_address_Network_interface_0, ipInfo.Address.GetAddressBytes());
            this.registers.SetBytes(RegisterTypes.Current_subnet_mask_Network_interface_0, ipInfo.IPv4Mask.GetAddressBytes());
            this.registers.SetIntBE(RegisterTypes.Number_of_network_interfaces, 1);
            this.registers.SetBytes(RegisterTypes.Primary_Application_IP_address, ipInfo.Address.GetAddressBytes()); //set IP

            foreach (var property in preSetMemory.Properties)
            {
                if (property.IsString)
                {
                    this.registers.SetString(property.Register, property.StringValue);
                }
                else if (property.IsBits)
                {
                    foreach(var bitIndex in property.Bits)
                    {
                        this.registers.SetBit(property.Register, bitIndex, true);
                    }
                }
                else if (property.IsInt)
                {
                    this.registers.SetIntBE(property.Register, property.IntValue);
                }
            }

            // xml manifest / description
            var manifestFileaddress = 0x1C400;
            this.registers.SetString(RegisterTypes.XML_Device_Description_File_First_URL, "Local:camera.xml;" + ToHexString(manifestFileaddress) + ";" + ToHexString(this.xml.Length));
            this.registers.WriteBytes(manifestFileaddress, this.xml);
        }


        private bool IsDirectAddress(IPEndPoint endpoint)
        {
            return endpoint.Address.Equals(((IPEndPoint)server.Client.LocalEndPoint).Address);
        }

        private void IncommingMessage(IAsyncResult res)
        {
            var server = (UdpClient)res.AsyncState;
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            var msg = new BufferReader(server.EndReceive(res, ref endpoint));

            var identifier = msg.ReadByte();
            if (identifier != 0x42)
            {
                server.BeginReceive(new AsyncCallback(this.IncommingMessage), server);
                return;
            }

            var flags = msg.ReadByte();
            var command = (PackageCommandType)msg.ReadWordBE();

            // check if Endpoint fits - we only response to requests directed to us
            if (command != PackageCommandType.DISCOVERY_CMD && IsDirectAddress(endpoint)) 
            {
                server.BeginReceive(new AsyncCallback(this.IncommingMessage), server);
                return;
            }

            var length = msg.ReadWordBE();
            var req_id = msg.ReadWordBE();
            var data = new BufferReader(msg.ReadBytes((int)length));

            BufferReader result = null;

            switch (command)
            {
                case PackageCommandType.DISCOVERY_CMD:
                    Console.WriteLine("DISCOVERY by: " + endpoint);
                    var discovery = new DiscoveryAck(req_id, this.registers);
                    result = discovery.ToBuffer();
                    break;
                case PackageCommandType.READREG_CMD:
                    Console.WriteLine("READREG by: " + endpoint);
                    var readReg = new ReadRegAck(req_id, this.registers, data);
                    result = readReg.ToBuffer();
                    break;
                case PackageCommandType.READMEM_CMD:
                    Console.WriteLine("READMEM by: " + endpoint);
                    var readMem = new ReadMemAck(req_id, this.registers, data);
                    result = readMem.ToBuffer();
                    break;
                case PackageCommandType.WRITEREG_CMD:
                    Console.WriteLine("WRITEREG by: " + endpoint);
                    var writeReg = new WriteRegAck(req_id, this.registers, data);
                    result = writeReg.ToBuffer();
                    break;
                    
                default:
                    Console.WriteLine("Unknown GigE Command: " + command);
                    break;
            }

            server.BeginReceive(new AsyncCallback(this.IncommingMessage), server);

            if (result != null)
            {
                server.Send(result.Buffer, endpoint);
            }
        }

        public void Run()
        {
            this.server = new UdpClient();
            this.server.Client.Bind(new IPEndPoint(IPAddress.Any, Server.CONTROL_PORT));
            this.server.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this.server.BeginReceive(new AsyncCallback(this.IncommingMessage), this.server);
        }


        private UnicastIPAddressInformation GetIpInfo()
        {
            foreach (UnicastIPAddressInformation ips in this.iface.GetIPProperties().UnicastAddresses)
            {
                if (ips.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ips;
                }
            }

            return null;
        }

        private string ToHexString(int num)
        {
            return num.ToString("X");
        }

        public Server(string camXmlFileName, RegisterConfig preSetMemory) :
            this("0.0.0.0", camXmlFileName, preSetMemory)
        {
        }

        private void FillTestImage()
        {
            /*
            var len = this.imageWidth * this.imageHeight;
            var colorShift = 255.0 / this.imageHeight;
            for (var i = 0; i < len; i++)
            {
                this.mat[i] = (i % 2 == 1) ? (byte)55 : (byte)(colorShift * Math.Floor((double)i / this.imageWidth));
            }
            */
        }

        private void GetAllNic(string address)
        {
            var ifaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (var iface in ifaces)
            {
                // 
                var ipProperties = iface.GetIPProperties();
                foreach (var ip in ipProperties.UnicastAddresses)
                {
                    if ((iface.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback) && (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
                    {
                        if (address == "0.0.0.0")
                        {
                            this.iface = iface;
                            return;
                        }
                        else
                        {
                            if (address == ip.Address.ToString())
                            {
                                this.iface = iface;
                                return;
                            }
                        }
                    }
                }
            }
        }


    }
}
