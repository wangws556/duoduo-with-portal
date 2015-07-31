using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.Configuration.Install;
using System.ComponentModel;

namespace YoYoStudio.ChatService
{
    public class ChatServiceWindowsService : ServiceBase
    {
        public ServiceHost serviceHost = null;
        public ChatServiceWindowsService()
        {
            ServiceName = "9258ChatService";
        }
        public static void Main()
        {
            ServiceBase.Run(new ChatServiceWindowsService());
        }
        //start the windows service
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }
            ChatService.Library.ChatService.Initialize();
            serviceHost = new ServiceHost(typeof(ChatService.Library.ChatService));

            serviceHost.Open();
        }

        //stop the windows service
        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }

    //Provide the ProjectInstaller class which allows the service to be installed
    //by Installutil.exe tool
    [RunInstaller(true)]
    public class ChatServiceInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;
        public ChatServiceInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "9258ChatService";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
