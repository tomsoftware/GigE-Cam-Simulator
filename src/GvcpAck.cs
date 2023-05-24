
namespace GigE_Cam_Simulator
{
    public enum ArvGvcpPacketType
    {
        ARV_GVCP_PACKET_TYPE_ACK = 0x00,
        ARV_GVCP_PACKET_TYPE_CMD = 0x42,
        ARV_GVCP_PACKET_TYPE_ERROR = 0x80,
        ARV_GVCP_PACKET_TYPE_UNKNOWN_ERROR = 0x8f
    }

    public enum ArvGvcpCommand
    {
        ARV_GVCP_COMMAND_DISCOVERY_CMD = 0x0002,
        ARV_GVCP_COMMAND_DISCOVERY_ACK = 0x0003,
        ARV_GVCP_COMMAND_BYE_CMD = 0x0004,
        ARV_GVCP_COMMAND_BYE_ACK = 0x0005,
        ARV_GVCP_COMMAND_PACKET_RESEND_CMD = 0x0040,
        ARV_GVCP_COMMAND_PACKET_RESEND_ACK = 0x0041,
        ARV_GVCP_COMMAND_READ_REGISTER_CMD = 0x0080,
        ARV_GVCP_COMMAND_READ_REGISTER_ACK = 0x0081,
        ARV_GVCP_COMMAND_WRITE_REGISTER_CMD = 0x0082,
        ARV_GVCP_COMMAND_WRITE_REGISTER_ACK = 0x0083,
        ARV_GVCP_COMMAND_READ_MEMORY_CMD = 0x0084,
        ARV_GVCP_COMMAND_READ_MEMORY_ACK = 0x0085,
        ARV_GVCP_COMMAND_WRITE_MEMORY_CMD = 0x0086,
        ARV_GVCP_COMMAND_WRITE_MEMORY_ACK = 0x0087,
        ARV_GVCP_COMMAND_PENDING_ACK = 0x0089
    }
    ;

    /// <summary>
    /// see: https://aravisproject.github.io/docs/aravis-0.4/aravis-gvcp.html
    /// </summary>
    public class GvcpAck
    {
        public ArvGvcpPacketType Type { get; }
        public ArvGvcpCommand Command { get; }
        public int Length { get; private set; }
        public uint PacketId { get; }


        public GvcpAck(uint packetId, ArvGvcpPacketType type, ArvGvcpCommand command)
        {
            this.PacketId = packetId;
            this.Command = command;
            this.Type = type;
        }

        public BufferReader CreateBuffer(int length)
        {
            var b = new BufferReader(length + 8);

            this.Length = length;

            b.WriteWordBE((int)this.Type);
            b.WriteWordBE((int)this.Command);
            b.WriteWordBE((int)this.Length);
            b.WriteWordBE((int)this.PacketId);
      
            return b;

        }
    }
}
