using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;

namespace YoYoStudio.DataService.Client
{
    public class DataServiceCache : Cache
    {
        private DSClient dataServiceClient;
        private int userId;
        private string token;
		private string applicationCondition = "";

        public DataServiceCache(int appId)
            : base(appId)
        {
			dataServiceClient = new DSClient(appId);
			applicationCondition = "[Application_Id]=" + BuiltIns.AllApplication.Id + " OR [Application_Id]=" + applicationId;
        }

        private Task LoadExchangeRateTask()
        {
            return Task.Factory.StartNew(() =>
                {
                    ExchangeRates = dataServiceClient.GetAllExchangeRate(userId, token, "", -1, -1);
                });
        }

        private Task LoadRoomGroupTask()
        {
            return Task.Factory.StartNew(() =>
                {
                    Rooms = dataServiceClient.GetRooms(userId, token, "", -1, -1);
                    RoomGroups = dataServiceClient.GetRoomGroups(userId, token, "", -1, -1);
                });
        }

        private Task LoadRoomRoleTask()
        {
            return Task.Factory.StartNew(() =>
            {
                int roomRoleCount = dataServiceClient.GetRoomRoleCount(userId, token, "");
				RoomRoles = new List<RoomRole>();
                if (roomRoleCount > 0)
                {
                    int batch = roomRoleCount / countPerBatch;
                    int left = roomRoleCount % countPerBatch;
                    int start = 1;
                    int count = countPerBatch;
                    for (int i = 0; i < batch; i++)
                    {
                        RoomRoles.AddRange(dataServiceClient.GetRoomRoles(userId, token, "", start, count));
                        start = start + countPerBatch;
                    }
                    if (left > 0)
                    {
                        RoomRoles.AddRange(dataServiceClient.GetRoomRoles(userId, token, "", start, left));
                    }
                }
            });
        }

        private Task LoadImageTask()
        {
            return Task.Factory.StartNew(() =>
                {
                    int imgCount = dataServiceClient.GetImageCount(userId, token, "");
					Images = new List<ImageWithoutBody>();
                    if (imgCount > 0)
                    {
                        int batch = imgCount / countPerBatch;
                        int left = imgCount % countPerBatch;
                        int start = 1;
                        int count = countPerBatch;
                        for (int i = 0; i < batch; i++)
                        {
                            Images.AddRange(dataServiceClient.GetImages(userId, token, "", start, count));
                            start = start + countPerBatch;
                        }
                        if (left > 0)
                        {
                            Images.AddRange(dataServiceClient.GetImages(userId, token, "", start, left));
                        }
                    }
                });
        }

        private Task LoadGiftGroupTask()
        {
            return Task.Factory.StartNew(() =>
            {
                GiftGroups = dataServiceClient.GetGiftGroups(userId, token, "", -1, -1);
                Gifts = dataServiceClient.GetGifts(userId, token, "", -1, -1);
            });
        }

        private Task LoadRoleCommandTask()
        {
            return Task.Factory.StartNew(() =>
                {
                    Roles = dataServiceClient.GetRoles(userId, token, applicationCondition, -1, -1);
                    RoleCommands = dataServiceClient.GetRoleCommandViews(userId, token, applicationCondition, -1, -1);
                    
                });
        }

        private Task LoadTask()
        {
            return Task.Factory.StartNew(() =>
                {
                    Application = dataServiceClient.GetApplication(userId, token, applicationId);
					Commands = dataServiceClient.GetCommands(userId, token, applicationCondition, -1, -1);
                    BlockTypes = dataServiceClient.GetBlockTypes(userId, token, "", -1, -1);
                });
        }

        public override void RefreshCache(params object[] args)
        {
            userId = (int)args[0];
            token = (string)args[1];
            Task.WaitAll(new Task[] { LoadExchangeRateTask(), LoadRoomGroupTask(), LoadRoomRoleTask(), LoadImageTask(), LoadGiftGroupTask(),LoadRoleCommandTask(), LoadTask() });
			dataServiceClient.Close();
            base.RefreshCache(args);
        }
    }
}
