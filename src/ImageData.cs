namespace GigE_Cam_Simulator
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class ImageData
    {
        public byte[] Data { get; }
        public int Width { get; }
        public int Height { get; }
        public int Stride { get; }

        public ImageData(byte[] data, int width, int height, int stride)
        {
            Data = data;
            Width = width;
            Height = height;
            Stride = stride;
        }


        public static ImageData FormFile(string fileName)
        {
            byte[] bytes;
            int width = 0;
            int height = 0;
            int stride = 0;

            using (var bitmap = new Bitmap(fileName))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                var length = bitmapData.Stride * bitmapData.Height;

                width = bitmap.Width;
                height = bitmap.Height;
                stride = bitmapData.Stride;

                bytes = new byte[length];

                // Copy bitmap to byte[]
                Marshal.Copy(bitmapData.Scan0, bytes, 0, length);
                bitmap.UnlockBits(bitmapData);
            }


            return new ImageData(bytes, width, height, stride);
        }
    }
}
