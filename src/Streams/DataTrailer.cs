namespace GigE_Cam_Simulator.Streams
{


    /// <summary>
    /// Data Trailer Packet
    /// The data trailer packet MUST be the last packet of the block
    /// After the data trailer packet is sent, the block_id/block_id64 MUST be incremented to its
    /// next valid value and the packet_id/packet_id32 MUST be reset to 0 for the next block
    /// </summary>
    public class DataTrailer : GvspAck
    {
        public PayloadType PayloadType { get; }

        public DataTrailer(uint blockId, uint packetId, PayloadType payloadType) :
            base(blockId, packetId,  GvspPacketType.DATA_TRAILER_FROMAT)
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
