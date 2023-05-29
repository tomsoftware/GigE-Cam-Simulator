﻿namespace GigE_Cam_Simulator
{
    public enum RegisterTypes
    {
        Unknown = 0,

        Version,
        Device_Mode,
        Device_MAC_address_High_Network_interface_0,
        Device_MAC_address_Low_Network_interface_0,
        Supported_IP_configuration_Network_interface_0,
        Current_IP_configuration_procedure_Network_interface_0,
        Current_IP_address_Network_interface_0,
        Current_subnet_mask_Network_interface_0,
        Current_default_Gateway_Network_interface_0,
        Manufacturer_name,
        Model_name,
        Device_version,
        Manufacturer_specific_information,
        Serial_number,
        User_defined_name,
        XML_Device_Description_File_First_URL,
        XML_Device_Description_File_Second_URL,
        Number_of_network_interfaces,
        Persistent_IP_address_Network_interface_0,
        Persistent_subnet_mask_Network_interface_0,
        Persistent_default_gateway_Network_interface_0,
        Link_Speed_Network_interface_0,
        MAC_address_High_Network_interface_1,
        MAC_address_Low_Network_interface_1,
        Supported_IP_configuration_Network_interface_1,
        Current_IP_configuration_procedure_Network_interface_1,
        Current_IP_address_Network_interface_1,
        Current_subnet_mask_Network_interface_1,
        Current_default_gateway_Network_interface_1,
        Persistent_IP_address_Network_interface_1,
        Persistent_subnet_mask_Network_interface_1,
        Persistent_default_gateway_Network_interface_1,
        Link_Speed_Network_interface_1,
        MAC_address_High_Network_interface_2,
        MAC_address_Low_Network_interface_2,
        Supported_IP_configuration_Network_interface_2,
        Current_IP_configuration_procedure_Network_interface_2,
        Current_IP_address_Network_interface_2,
        Current_subnet_mask_Network_interface_2,
        Current_default_gateway_Network_interface_2,
        Persistent_IP_address_Network_interface_2,
        Persistent_subnet_mask_Network_interface_2,
        Persistent_default_gateway_Network_interface_2,
        Link_Speed_Network_interface_2,
        MAC_address_High_Network_interface_3,
        MAC_address_Low_Network_interface_3,
        Supported_IP_configuration_Network_interface_3,
        Current_IP_configuration_procedure_Network_interface_3,
        Current_IP_address_Network_interface_3,
        Current_subnet_mask_Network_interface_3,
        Current_default_gateway_Network_interface_3,
        Persistent_IP_address_Network_interface_3,
        Persistent_subnet_mask_Network_interface_3,
        Persistent_default_gateway_Network_interface_3,
        Link_Speed_Network_interface_3,
        Number_of_Message_channels,
        /// <summary>
        /// This register reports the number of stream channels supported by this device.
        /// </summary>
        Number_of_Stream_channels,
        Number_of_Action_Signals,
        Action_Device_Key,
        Stream_channels_Capability,
        Message_channel_Capability,
        GVCP_Capability,
        Heartbeat_timeout,
        Timestamp_tick_frequency_High,
        Timestamp_tick_frequency_Low,
        Timestamp_control,
        Timestamp_value_latched_High,
        Timestamp_value_latched_Low,
        Discovery_ACK_delay,
        GVCP_Configuration,
        Pending_Timeout,
        Control_Switchover_Key,
        Control_Channel_Privilege,
        Primary_Application_Port,
        /// <summary>
        /// This optional register provides IP address information about the primary application holding the control channel privilege.
        /// </summary>
        Primary_Application_IP_address,
        MCP,
        MCDA,
        MCTT,
        MCRC,
        MCSP,
        Stream_Channel_Port_0,
        Stream_Channel_Packet_Size_0,
        SCPD0,
        Stream_Channel_Destination_Address_0,
        SCSP0,
        SCC0,
        SCCFG0,
        SCP1,
        SCPS1,
        SCPD1,
        SCDA1,
        SCSP1,
        SCC1,
        SCCFG1,
        SCP511,
        SCPS511,
        SCPD511,
        SCDA511,
        SCSP511,
        SCC511,
        SCCFG511,
        Manifest_Table,
        ACTION_GROUP_KEY0,
        ACTION_GROUP_MASK0,
        ACTION_GROUP_KEY1,
        ACTION_GROUP_MASK1,
        ACTION_GROUP_KEY127,
        ACTION_GROUP_MASK127,
    }
}
