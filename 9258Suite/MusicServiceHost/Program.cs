using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoYoStudio.SocketService.Music;

namespace MusicServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpAsynchronousServer tcpServer = new TcpAsynchronousServer();
                tcpServer.StartListening();
                Console.WriteLine("MusicService OK");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        
    }
}
