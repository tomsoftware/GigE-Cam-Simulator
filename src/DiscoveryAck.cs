namespace GigE_Cam_Simulator
{
    /// <summary>
    /// https://aravisproject.github.io/docs/aravis-0.4/aravis-gvcp.html
    /// </summary>
    public class DiscoveryAck : GvcpAck
    {
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public uint DeviceMode { get; set; }

        public byte[] mac_address_high { get; set; }
        public byte[] mac_address_low { get; set; }

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
            base(req_id, ArvGvcpPacketType.ARV_GVCP_PACKET_TYPE_ACK, ArvGvcpCommand.ARV_GVCP_COMMAND_DISCOVERY_ACK)
        {
            this.VersionMajor = 3;
            this.VersionMinor = 1;

            this.DeviceMode = registers.ReadIntBE(RegisterTypes.Device_Mode);

            this.mac_address_high = registers.GetBytes(RegisterTypes.Device_MAC_address_High_Network_interface_0);
            this.mac_address_low = registers.GetBytes(RegisterTypes.Device_MAC_address_Low_Network_interface_0);


            this.ip_config_options = 0;
            this.ip_config_current = 0;


            this.current_ip_buf = registers.GetBytes(RegisterTypes.Current_IP_address_Network_interface_0);


            this.current_subnet_mask_buf = registers.GetBytes(RegisterTypes.Current_subnet_mask_Network_interface_0);

            this.manufacturer_name = registers.GetString(RegisterTypes.Manufacturer_name);
            this.model_name = registers.GetString(RegisterTypes.Model_name);
            this.device_version = registers.GetString(RegisterTypes.Device_version);
            this.manufacturer_info = registers.GetString(RegisterTypes.Manufacturer_specific_information);
            this.serial_number = registers.GetString(RegisterTypes.Serial_number);
            this.user_defined_name = registers.GetString(RegisterTypes.User_defined_name);

        }

        public BufferReader ToBuffer()
        {
            var b = base.CreateBuffer(0x00f8);

            b.WriteWordBE(this.VersionMajor);  // length: 2, type: 'int' },
            b.WriteWordBE(this.VersionMinor);  // length: 2, type: 'int' },
            b.WriteIntBE(this.DeviceMode);  // length: 4, type: 'int' },
            
            b.WriteBytes(this.mac_address_high, 4);  // length: 6, type: 'buffer' },
            b.WriteBytes(this.mac_address_low, 4);  // length: 6, type: 'buffer' },

            b.WriteIntBE(this.ip_config_options);  // length: 4, type: 'int' },
            b.WriteIntBE(this.ip_config_current);  // length: 4, type: 'int' },

            b.WriteNull(12);

            b.WriteBytes(this.current_ip_buf, 4);  // length: 4, type: 'buffer' },


            b.WriteNull(12);

            b.WriteBytes(this.current_subnet_mask_buf, 4);  // length: 4, type: 'buffer' },

            b.WriteNull(12);

            b.WriteBytes(this.current_gateway_buf, 4);  // length: 4, type: 'buffer' },


            b.WriteString(this.manufacturer_name, 32);  // length: 32, type: 'string' },
            b.WriteString(this.model_name, 32);  // length: 32, type: 'string' },
            b.WriteString(this.device_version, 32);  // length: 32, type: 'string' },
            b.WriteString(this.manufacturer_info, 48);  // length: 48, type: 'string' },
            b.WriteString(this.serial_number, 16);  // length: 16, type: 'string' },
            b.WriteString(this.user_defined_name,16);  // length: 16, type: 'string' }

            return b;

        }
    }
}
