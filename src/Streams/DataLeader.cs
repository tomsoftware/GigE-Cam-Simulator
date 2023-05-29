using System.Runtime.Intrinsics.Arm;

namespace GigE_Cam_Simulator.Streams
{
    /// <summary>
    /// 24.3.1 Data Leader Packet
    /// The data leader packet MUST be the first packet of the block
    /// The data leader packet MUST be sent in a separate packet with packet_id/packet_id32 set
    /// to 0. It MUST fit in one packet of 576 bytes at most (including the IP, UDP and GVSP header)
    /// </summary>
    public class DataLeader : GvspAck
    {
        public PayloadType PayloadType { get; }

        public DataLeader(uint blockId, PayloadType payloadType) :
            base(blockId, 0,  GvspPacketType.DATA_LEADER_FROMAT)
        {
            this.PayloadType = payloadType;
        }

        public BufferReader ToBuffer(int length)
        {
            var b = CreateBuffer(length + 4);

            b.WriteWordBE(0); // reserved
            b.WriteWordBE((uint)this.PayloadType);

            return b;
        }
    }
}
