using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using YoYoStudio.Common;
using YoYoStudio.DataService.Client;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Model;

namespace YoYoStudio.ChatService.Library
{
	public partial class ChatService
	{
        public static int ApplicationId = BuiltIns._9258ChatApplication.Id;

		private static SafeDictionary<int, UserNCallback> userCache = new SafeDictionary<int, UserNCallback>();
        private static SafeDictionary<string, RoomNCallback> roomCache = new SafeDictionary<string, RoomNCallback>();
		private static Dictionary<int, int> roomUserCountCache = new Dictionary<int, int>();
		private static DSClient dataServiceClient;
		private static DataServiceCache cache;
		private static string serviceToken;
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static long cacheVersion = 1;
        private const string cacheVersionFile = "Cache.Ver";
        private static Timer timer = new Timer();

		public static void Initialize()
		{
            log4net.Config.XmlConfigurator.Configure();
			dataServiceClient = new DSClient(ApplicationId);
			serviceToken = dataServiceClient.Login(BuiltIns._9258Administrator.Id, BuiltIns._9258Administrator.Password);
			cache = new DataServiceCache(ApplicationId);
			cache.RefreshCache(BuiltIns._9258Administrator.Id, serviceToken);
            cache.BuildRelationship();
            timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            if (File.Exists(cacheVersionFile))
            {                
                var lines = File.ReadLines(cacheVersionFile);
                cacheVersion = long.Parse(lines.ElementAt(0)) + 1;
            }
            File.Delete(cacheVersionFile);
            File.WriteAllText(cacheVersionFile, cacheVersion.ToString());
		}

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            serviceToken = dataServiceClient.Login(BuiltIns._9258Administrator.Id, BuiltIns._9258Administrator.Password);
        }		
	}
}
