namespace GigE_Cam_Simulator.Commads
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

    public enum GvcpPacketType
    {
        GVCP_PACKET_TYPE_ACK = 0x00,
        GVCP_PACKET_TYPE_CMD = 0x42,
        GVCP_PACKET_TYPE_ERROR = 0x80,
        GVCP_PACKET_TYPE_UNKNOWN_ERROR = 0x8f
    }

    public enum ArvGvcpCommand
    {
        GVCP_COMMAND_DISCOVERY_CMD = 0x0002,
        GVCP_COMMAND_DISCOVERY_ACK = 0x0003,
        GVCP_COMMAND_BYE_CMD = 0x0004,
        GVCP_COMMAND_BYE_ACK = 0x0005,
        GVCP_COMMAND_PACKET_RESEND_CMD = 0x0040,
        GVCP_COMMAND_PACKET_RESEND_ACK = 0x0041,
        GVCP_COMMAND_READ_REGISTER_CMD = 0x0080,
        GVCP_COMMAND_READ_REGISTER_ACK = 0x0081,
        GVCP_COMMAND_WRITE_REGISTER_CMD = 0x0082,
        GVCP_COMMAND_WRITE_REGISTER_ACK = 0x0083,
        GVCP_COMMAND_READ_MEMORY_CMD = 0x0084,
        GVCP_COMMAND_READ_MEMORY_ACK = 0x0085,
        GVCP_COMMAND_WRITE_MEMORY_CMD = 0x0086,
        GVCP_COMMAND_WRITE_MEMORY_ACK = 0x0087,
        GVCP_COMMAND_PENDING_ACK = 0x0089
    }

    /// <summary>
    /// GigE Vision Command Package Header
    /// see: https://aravisproject.github.io/docs/aravis-0.4/aravis-gvcp.html
    /// </summary>
    public class GvcpAck
    {
        public GvcpPacketType Type { get; }
        public ArvGvcpCommand Command { get; }
        public uint Length { get; private set; }
        public uint PacketId { get; }


        public GvcpAck(uint packetId, GvcpPacketType type, ArvGvcpCommand command)
        {
            PacketId = packetId;
            Command = command;
            Type = type;
        }

        public BufferReader CreateBuffer(int length)
        {
            var b = new BufferReader(length + 8);

            this.Length = (uint)length;

            b.WriteWordBE((uint)Type);
            b.WriteWordBE((uint)Command);
            b.WriteWordBE((uint)length);
            b.WriteWordBE(this.PacketId);

            return b;

        }
    }
}
