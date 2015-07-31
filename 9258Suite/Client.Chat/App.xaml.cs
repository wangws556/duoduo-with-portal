using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using YoYoStudio.Client.Chat.WebPages;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Common;

namespace Client.Chat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                YoYoStudio.Client.Chat.Helper.Logger.Info("Application Started : " + DateTime.Now.ToLongTimeString());
                AllWebPages.Initialize();
            }
            catch (Exception ex)
            {
                YoYoStudio.Client.Chat.Helper.Logger.Error("OnStartup", ex);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Singleton<ApplicationViewModel>.Instance.Dispose();
            YoYoStudio.Client.Chat.Helper.Logger.Info("Application Shutdown : " + DateTime.Now.ToLongTimeString());
            base.OnExit(e);
        }		
    }
}
