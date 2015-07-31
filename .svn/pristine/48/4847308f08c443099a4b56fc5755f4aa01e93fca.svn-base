using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using YoYoStudio.SocketService.Audio;

namespace YoYoStudio.AudioService
{
    public class AudioServiceWindowsService : ServiceBase
    {
        public Server server = null;
        public AudioServiceWindowsService()
        {
            ServiceName = "9258AudioService";
        }

        public static void Main(string[] args)
        {
            ServiceBase.Run(new AudioServiceWindowsService());
        }

        //start the windows service
        protected override void OnStart(string[] args)
        {
            if (server != null && server.IsRunning)
            {
                server.Stop();
            }
            server = new Server();
            server.Start();
        }

        //stop the windows service
        protected override void OnStop()
        {
            if (server != null && server.IsRunning)
            {
                server.Stop();
                server = null;
            }
        }
    }

    [RunInstaller(true)]
    public class AudioServiceInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;
        public AudioServiceInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "9258AudioService";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
