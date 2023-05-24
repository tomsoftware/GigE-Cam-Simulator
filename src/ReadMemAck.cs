using System;
using System.Collections;
using System.Security.Cryptography;

namespace GigE_Cam_Simulator
{

    /// <summary>
    /// https://www.visiononline.org/userAssets/aiaUploads/File/GigE_Vision_Specification_2-0-03.pdf
    /// </summary>
    public class ReadMemAck : GvcpAck
    {
        byte[] resultData;
        uint address;

        private static string ByteToString(byte[] data)
        {
            return System.Text.Encoding.ASCII.GetString(data);
        }

        public ReadMemAck(uint req_id, RegisterMemory registers, BufferReader message) :
            base(req_id, ArvGvcpPacketType.ARV_GVCP_PACKET_TYPE_ACK, ArvGvcpCommand.ARV_GVCP_COMMAND_READ_MEMORY_ACK)
        {
            var address = (int)message.ReadIntBE();
            var reserved = (int)message.ReadWordBE();
            var count = (int)message.ReadWordBE();

            this.resultData = registers.GetBytes(address, count);
            this.address = (uint)address;

            Console.WriteLine("    read: " + address.ToString("x") + " --> " + registers.FindRegisterTypeByAddress(address) + " = " + ByteToString(this.resultData));

       
        }

        public BufferReader ToBuffer()
        {
            var b = base.CreateBuffer(4 + resultData.Length);

            b.WriteIntBE(this.address);
            b.WriteBytes(resultData, resultData.Length);
            
            return b;

        }
    }
}
