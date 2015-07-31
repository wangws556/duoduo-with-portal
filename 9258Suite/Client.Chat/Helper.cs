using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Wpf;
using System.Windows;
using System.Threading;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Client.Chat.Controls;

namespace YoYoStudio.Client.Chat
{
    public static class Helper
    {       
        static Helper()
        {
        }
        public static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public static HallWindow MainWindow { get; set; }

        public static void OpenAndSelectFile(string file)
        {
            string args = string.Format("/Select, {0}", file);
            ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", args);
            System.Diagnostics.Process.Start(pfi);
        }

		
    }
}
