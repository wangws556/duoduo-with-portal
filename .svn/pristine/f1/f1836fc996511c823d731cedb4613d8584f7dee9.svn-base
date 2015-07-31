using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;

namespace YoYoStudio.ChatService
{
	public partial class WcfServiceWrapper : ServiceBase
	{
		private ServiceHost wcfHost;
		public WcfServiceWrapper()
		{
			InitializeComponent();
			ServiceName = YoYoStudio.Resource.Text.ChatService;
		}

		protected override void OnStart(string[] args)
		{
			if (wcfHost != null)
			{
				wcfHost.Close();
			}
            ChatService.Library.ChatService.Initialize();
			wcfHost = new ServiceHost(typeof(ChatService.Library.ChatService));
			wcfHost.Open();
		}

		protected override void OnStop()
		{
			if (wcfHost != null)
			{
				wcfHost.Close();
				wcfHost = null;
			}
		}

	}
}
