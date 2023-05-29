namespace GigE_Cam_Simulator.Streams
{

    /// <summary>
    /// 25.2.3 Image Data Trailer Packet
    /// </summary>
    public class DataTrailer_ImageData : DataTrailer
    {
        /// <summary>
        /// This field is the actual height, in lines, for this particular data block. This is done to
        /// support variable frame size image sources once the actual number of lines transmitted has
        /// been confirmed.Only image height can be variable.
        /// For interlaced image, this represents the height of one field as each field is considered to
        /// be a data block
        /// </summary>
        uint SizeY { get; set; }


        public DataTrailer_ImageData(uint blockId, uint packetId, uint sizeY) :
            base(blockId, packetId, PayloadType.Image)
        {
            this.SizeY = sizeY;
        }

        public new BufferReader ToBuffer()
        {
            var b = base.ToBuffer(4);

            b.WriteIntBE(this.SizeY);
       
            return b;

        }
    }
}
