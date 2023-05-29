﻿using GigE_Cam_Simulator.Streams;

namespace GigE_Cam_Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "D:\\Thomas\\GigE-Cam-simulator\\GigE-Cam-Simulator\\GigE-Cam-Simulator\\data\\";
            var cameraXml = Path.Combine(path, "camera.xml");
            var memoryXml = Path.Combine(path, "memory.xml");

            var preSetMemory = new RegisterConfig(memoryXml);

            var server = new Server(cameraXml, preSetMemory);

            server.OnRegisterChanged(RegisterTypes.Stream_Channel_Packet_Size_0, (mem) =>
            {
                // mem.WriteIntBE(0x128, 17301505); // PixelFormatRegister 
                // mem.WriteIntBE(0x104, 500); // HeightRegister 

                if (mem.ReadIntBE(RegisterTypes.Stream_Channel_Packet_Size_0) != 1400)
                {
                    mem.WriteIntBE(RegisterTypes.Stream_Channel_Packet_Size_0, 1400);
                }
            });

            server.OnRegisterChanged(0x124, (mem) =>
            {
                if (mem.ReadIntBE(0x124) == 1)
                {
                    Console.WriteLine("--- StartAcquisition");
                    server.StartAcquisition(100);
                }
                else
                {
                    Console.WriteLine("--- StopAcquisition");
                    server.StopAcquisition();
                }
                
            });

            var imageData = ImageData.FormFile(Path.Combine(path, "left01.jpg"));

            server.OnAcquiesceImage(() =>
            {
                return imageData;
            });

            server.Run();

            Console.WriteLine("Camera Server is running...");
            Console.ReadLine();
        }
    }
}