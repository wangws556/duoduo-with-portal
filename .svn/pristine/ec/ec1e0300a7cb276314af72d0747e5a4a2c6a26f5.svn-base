using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.RoomService.Library;

namespace YoYoStudio.RoomServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RoomService.Library.RoomService.Initialize();
                System.ServiceModel.ServiceHost host = new System.ServiceModel.ServiceHost(typeof(RoomService.Library.RoomService));
                host.Open();
                Console.WriteLine("Room Service OK : ");
                Console.WriteLine(host.BaseAddresses[0].AbsoluteUri);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }
    }
}
