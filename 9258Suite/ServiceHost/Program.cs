using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.ChatService.Library;

namespace YoYoStudio.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                ChatService.Library.ChatService.Initialize();
                System.ServiceModel.ServiceHost host = new System.ServiceModel.ServiceHost(typeof(ChatService.Library.ChatService));
                host.Open();
                Console.WriteLine("Hall Service OK : ");
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
