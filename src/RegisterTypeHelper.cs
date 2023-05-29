
using System;

namespace GigE_Cam_Simulator
{
    public class RegisterInfo
    {
        public RegisterTypes Type { get; }
        public int Address { get; }

        public int Length { get; }

        public RegisterInfo(RegisterTypes type, int address, int length)
        {
            this.Type = type;
            this.Address = address;
            this.Length = length;
        }

        public string TypeName => Type.ToString();
    }


    public static class RegisterTypeHelper
    {
        public static RegisterInfo UnknownRegister = new RegisterInfo(RegisterTypes.Unknown, 0, 0);

        private static Dictionary<string, RegisterTypes> nameLookup = CreateNameLookup();

        private static RegisterInfo[] registers = CreateRegisterInfoList();
        private static Dictionary<int, RegisterInfo> addressLookup = CreateAddressLookup(registers);

        public static RegisterInfo RegisterByType(RegisterTypes type)
        {
            var index = (int)type;
            if ((index <= 0) || (index >= registers.Length))
            {
                return UnknownRegister;
            }

            return registers[index];
        }

        public static RegisterInfo RegisterByAddress(int registerAddress)
        {
            if (addressLookup.TryGetValue(registerAddress, out var info))
            {
                return info;
            }

            return UnknownRegister;
        }

        public static RegisterTypes RegisterTypeByName(string registerName)
        {
            if (registerName == null)
            {
                return RegisterTypes.Unknown;
            }

            if (nameLookup.TryGetValue(registerName, out var t))
            {
                return t;
            }
            return RegisterTypes.Unknown;

        }
        private static Dictionary<int, RegisterInfo> CreateAddressLookup(RegisterInfo[] registers)
        {
            var lookup = new Dictionary<int, RegisterInfo>();

            foreach (var info in registers)
            {
                if (info == null)
                {
                    continue;
                }
                lookup.Add(info.Address, info);
            }
            return lookup;
        }

        static Dictionary<string, RegisterTypes> CreateNameLookup()
        {
            var nameLookup = new Dictionary<string, RegisterTypes>(StringComparer.OrdinalIgnoreCase);
            var enumValues = Enum.GetValues(typeof(RegisterTypes));
            foreach (var enumValue in enumValues)
            {
                nameLookup.Add(enumValue.ToString(), (RegisterTypes)enumValue);
            }
            return nameLookup;
        }

        static RegisterInfo[] CreateRegisterInfoList()
        {
            var values = new List<RegisterInfo>
            {
                // https://www.visiononline.org/userAssets/aiaUploads/File/GigE_Vision_Specification_2-0-03.pdf
                // page: 324
                new RegisterInfo(RegisterTypes.Version, 0x0000, 4),
                new RegisterInfo(RegisterTypes.Device_Mode, 0x0004, 4),
                new RegisterInfo(RegisterTypes.Device_MAC_address_High_Network_interface_0, 0x0008, 4),
                new RegisterInfo(RegisterTypes.Device_MAC_address_Low_Network_interface_0, 0x000C, 4),
                new RegisterInfo(RegisterTypes.Supported_IP_configuration_Network_interface_0, 0x0010, 4),
                new RegisterInfo(RegisterTypes.Current_IP_configuration_procedure_Network_interface_0, 0x0014, 4),
                new RegisterInfo(RegisterTypes.Current_IP_address_Network_interface_0, 0x0024, 4),
                new RegisterInfo(RegisterTypes.Current_subnet_mask_Network_interface_0, 0x0034, 4),
                new RegisterInfo(RegisterTypes.Current_default_Gateway_Network_interface_0, 0x0044, 4),
                new RegisterInfo(RegisterTypes.Manufacturer_name, 0x0048, 32),
                new RegisterInfo(RegisterTypes.Model_name, 0x0068, 32),
                new RegisterInfo(RegisterTypes.Device_version, 0x0088, 32),
                new RegisterInfo(RegisterTypes.Manufacturer_specific_information, 0x00A8, 48),
                new RegisterInfo(RegisterTypes.Serial_number, 0x00D8, 16),
                new RegisterInfo(RegisterTypes.User_defined_name, 0x00E8, 16),
                new RegisterInfo(RegisterTypes.XML_Device_Description_File_First_URL, 0x0200, 512),
                new RegisterInfo(RegisterTypes.XML_Device_Description_File_Second_URL, 0x0400, 512),
                new RegisterInfo(RegisterTypes.Number_of_network_interfaces, 0x0600, 4),
                new RegisterInfo(RegisterTypes.Persistent_IP_address_Network_interface_0, 0x064C, 4),
                new RegisterInfo(RegisterTypes.Persistent_subnet_mask_Network_interface_0, 0x065C, 4),
                new RegisterInfo(RegisterTypes.Persistent_default_gateway_Network_interface_0, 0x066C, 4),
                new RegisterInfo(RegisterTypes.Link_Speed_Network_interface_0, 0x0670, 4),
                new RegisterInfo(RegisterTypes.MAC_address_High_Network_interface_1, 0x0680, 4),
                new RegisterInfo(RegisterTypes.MAC_address_Low_Network_interface_1, 0x0684, 4),
                new RegisterInfo(RegisterTypes.Supported_IP_configuration_Network_interface_1, 0x0688, 4),
                new RegisterInfo(RegisterTypes.Current_IP_configuration_procedure_Network_interface_1, 0x068C, 4),
                new RegisterInfo(RegisterTypes.Current_IP_address_Network_interface_1, 0x069C, 4),
                new RegisterInfo(RegisterTypes.Current_subnet_mask_Network_interface_1, 0x06AC, 4),
                new RegisterInfo(RegisterTypes.Current_default_gateway_Network_interface_1, 0x06BC, 4),
                new RegisterInfo(RegisterTypes.Persistent_IP_address_Network_interface_1, 0x06CC, 4),
                new RegisterInfo(RegisterTypes.Persistent_subnet_mask_Network_interface_1, 0x06DC, 4),
                new RegisterInfo(RegisterTypes.Persistent_default_gateway_Network_interface_1, 0x06EC, 4),
                new RegisterInfo(RegisterTypes.Link_Speed_Network_interface_1, 0x06F0, 4),
                new RegisterInfo(RegisterTypes.MAC_address_High_Network_interface_2, 0x0700, 4),
                new RegisterInfo(RegisterTypes.MAC_address_Low_Network_interface_2, 0x0704, 4),
                new RegisterInfo(RegisterTypes.Supported_IP_configuration_Network_interface_2, 0x0708, 4),
                new RegisterInfo(RegisterTypes.Current_IP_configuration_procedure_Network_interface_2, 0x070C, 4),
                new RegisterInfo(RegisterTypes.Current_IP_address_Network_interface_2, 0x071C, 4),
                new RegisterInfo(RegisterTypes.Current_subnet_mask_Network_interface_2, 0x072C, 4),
                new RegisterInfo(RegisterTypes.Current_default_gateway_Network_interface_2, 0x073C, 4),
                new RegisterInfo(RegisterTypes.Persistent_IP_address_Network_interface_2, 0x074C, 4),
                new RegisterInfo(RegisterTypes.Persistent_subnet_mask_Network_interface_2, 0x075C, 4),
                new RegisterInfo(RegisterTypes.Persistent_default_gateway_Network_interface_2, 0x076C, 4),
                new RegisterInfo(RegisterTypes.Link_Speed_Network_interface_2, 0x0770, 4),
                new RegisterInfo(RegisterTypes.MAC_address_High_Network_interface_3, 0x0780, 4),
                new RegisterInfo(RegisterTypes.MAC_address_Low_Network_interface_3, 0x0784, 4),
                new RegisterInfo(RegisterTypes.Supported_IP_configuration_Network_interface_3, 0x0788, 4),
                new RegisterInfo(RegisterTypes.Current_IP_configuration_procedure_Network_interface_3, 0x078C, 4),
                new RegisterInfo(RegisterTypes.Current_IP_address_Network_interface_3, 0x079C, 4),
                new RegisterInfo(RegisterTypes.Current_subnet_mask_Network_interface_3, 0x07AC, 4),
                new RegisterInfo(RegisterTypes.Current_default_gateway_Network_interface_3, 0x07BC, 4),
                new RegisterInfo(RegisterTypes.Persistent_IP_address_Network_interface_3, 0x07CC, 4),
                new RegisterInfo(RegisterTypes.Persistent_subnet_mask_Network_interface_3, 0x07DC, 4),
                new RegisterInfo(RegisterTypes.Persistent_default_gateway_Network_interface_3, 0x07EC, 4),
                new RegisterInfo(RegisterTypes.Link_Speed_Network_interface_3, 0x07F0, 4),
                new RegisterInfo(RegisterTypes.Number_of_Message_channels, 0x0900, 4),
                new RegisterInfo(RegisterTypes.Number_of_Stream_channels, 0x0904, 4),
                new RegisterInfo(RegisterTypes.Number_of_Action_Signals, 0x0908, 4),
                new RegisterInfo(RegisterTypes.Action_Device_Key, 0x090C, 4),
                new RegisterInfo(RegisterTypes.Stream_channels_Capability, 0x092C, 4),
                new RegisterInfo(RegisterTypes.Message_channel_Capability, 0x0930, 4),
                new RegisterInfo(RegisterTypes.GVCP_Capability, 0x0934, 4),
                new RegisterInfo(RegisterTypes.Heartbeat_timeout, 0x0938, 4),
                new RegisterInfo(RegisterTypes.Timestamp_tick_frequency_High, 0x093C, 4),
                new RegisterInfo(RegisterTypes.Timestamp_tick_frequency_Low, 0x0940, 4),
                new RegisterInfo(RegisterTypes.Timestamp_control, 0x0944, 4),
                new RegisterInfo(RegisterTypes.Timestamp_value_latched_High, 0x0948, 4),
                new RegisterInfo(RegisterTypes.Timestamp_value_latched_Low, 0x094C, 4),
                new RegisterInfo(RegisterTypes.Discovery_ACK_delay, 0x0950, 4),
                new RegisterInfo(RegisterTypes.GVCP_Configuration, 0x0954, 4),
                new RegisterInfo(RegisterTypes.Pending_Timeout, 0x0958, 4),
                new RegisterInfo(RegisterTypes.Control_Switchover_Key, 0x095C, 4),
                new RegisterInfo(RegisterTypes.Control_Channel_Privilege, 0x0A00, 4), // CCP
                new RegisterInfo(RegisterTypes.Primary_Application_Port, 0x0A04, 4),
                new RegisterInfo(RegisterTypes.Primary_Application_IP_address, 0x0A14, 4),
                new RegisterInfo(RegisterTypes.MCP, 0x0B00, 4),
                new RegisterInfo(RegisterTypes.MCDA, 0x0B10, 4),
                new RegisterInfo(RegisterTypes.MCTT, 0x0B14, 4),
                new RegisterInfo(RegisterTypes.MCRC, 0x0B18, 4),
                new RegisterInfo(RegisterTypes.MCSP, 0x0B1C, 4),
                new RegisterInfo(RegisterTypes.Stream_Channel_Port_0, 0x0D00, 4),
                new RegisterInfo(RegisterTypes.Stream_Channel_Packet_Size_0, 0x0D04, 4), // SCPS0
                new RegisterInfo(RegisterTypes.SCPD0, 0x0D08, 4),
                new RegisterInfo(RegisterTypes.Stream_Channel_Destination_Address_0, 0x0D18, 4), // SCDA0
                new RegisterInfo(RegisterTypes.SCSP0, 0x0D1C, 4),
                new RegisterInfo(RegisterTypes.SCC0, 0x0D20, 4),
                new RegisterInfo(RegisterTypes.SCCFG0, 0x0D24, 4),
                new RegisterInfo(RegisterTypes.SCP1, 0x0D40, 4),
                new RegisterInfo(RegisterTypes.SCPS1, 0x0D44, 4),
                new RegisterInfo(RegisterTypes.SCPD1, 0x0D48, 4),
                new RegisterInfo(RegisterTypes.SCDA1, 0x0D58, 4),
                new RegisterInfo(RegisterTypes.SCSP1, 0x0D5C, 4),
                new RegisterInfo(RegisterTypes.SCC1, 0x0D60, 4),
                new RegisterInfo(RegisterTypes.SCCFG1, 0x0D64, 4),
                new RegisterInfo(RegisterTypes.SCP511, 0x8CC0, 4),
                new RegisterInfo(RegisterTypes.SCPS511, 0x8CC4, 4),
                new RegisterInfo(RegisterTypes.SCPD511, 0x8CC8, 4),
                new RegisterInfo(RegisterTypes.SCDA511, 0x8CD8, 4),
                new RegisterInfo(RegisterTypes.SCSP511, 0x8CDC, 4),
                new RegisterInfo(RegisterTypes.SCC511, 0x8CE0, 4),
                new RegisterInfo(RegisterTypes.SCCFG511, 0x8CE4, 4),
                new RegisterInfo(RegisterTypes.Manifest_Table, 0x9000, 512),
                new RegisterInfo(RegisterTypes.ACTION_GROUP_KEY0, 0x9800, 4),
                new RegisterInfo(RegisterTypes.ACTION_GROUP_MASK0, 0x9804, 4),
                new RegisterInfo(RegisterTypes.ACTION_GROUP_KEY1, 0x9810, 4),
                new RegisterInfo(RegisterTypes.ACTION_GROUP_MASK1, 0x9814, 4),
                new RegisterInfo(RegisterTypes.ACTION_GROUP_KEY127, 0x9FF0, 4),
                new RegisterInfo(RegisterTypes.ACTION_GROUP_MASK127, 0x9FF4, 4),
            };

            var registerTypesCount = Enum.GetNames(typeof(RegisterTypes)).Length;
            var reg = new RegisterInfo[registerTypesCount];

            foreach (var v in values)
            {
                reg[(int)v.Type] = v;
            }

            return reg;
        }
    }

}
