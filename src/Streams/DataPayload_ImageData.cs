namespace GigE_Cam_Simulator.Streams
{

    /// <summary>
    /// 24.3.2 Data Payload Packet
    /// Data payload packets transport the actual information to a GVSP receiver.There might be up to 16 million
    /// data payload packets per block (for 24-bit packet_id field).
    /// The last data payload packet of a block might have a smaller size than the other payload packets since total
    /// amount of data to transfer is not necessarily a multiple of the packet siz
    /// </summary>
    public class DataPayload_ImageData : GvspAck
    {
        public DataPayload_ImageData(uint blockId, uint packetId) :
            base(blockId, packetId, GvspPacketType.DATA_PAYLOAD_GENERIC_FROMAT)
        {
        }

        public BufferReader ToBuffer(byte[] payload, int offset, int length)
        {
            var b = CreateBuffer(payload.Length);

            b.WriteBytes(payload, payload.Length);

            return b;

        }
    }
}
