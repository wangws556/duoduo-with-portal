using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;

namespace YoYoStudio.DataService.Library
{
    public partial class DataService
    {
        private static object userIdLock = new object();
        private static int currentUserId = -1;
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		static DataService()
		{
            log4net.Config.XmlConfigurator.Configure();
			AddBuiltIns();
		}

		public static string GetServiceAddress(string ip)
		{
			return "https://" + ip + "/DataService/Service.svc";
		}

		private static void AddBuiltIns()
		{
			IModelAccesser accesser = new DirectModelAccesser();
			foreach (var imageType in BuiltIns.ImageTypes)
			{
				accesser.Get(imageType);
				if (!imageType.Loaded)
				{
					accesser.Add<ImageType>(imageType);
				}
			}

            foreach (var img in BuiltIns.Images)
            {
                accesser.Get(img);
                if (!img.Loaded)
                {
                    accesser.Add<Image>(img);
                }
            }

			foreach (var app in BuiltIns.Applications)
			{
				accesser.Get(app);
				if (!app.Loaded)
				{
					accesser.Add<Application>(app);
				}
			}
			foreach (var cmd in BuiltIns.Commands)
			{
				accesser.Get(cmd);
				if (!cmd.Loaded)
				{
					accesser.Add<Command>(cmd);
				}
			}
			foreach (var Role in BuiltIns.Roles)
			{
				accesser.Get(Role);
				if (!Role.Loaded)
				{
					accesser.Add<Role>(Role);
				}
			}
			foreach (var roleCommand in BuiltIns.RoleCommands)
			{
				accesser.Get(roleCommand);
				if (!roleCommand.Loaded)
				{
					accesser.Add<RoleCommand>(roleCommand);
				}
			}
			foreach (var user in BuiltIns.Users)
			{
				accesser.Get(user);
				if (!user.Loaded)
				{
					accesser.Add<User>(user);
				}
			}
			foreach (var userInfo in BuiltIns.UserInfos)
			{
				accesser.Get(userInfo);
				if (!userInfo.Loaded)
				{
					accesser.Add<UserApplicationInfo>(userInfo);
				}
			}


			foreach (var blockType in BuiltIns.BlockTypes)
			{
				accesser.Get(blockType);
				if (!blockType.Loaded)
				{
					accesser.Add<BlockType>(blockType);
				}
			}
		}

        
    }
}
