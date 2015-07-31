using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.SocketService.Audio;

namespace AudioServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Server server = new Server();
                server.Start();
                Console.WriteLine("AudioService OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
