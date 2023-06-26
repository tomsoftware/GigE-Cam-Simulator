namespace GigE_Cam_Simulator
{
    using SkiaSharp;
    using System.Drawing;

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

            using (var img = SKBitmap.Decode(fileName))
            {
                var length = img.Width * img.Height;

                width = img.Width;
                height = img.Height;

                bytes = new byte[length];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var pixel = img.GetPixel(x, y);

                        bytes[y * width + x] = (byte)((pixel.Red + pixel.Green + pixel.Blue) / 3);
                    }
                }

            }


            return new ImageData(bytes, width, height, width);
        }
    }
}
