namespace GigE_Cam_Simulator.Streams
{
    public enum GVSP_PIX
    {
        MONO = 0x01000000,
        RGB = 0x02000000, /// deprecated in version 1.1
        COLOR = 0x02000000,
    }

    public enum GVSP_PIX_OCCUPY
    {
        OCCUPY1BIT = 0x00010000,
        OCCUPY2BIT = 0x00020000,
        OCCUPY4BIT = 0x00040000,
        OCCUPY8BIT = 0x00080000,
        OCCUPY12BIT = 0x000C0000,
        OCCUPY16BIT = 0x00100000,
        OCCUPY24BIT = 0x00180000,
        OCCUPY32BIT = 0x00200000,
        OCCUPY36BIT = 0x00240000,
        OCCUPY48BIT = 0x00300000,
    }

    public enum PixelFormat
    {
        // 27.1 Mono buffer format defines
        GVSP_PIX_MONO1P = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY1BIT | 0x0037),
        GVSP_PIX_MONO2P = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY2BIT | 0x0038),
        GVSP_PIX_MONO4P = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY4BIT | 0x0039),
        GVSP_PIX_MONO8 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY8BIT | 0x0001),
        GVSP_PIX_MONO8S = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY8BIT | 0x0002),
        GVSP_PIX_MONO10 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0003),
        GVSP_PIX_MONO10_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x0004),
        GVSP_PIX_MONO12 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0005),
        GVSP_PIX_MONO12_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x0006),
        GVSP_PIX_MONO14 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0025),
        GVSP_PIX_MONO16 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0007),

        // 27.2 Bayer buffer format defines
        GVSP_PIX_BAYGR8 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY8BIT | 0x0008),
        GVSP_PIX_BAYRG8 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY8BIT | 0x0009),
        GVSP_PIX_BAYGB8 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY8BIT | 0x000A),
        GVSP_PIX_BAYBG8 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY8BIT | 0x000B),
        GVSP_PIX_BAYGR10 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x000C),
        GVSP_PIX_BAYRG10 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x000D),
        GVSP_PIX_BAYGB10 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x000E),
        GVSP_PIX_BAYBG10 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x000F),
        GVSP_PIX_BAYGR12 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0010),
        GVSP_PIX_BAYRG12 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0011),
        GVSP_PIX_BAYGB12 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0012),
        GVSP_PIX_BAYBG12 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0013),
        GVSP_PIX_BAYGR10_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x0026),
        GVSP_PIX_BAYRG10_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x0027),
        GVSP_PIX_BAYGB10_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x0028),
        GVSP_PIX_BAYBG10_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x0029),
        GVSP_PIX_BAYGR12_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x002A),
        GVSP_PIX_BAYRG12_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x002B),
        GVSP_PIX_BAYGB12_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x002C),
        GVSP_PIX_BAYBG12_PACKED = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x002D),
        GVSP_PIX_BAYGR16 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x002E),
        GVSP_PIX_BAYRG16 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x002F),
        GVSP_PIX_BAYGB16 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0030),
        GVSP_PIX_BAYBG16 = (GVSP_PIX.MONO | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0031),

        // 27.3 RGB Packed buffer format defines
        GVSP_PIX_RGB8 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY24BIT | 0x0014),
        GVSP_PIX_BGR8 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY24BIT | 0x0015),
        GVSP_PIX_RGBA8 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY32BIT | 0x0016),
        GVSP_PIX_BGRA8 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY32BIT | 0x0017),
        GVSP_PIX_RGB10 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY48BIT | 0x0018),
        GVSP_PIX_BGR10 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY48BIT | 0x0019),
        GVSP_PIX_RGB12 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY48BIT | 0x001A),
        GVSP_PIX_BGR12 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY48BIT | 0x001B),
        GVSP_PIX_RGB16 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY48BIT | 0x0033),
        GVSP_PIX_RGB10V1_PACKED = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY32BIT | 0x001C),
        GVSP_PIX_RGB10P32 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY32BIT | 0x001D),
        GVSP_PIX_RGB12V1_PACKED = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY36BIT | 0X0034),
        GVSP_PIX_RGB565P = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0035),
        GVSP_PIX_BGR565P = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0X0036),

        //27.4 YUV and YCbCr Packed buffer format defines
        GVSP_PIX_YUV411_8_UYYVYY = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x001E),
        GVSP_PIX_YUV422_8_UYVY = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x001F),
        GVSP_PIX_YUV422_8 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0032),
        GVSP_PIX_YUV8_UYV = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY24BIT | 0x0020),
        GVSP_PIX_YCBCR8_CBYCR = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY24BIT | 0x003A),
        GVSP_PIX_YCBCR422_8 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x003B),
        GVSP_PIX_YCBCR422_8_CBYCRY = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0043),
        GVSP_PIX_YCBCR411_8_CBYYCRYY = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x003C),
        GVSP_PIX_YCBCR601_8_CBYCR = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY24BIT | 0x003D),
        GVSP_PIX_YCBCR601_422_8 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x003E),
        GVSP_PIX_YCBCR601_422_8_CBYCRY = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0044),
        GVSP_PIX_YCBCR601_411_8_CBYYCRYY = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY12BIT | 0x003F),
        GVSP_PIX_YCBCR709_8_CBYCR = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY24BIT | 0x0040),
        GVSP_PIX_YCBCR709_422_8 = (GVSP_PIX.COLOR | GVSP_PIX_OCCUPY.OCCUPY16BIT | 0x0041)
    }



    /// <summary>
    /// 25.2.1 Image Data Leader Packet
    /// </summary>
    public class DataLeader_ImageData : DataLeader
    {
        /// Ingonre: field_id
        /// Ingonre: field_count

        /// <summary>
        /// 64-bit timestamp representing when the block of data was generated. Timestamps are
        /// optional.This field should be 0 when timestamps are not supported.
        /// Different ROIs from the same exposure must use the same timestamp
        /// Note: The GenICam specification supports 64-bit signed integer while this field is 64-bit
        /// unsigned.Therefore, only 63 bits are available through the GenICam interface
        /// </summary>
        uint Timestamp_high { get; set; }
        uint Timestamp_low { get; set; }

        /// <summary>
        /// This field gives the pixel format of payload data. Refer to Pixel Layout section
        /// </summary>
        PixelFormat PixelFormat { get; set; }

        /// <summary>
        /// Width in pixels of the image transported in the data payload portion of this data block.
        /// </summary>
        uint SizeX { get; set; }

        /// <summary>
        /// Height in lines of the image (for progressive scan) or field (for interlaced scan)
        /// transported in the data payload portion of this data block.
        /// </summary>
        uint SizeY { get; set; }

        /// <summary>
        /// Offset in pixels from image origin. Used for ROI support. When no ROI is defined this
        /// field must be set to 0.
        /// </summary>
        uint OffsetX { get; set; }

        /// <summary>
        /// Offset in lines from image origin. Used for ROI support. When no ROI is defined this
        /// field must be set to 0
        /// </summary>
        uint OffsetY { get; set; }

        /// <summary>
        /// Horizontal padding expressed in bytes. Number of extra bytes transmitted at the end of
        /// each line to facilitate image alignment in buffers.This can typically used to have 32-bit
        /// aligned image lines.This is similar to the horizontal invalid (or horizontal blanking) in
        /// analog cameras.
        ///  Set to 0 when no horizontal padding is used.
        /// </summary>
        uint PaddingX { get; set; }

        /// <summary>
        /// Vertical padding expressed in bytes. Number of extra bytes transmitted at the end of the
        /// image to facilitate image alignment in buffers.This could be used to align buffers to
        /// certain block size(for instance 4 KB). This is similar to the vertical invalid(or vertical
        /// blanking) in analog cameras.
        /// Set to 0 when no vertical padding is used.
        /// </summary>
        uint PaddingY { get; set; }

        public DataLeader_ImageData(uint blockId, PixelFormat pixelFormat, uint sizeX, uint sizeY) :
            base(blockId, PayloadType.Image)
        {
            this.PixelFormat = pixelFormat;
            this.SizeX = sizeX;
            this.SizeY = sizeY;
        }

        public new BufferReader ToBuffer()
        {
            var b = base.ToBuffer(8 * 4);

            b.WriteIntBE(this.Timestamp_high);
            b.WriteIntBE(this.Timestamp_low);
            b.WriteIntBE((uint)this.PixelFormat);
            b.WriteIntBE(this.SizeX);
            b.WriteIntBE(this.SizeY);
            b.WriteIntBE(this.OffsetX);
            b.WriteIntBE(this.OffsetY);
            b.WriteWordBE(this.PaddingX);
            b.WriteWordBE(this.PaddingY);

            return b;

        }
    }
}
