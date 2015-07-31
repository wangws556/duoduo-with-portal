using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;

namespace YoYoStudio.RoomService
{
	public class RoomServiceWindowsService:ServiceBase
	{
        public ServiceHost serviceHost = null;
        public RoomServiceWindowsService()
        {
            ServiceName = "9258RoomService";
        }
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		public static void Main()
		{
            ServiceBase.Run(new RoomServiceWindowsService());
		}

        //start the windows service
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }
            RoomService.Library.RoomService.Initialize();
            serviceHost = new ServiceHost(typeof(RoomService.Library.RoomService));
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
    public class RoomServiceInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;
        public RoomServiceInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "9258RoomService";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
