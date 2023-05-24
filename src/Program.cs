namespace GigE_Cam_Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var file1 = "D:\\Thomas\\GigE-Cam-simulator\\GigE-Cam-Simulator\\GigE-Cam-Simulator\\data\\arv-fake-camera.xml";
            var file2 = "D:\\Thomas\\GigE-Cam-simulator\\GigE-Cam-Simulator\\GigE-Cam-Simulator\\data\\camera.xml";

            var server = new Server(file1);
   
            server.Run();

            Console.WriteLine("Hello, World!");
            Console.ReadLine();
        }
    }
}