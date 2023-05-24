namespace GigE_Cam_Simulator
{
    using Microsoft.Win32;
    using System;
    using System.Text;
    using static System.Collections.Specialized.BitVector32;

    public class Register
    {
        public RegisterTypes Type { get; }
        public int Address { get; }

        public int Length { get; }

        public Action<RegisterMemory> Action { get; set; } = null;

        public Register(RegisterTypes type, int address, int length)
        {
            this.Type = type;
            this.Address = address;
            this.Length = length;
        }

        public string TypeName => Type.ToString();
    }

    public class RegisterMemory
    {
        public static Register BadRegister = new Register(RegisterTypes.Unknown, 0, 0);

        public Register[] Registers { get; }

        public Dictionary<int, RegisterTypes> RegisterTypesLookup = new Dictionary<int, RegisterTypes>();

        private BufferReader data;

        public byte GetByte(RegisterTypes register, int index)
        {
            var address = Registers[(int)register].Address + index;
            return data.GetByte(address);
        }

        public void SetByte(RegisterTypes register, int index, byte value)
        {
            var address = Registers[(int)register].Address + index;
            data.SetByte(address, value);
        }

        public void WriteBytes(int address, byte[] values)
        {
            data.SetBytes(address, values, values.Length);
        }

        public void WriteIntBE(int address, int value)
        { 
            data.SetIntBE(address, value);
        }

        public uint ReadIntBE(int address)
        {
            return data.GetIntBE(address);
        }

        public uint ReadIntBE(RegisterTypes register)
        {
            var reg = Registers[(int)register];
            return this.ReadIntBE(reg.Address);
        }


        public byte[] GetBytes(int address, int lenght)
        {
            return this.data.GetBytes(address, lenght);
        }

        public byte[] GetBytes(RegisterTypes register)
        {
            var reg = Registers[(int)register];
            return this.data.GetBytes(reg.Address, reg.Length);
        }


        public void SetBytes(RegisterTypes register, byte[] values)
        {
            var reg = Registers[(int)register];
            var address = reg.Address;

            var l = Math.Min(values.Length, reg.Length);

            // fill in data
            this.data.SetBytes(address, values, l);
            
            // clear buffer
            this.data.SetNull(address + l, l - reg.Length);
           
            address += reg.Length;
        }

        public void WriteBit(int address, int index, bool value)
        {
            this.data.SetBit(address, index, value);
        }

        public void SetBit(RegisterTypes register, int index, bool value)
        {
            var address = Registers[(int)register].Address;
            this.data.SetBit(address, index, value);
        }

        public void SetValue(RegisterTypes register, int value)
        {
            var reg = Registers[(int)register];
            this.WriteIntBE(reg.Address, value);
        }

        public uint GetValue(RegisterTypes register)
        {
            var reg = Registers[(int)register];
            return this.ReadIntBE(reg.Address);
        }

        public void SetString(RegisterTypes register, string value)
        {
            SetBytes(register, ASCIIEncoding.ASCII.GetBytes(value));
        }

        public string GetString(RegisterTypes register)
        {
            var reg = Registers[(int)register];
            return this.data.GetString(reg.Address, reg.Length);
        }

        public void Register(RegisterTypes type, int address, int lenght)
        {
            this.Registers[(int)type] = new Register(type, address, lenght);

            RegisterTypesLookup.Add(address, type);
        }

        public RegisterTypes FindRegisterTypeByAddress(int registerAddress)
        {
            if ( RegisterTypesLookup.TryGetValue(registerAddress, out RegisterTypes registerType))
            {
                return registerType;
            }

            return RegisterTypes.Unknown;
        }

        public void RegisterWriteCallback(RegisterTypes register, Action<RegisterMemory> action)
        {
            Registers[(int)register].Action = action;
        }

        internal void TriggerWriteCallback(RegisterTypes register)
        {
            var reg = Registers[(int)register];
            if (reg == null)
            {
                return;
            }

            var action = reg.Action;

            if (action == null)
            {
                return;
            }

            action(this);
        }

        public RegisterMemory(int size)
        {
            this.data = new BufferReader(size);

            this.Registers = new Register[(int)RegisterTypes.LAST_ITEM];

            this.Register(RegisterTypes.Version, 0x0000, 4);
            this.Register(RegisterTypes.Device_Mode, 0x0004, 4);
            this.Register(RegisterTypes.Device_MAC_address_High_Network_interface_0, 0x0008, 4);
            this.Register(RegisterTypes.Device_MAC_address_Low_Network_interface_0, 0x000C, 4);
            this.Register(RegisterTypes.Supported_IP_configuration_Network_interface_0, 0x0010, 4);
            this.Register(RegisterTypes.Current_IP_configuration_procedure_Network_interface_0, 0x0014, 4);
            this.Register(RegisterTypes.Current_IP_address_Network_interface_0, 0x0024, 4);
            this.Register(RegisterTypes.Current_subnet_mask_Network_interface_0, 0x0034, 4);
            this.Register(RegisterTypes.Current_default_Gateway_Network_interface_0, 0x0044, 4);
            this.Register(RegisterTypes.Manufacturer_name, 0x0048, 32);
            this.Register(RegisterTypes.Model_name, 0x0068, 32);
            this.Register(RegisterTypes.Device_version, 0x0088, 32);
            this.Register(RegisterTypes.Manufacturer_specific_information, 0x00A8, 48);
            this.Register(RegisterTypes.Serial_number, 0x00D8, 16);
            this.Register(RegisterTypes.User_defined_name, 0x00E8, 16);
            this.Register(RegisterTypes.First_choice_of_URL_for_XML_device_description_file, 0x0200, 512);
            this.Register(RegisterTypes.Second_choice_of_URL_for_XML_device_description_file, 0x0400, 512);
            this.Register(RegisterTypes.Number_of_network_interfaces, 0x0600, 4);
            this.Register(RegisterTypes.Persistent_IP_address_Network_interface_0, 0x064C, 4);
            this.Register(RegisterTypes.Persistent_subnet_mask_Network_interface_0, 0x065C, 4);
            this.Register(RegisterTypes.Persistent_default_gateway_Network_interface_0, 0x066C, 4);
            this.Register(RegisterTypes.Link_Speed_Network_interface_0, 0x0670, 4);
            this.Register(RegisterTypes.MAC_address_High_Network_interface_1, 0x0680, 4);
            this.Register(RegisterTypes.MAC_address_Low_Network_interface_1, 0x0684, 4);
            this.Register(RegisterTypes.Supported_IP_configuration_Network_interface_1, 0x0688, 4);
            this.Register(RegisterTypes.Current_IP_configuration_procedure_Network_interface_1, 0x068C, 4);
            this.Register(RegisterTypes.Current_IP_address_Network_interface_1, 0x069C, 4);
            this.Register(RegisterTypes.Current_subnet_mask_Network_interface_1, 0x06AC, 4);
            this.Register(RegisterTypes.Current_default_gateway_Network_interface_1, 0x06BC, 4);
            this.Register(RegisterTypes.Persistent_IP_address_Network_interface_1, 0x06CC, 4);
            this.Register(RegisterTypes.Persistent_subnet_mask_Network_interface_1, 0x06DC, 4);
            this.Register(RegisterTypes.Persistent_default_gateway_Network_interface_1, 0x06EC, 4);
            this.Register(RegisterTypes.Link_Speed_Network_interface_1, 0x06F0, 4);
            this.Register(RegisterTypes.MAC_address_High_Network_interface_2, 0x0700, 4);
            this.Register(RegisterTypes.MAC_address_Low_Network_interface_2, 0x0704, 4);
            this.Register(RegisterTypes.Supported_IP_configuration_Network_interface_2, 0x0708, 4);
            this.Register(RegisterTypes.Current_IP_configuration_procedure_Network_interface_2, 0x070C, 4);
            this.Register(RegisterTypes.Current_IP_address_Network_interface_2, 0x071C, 4);
            this.Register(RegisterTypes.Current_subnet_mask_Network_interface_2, 0x072C, 4);
            this.Register(RegisterTypes.Current_default_gateway_Network_interface_2, 0x073C, 4);
            this.Register(RegisterTypes.Persistent_IP_address_Network_interface_2, 0x074C, 4);
            this.Register(RegisterTypes.Persistent_subnet_mask_Network_interface_2, 0x075C, 4);
            this.Register(RegisterTypes.Persistent_default_gateway_Network_interface_2, 0x076C, 4);
            this.Register(RegisterTypes.Link_Speed_Network_interface_2, 0x0770, 4);
            this.Register(RegisterTypes.MAC_address_High_Network_interface_3, 0x0780, 4);
            this.Register(RegisterTypes.MAC_address_Low_Network_interface_3, 0x0784, 4);
            this.Register(RegisterTypes.Supported_IP_configuration_Network_interface_3, 0x0788, 4);
            this.Register(RegisterTypes.Current_IP_configuration_procedure_Network_interface_3, 0x078C, 4);
            this.Register(RegisterTypes.Current_IP_address_Network_interface_3, 0x079C, 4);
            this.Register(RegisterTypes.Current_subnet_mask_Network_interface_3, 0x07AC, 4);
            this.Register(RegisterTypes.Current_default_gateway_Network_interface_3, 0x07BC, 4);
            this.Register(RegisterTypes.Persistent_IP_address_Network_interface_3, 0x07CC, 4);
            this.Register(RegisterTypes.Persistent_subnet_mask_Network_interface_3, 0x07DC, 4);
            this.Register(RegisterTypes.Persistent_default_gateway_Network_interface_3, 0x07EC, 4);
            this.Register(RegisterTypes.Link_Speed_Network_interface_3, 0x07F0, 4);
            this.Register(RegisterTypes.Number_of_Message_channels, 0x0900, 4);
            this.Register(RegisterTypes.Number_of_Stream_channels, 0x0904, 4);
            this.Register(RegisterTypes.Number_of_Action_Signals, 0x0908, 4);
            this.Register(RegisterTypes.Action_Device_Key, 0x090C, 4);
            this.Register(RegisterTypes.Stream_channels_Capability, 0x092C, 4);
            this.Register(RegisterTypes.Message_channel_Capability, 0x0930, 4);
            this.Register(RegisterTypes.GVCP_Capability, 0x0934, 4);
            this.Register(RegisterTypes.Heartbeat_timeout, 0x0938, 4);
            this.Register(RegisterTypes.Timestamp_tick_frequency_High, 0x093C, 4);
            this.Register(RegisterTypes.Timestamp_tick_frequency_Low, 0x0940, 4);
            this.Register(RegisterTypes.Timestamp_control, 0x0944, 4);
            this.Register(RegisterTypes.Timestamp_value_latched_High,0x0948,4);
            this.Register(RegisterTypes.Timestamp_value_latched_Low,0x094C,4);
            this.Register(RegisterTypes.Discovery_ACK_delay, 0x0950, 4);
            this.Register(RegisterTypes.GVCP_Configuration, 0x0954, 4);
            this.Register(RegisterTypes.Pending_Timeout, 0x0958, 4);
            this.Register(RegisterTypes.Control_Switchover_Key, 0x095C, 4);
            this.Register(RegisterTypes.CCP_Control_Channel_Privilege, 0x0A00, 4);
            this.Register(RegisterTypes.Primary_Application_Port, 0x0A04, 4);
            this.Register(RegisterTypes.Primary_Application_IP_address, 0x0A14, 4);
            this.Register(RegisterTypes.MCP, 0x0B00, 4);
            this.Register(RegisterTypes.MCDA, 0x0B10, 4);
            this.Register(RegisterTypes.MCTT, 0x0B14, 4);
            this.Register(RegisterTypes.MCRC, 0x0B18, 4);
            this.Register(RegisterTypes.MCSP, 0x0B1C, 4);
            this.Register(RegisterTypes.SCP0, 0x0D00, 4);
            this.Register(RegisterTypes.SCPS0, 0x0D04, 4);
            this.Register(RegisterTypes.SCPD0, 0x0D08, 4);
            this.Register(RegisterTypes.SCDA0, 0x0D18, 4);
            this.Register(RegisterTypes.SCSP0, 0x0D1C, 4);
            this.Register(RegisterTypes.SCC0, 0x0D20, 4);
            this.Register(RegisterTypes.SCCFG0, 0x0D24, 4);
            this.Register(RegisterTypes.SCP1, 0x0D40, 4);
            this.Register(RegisterTypes.SCPS1, 0x0D44, 4);
            this.Register(RegisterTypes.SCPD1, 0x0D48, 4);
            this.Register(RegisterTypes.SCDA1, 0x0D58, 4);
            this.Register(RegisterTypes.SCSP1, 0x0D5C, 4);
            this.Register(RegisterTypes.SCC1, 0x0D60, 4);
            this.Register(RegisterTypes.SCCFG1, 0x0D64, 4);
            this.Register(RegisterTypes.SCP511, 0x8CC0, 4);
            this.Register(RegisterTypes.SCPS511, 0x8CC4, 4);
            this.Register(RegisterTypes.SCPD511, 0x8CC8, 4);
            this.Register(RegisterTypes.SCDA511, 0x8CD8, 4);
            this.Register(RegisterTypes.SCSP511, 0x8CDC, 4);
            this.Register(RegisterTypes.SCC511, 0x8CE0, 4);
            this.Register(RegisterTypes.SCCFG511, 0x8CE4, 4);
            this.Register(RegisterTypes.Manifest_Table, 0x9000, 512);
            this.Register(RegisterTypes.ACTION_GROUP_KEY0, 0x9800, 4);
            this.Register(RegisterTypes.ACTION_GROUP_MASK0, 0x9804, 4);
            this.Register(RegisterTypes.ACTION_GROUP_KEY1, 0x9810, 4);
            this.Register(RegisterTypes.ACTION_GROUP_MASK1, 0x9814, 4);
            this.Register(RegisterTypes.ACTION_GROUP_KEY127, 0x9FF0, 4);
            this.Register(RegisterTypes.ACTION_GROUP_MASK127, 0x9FF4, 4);

        }
    }
}
