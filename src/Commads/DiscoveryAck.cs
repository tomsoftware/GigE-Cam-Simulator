namespace GigE_Cam_Simulator.Commads
{
    /// <summary>
    /// https://aravisproject.github.io/docs/aravis-0.4/aravis-gvcp.html
    /// </summary>
    public class DiscoveryAck : GvcpAck
    {
        public uint VersionMajor { get; set; }
        public uint VersionMinor { get; set; }
        public uint DeviceMode { get; set; }

        public byte[] MacAddressHigh { get; set; }
        public byte[] MacAddressLow { get; set; }

        public uint ip_config_options { get; set; }
        public uint ip_config_current { get; set; }


        public byte[] current_ip_buf { get; set; }


        public byte[] current_subnet_mask_buf { get; set; }


        public byte[] current_gateway_buf { get; set; }


        public string manufacturer_name { get; set; }
        public string model_name { get; set; }
        public string device_version { get; set; }
        public string manufacturer_info { get; set; }
        public string serial_number { get; set; }
        public string user_defined_name { get; set; }

        public DiscoveryAck(uint req_id, RegisterMemory registers) :
            base(req_id, GvcpPacketType.GVCP_PACKET_TYPE_ACK, ArvGvcpCommand.GVCP_COMMAND_DISCOVERY_ACK)
        {
            VersionMajor = 3;
            VersionMinor = 1;

            DeviceMode = registers.ReadIntBE(RegisterTypes.Device_Mode);

            MacAddressHigh = registers.ReadBytes(RegisterTypes.Device_MAC_address_High_Network_interface_0);
            MacAddressLow = registers.ReadBytes(RegisterTypes.Device_MAC_address_Low_Network_interface_0);


            ip_config_options = 0;
            ip_config_current = 0;


            current_ip_buf = registers.ReadBytes(RegisterTypes.Current_IP_address_Network_interface_0);


            current_subnet_mask_buf = registers.ReadBytes(RegisterTypes.Current_subnet_mask_Network_interface_0);

            manufacturer_name = registers.ReadString(RegisterTypes.Manufacturer_name);
            model_name = registers.ReadString(RegisterTypes.Model_name);
            device_version = registers.ReadString(RegisterTypes.Device_version);
            manufacturer_info = registers.ReadString(RegisterTypes.Manufacturer_specific_information);
            serial_number = registers.ReadString(RegisterTypes.Serial_number);
            user_defined_name = registers.ReadString(RegisterTypes.User_defined_name);
        }

        public BufferReader ToBuffer()
        {
            var b = CreateBuffer(0x00f8);

            b.WriteWordBE(VersionMajor);  // length: 2, type: 'int' },
            b.WriteWordBE(VersionMinor);  // length: 2, type: 'int' },
            b.WriteIntBE(DeviceMode);  // length: 4, type: 'int' },

            b.WriteBytes(MacAddressHigh, 4);  // length: 6, type: 'buffer' },
            b.WriteBytes(MacAddressLow, 4);  // length: 6, type: 'buffer' },

            b.WriteIntBE(ip_config_options);  // length: 4, type: 'int' },
            b.WriteIntBE(ip_config_current);  // length: 4, type: 'int' },

            b.WriteNull(3 * 4);

            b.WriteBytes(current_ip_buf, 4);  // length: 4, type: 'buffer' },


            b.WriteNull(3 * 4);

            b.WriteBytes(current_subnet_mask_buf, 4);  // length: 4, type: 'buffer' },

            b.WriteNull(3 * 4);

            b.WriteBytes(current_gateway_buf, 4);  // length: 4, type: 'buffer' },


            b.WriteString(manufacturer_name, 32);  // length: 32, type: 'string' },
            b.WriteString(model_name, 32);  // length: 32, type: 'string' },
            b.WriteString(device_version, 32);  // length: 32, type: 'string' },
            b.WriteString(manufacturer_info, 48);  // length: 48, type: 'string' },
            b.WriteString(serial_number, 16);  // length: 16, type: 'string' },
            b.WriteString(user_defined_name, 16);  // length: 16, type: 'string' }

            return b;

        }
    }
}
