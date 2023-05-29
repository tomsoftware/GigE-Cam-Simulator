namespace GigE_Cam_Simulator.Streams
{
    [Flags]
    public enum GvspStatusType
    {
        GEV_FLAG_Device_specific_1 = 0x01,
        GEV_FLAG_RESEND_RANGE_ERROR = 0x1000,
        GEV_FLAG_PREVIOUS_BLOCK_DROPPED = 0x2000,
        GEV_FLAG_PACKET_RESEND = 0x4000
    }

    /// <summary>
    /// To efficiently transport information, GVSP defines various payload types that can be streamed out of a
    /// GVSP transmitter.
    /// </summary>
    public enum GvspPacketType
    {
        DATA_LEADER_FROMAT = 1,
        DATA_TRAILER_FROMAT = 2,
        DATA_PAYLOAD_GENERIC_FROMAT = 3,
        DATA_PAYLOAD_H_264_FROMAT = 5,
        DATA_PAYLOAD_MULTI_ZONE_FROMAT = 6,
        DATA_ALL_IN_FROMAT = 4,
    }

    public enum PayloadType
    {
        /// <summary>
        /// Uncompressed image data
        /// </summary>
        Image = 1,

        /// <summary>
        /// Raw binary data
        /// </summary>
        RawData = 2,

        /// <summary>
        /// A computer file ready to be saved to PC hard drive
        /// </summary>
        File = 3,

        /// <summary>
        /// Tagged blocks of data, as per the GenICamTM specification
        /// </summary>
        ChunkData = 4,

        /// <summary>
        /// Tagged blocks of data, where the first chunk is an image
        /// </summary>
        [Obsolete]
        ExtendedChunkData = 5,

        /// <summary>
        /// JPEG compressed image, as per ITU-T Rec. T.81
        /// </summary>
        JPEG = 6,

        /// <summary>
        /// JPEG 2000 codestream, as per ITU-T Rec. T.800
        /// </summary>
        JPEG2000 = 7,

        /// <summary>
        /// H.264 access unit, as per ITU-T Rec. H.264
        /// </summary>
        H264 = 8,

        /// <summary>
        /// Uncompressed image data sliced into horizontal bands called zones.
        /// </summary>
        MultiZoneImage = 9
    }

    /// <summary>
    /// GigE Vision Command Steam Header
    /// see: https://aravisproject.github.io/docs/aravis-0.4/aravis-gvsp.html
    /// </summary>
    public class GvspAck
    {
        /// <summary>
        /// Status of the streaming operation
        /// </summary>
        public uint Status { get; }

        /// <summary>
        /// ID of the data block.Sequential and incrementing starting at 1. A block_id of 0 is
        /// reserved for the test packet.block_id wraps-around to 1 when it reaches its
        /// maximum value.For a GVSP transmitter, the block_id is reset to 1 when the
        /// stream channel is opened.
        /// </summary>
        public uint BlockId { get; }

        /// <summary>
        /// Combination of:
        /// - EI: Extended ID (EI) flag indicating if block IDs are 64 bits and packet IDs are 32 bits.
        /// - reserved: ignore
        /// - packet_format: Transmission Mode
        /// </summary>
        public GvspPacketType PacketFormat { get; }

        /// <summary>
        /// ID of packet in the block. The packet_id is reset to 0 at the start of each data
        /// block.packet_id 0 is thus the data leader for the current block_id.
        /// </summary>
        public uint PacketId { get; }


        public GvspAck(uint blockId, uint packetId, GvspPacketType format)
        {
            BlockId = blockId;
            PacketId = packetId;
            PacketFormat = format;
        }

        public BufferReader CreateBuffer(int length)
        {
            var b = new BufferReader(length + 12);

            b.WriteWordBE(Status);
            b.WriteWordBE(BlockId);
            b.WriteIntBE((uint)PacketFormat << 24 | PacketId);
            b.WriteIntBE(PacketId);

            return b;

        }
    }
}
