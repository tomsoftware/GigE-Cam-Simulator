namespace GigE_Cam_Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "D:\\Thomas\\GigE-Cam-simulator\\GigE-Cam-Simulator\\GigE-Cam-Simulator\\data\\";
            var cameraXml = Path.Combine(path, "camera.xml");
            var memoryXml = Path.Combine(path, "memory.xml");

            var memory = new RegisterConfig(memoryXml);

            var server = new Server(cameraXml, memory);
   
            server.Run();

            Console.WriteLine("Hello, World!");
            Console.ReadLine();
        }
    }
}