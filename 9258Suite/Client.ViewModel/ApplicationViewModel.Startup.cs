using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using YoYoStudio.ChatService.Client;
using YoYoStudio.Common;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model;

namespace YoYoStudio.Client.ViewModel
{
	public partial class ApplicationViewModel
	{
		private Timer timer;

		public void StartUp()
		{
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, e) =>
                {
                    Cache = ClientCache.LoadCache();
                    Cache.BuildRelationship();
                    BuildCache();
                    Initialize();
                };
            worker.RunWorkerCompleted += (s, e) =>
                {
                    timer.Start();
                    Messenger.Default.Send<EnumNotificationMessage<object, WebWindowAction>>(
                        new EnumNotificationMessage<object, WebWindowAction>(WebWindowAction.InitHall));
                };
				
			timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds);
			timer.Elapsed += timer_Elapsed;
            worker.RunWorkerAsync();
		}

        private void UpdateOnlineUserCount()
        {
            if (OnlineUserCountChangedEvent != null)
            {
                lock (OnlineUserCountChangedEvent)
                {
                    var roomUsersCount = ChatClient.GetRoomOnlineUserCount();
                    if (roomUsersCount != null)
                    {
                        List<RoomViewModel> changeRooms = new List<RoomViewModel>();
                        foreach (var pair in roomUsersCount)
                        {
                            var roomVM = LocalCache.AllRoomVMs.FirstOrDefault(r => r.Id == pair.Key);
                            if (roomVM != null)
                            {
                                if (roomVM.OnlineUserCount != pair.Value)
                                {
                                    roomVM.OnlineUserCount = pair.Value;
                                    changeRooms.Add(roomVM);
                                }
                            }
                        }
                        if (changeRooms.Count > 0)
                        {
                            OnlineUserCountChangedEvent(changeRooms);
                        }
                    }
                }
            }
            else
            {
                ChatClient.KeepAlive();
            }
        }

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
            UpdateOnlineUserCount();
            Utility.MinimizeRelease();
		}

        public ImageViewModel AddImage(YoYoStudio.Model.Core.ImageWithoutBody img)
        {
            string group = string.IsNullOrEmpty(img.ImageGroup) ? Const.DefaultImageGroup : img.ImageGroup;
            string path = Const.ImageRootFolderName + "\\" + img.ImageType_Id.ToString() + "\\" + group + "\\" + img.Id;
            var vm = new ImageViewModel
            {
                RelativePathWithoutExt = path,
                AbsolutePathWithoutExt = Path.Combine(Root, path),
                Id = img.Id,
                ImageGroup = group,
                Tag = img,
                Ext = img.Ext
            };
            vm.Initialize();
            lock (LocalCache.AllImages)
            {
                LocalCache.AllImages[img.ImageType_Id][img.Id] = vm;
            }            
            return vm;
        }

        public void LogOff()
        {
			LocalCache.CurrentUserVM = null;
			IsAuthenticated = false;
            ChatClient.LogOff();
			ChatClient.Close();
        }

		private void BuildCache()
		{
			Cache.Images.ForEach(img =>
			{
                AddImage(img);
			});
			Cache.GiftGroups.ForEach(g =>
			{
				GiftGroupViewModel giftGroupVM = new GiftGroupViewModel(g);
				LocalCache.AllGiftGroupVMs.Add(giftGroupVM);
			});
			Cache.Gifts.ForEach(g =>
			{
				GiftViewModel giftVM = new GiftViewModel(g);
				giftVM.Initialize();
				LocalCache.AllGiftVMs.Add(giftVM);
			});
			LocalCache.AllGiftGroupVMs.ForEach(g =>
			{
				g.Initialize();
			});
			Cache.RoomGroups.ForEach(rg =>
			{
				RoomGroupViewModel roomGroupVM = new RoomGroupViewModel(rg);
				LocalCache.AllRoomGroupVMs.Add(roomGroupVM);
			});
			//Create all the RoomViewModels so that we can initialize the RoomVMs of RoomGroupVM
			Cache.Rooms.ForEach(r =>
			{
				RoomViewModel roomVM = new RoomViewModel(r);
				roomVM.Initialize();
				LocalCache.AllRoomVMs.Add(roomVM);
			});
			LocalCache.AllRoomGroupVMs.ForEach(rgv =>
			{
				rgv.Initialize();
			});
			Cache.Commands.ForEach(c =>
			{
				if (c.Application_Id == ApplicationId || c.Application_Id == BuiltIns.AllApplication.Id)
				{
					CommandViewModel vm = new CommandViewModel(c);
					vm.Initialize();
					LocalCache.AllCommandVMs.Add(vm);
				}
			});
			Cache.Roles.ForEach(r =>
			{
				if (r.Application_Id == ApplicationId || r.Application_Id == BuiltIns.AllApplication.Id)
				{
					RoleViewModel vm = new RoleViewModel(r);
					LocalCache.AllRoleVMs.Add(vm);
				}
			});
			Cache.RoleCommands.ForEach(rc =>
			{
				if (rc.Application_Id == ApplicationId || rc.Application_Id == BuiltIns.AllApplication.Id)
				{
					RoleCommandViewModel vm = new RoleCommandViewModel(rc);
					vm.Initialize();
					LocalCache.AllRoleCommandVMs.Add(vm);
				}
			});
			LocalCache.AllRoleVMs.ForEach(r =>
			{
				r.Initialize();
			});
			Cache.RoomRoles.ForEach(rr =>
			{
				RoomRoleViewModel vm = new RoomRoleViewModel(rr);
                vm.Initialize();
				LocalCache.AllRoomRoleVMs.Add(vm);
			});
            Cache.ExchangeRates.ForEach(er =>
                {
                    ExchangeRateViewModel vm = new ExchangeRateViewModel(er);
                    LocalCache.AllExchangeRateVMs.Add(vm);
                });

		}

        public void ShutDown()
        {
            Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.ApplicationShutdown));
        }

		#region IDisposable Implementation

		protected override void ReleaseUnManagedResource()
		{
            try
            {
                ProfileVM.Dispose();
                timer.Dispose();
                base.ReleaseUnManagedResource();
                ChatClient.Close();
            }
            catch
            { }
		}

        protected override void ReleaseManagedResource()
        {
            if (userListMenus != null)
            {
                userListMenus.Clear();
            }
            if (managerListMenus != null)
            {
                managerListMenus.Clear();
            }
            if (micUserListMenus != null)
            {
                micUserListMenus.Clear();
            }
            if (LocalCache != null)
            {
                LocalCache.ClearCache();
            }
            
            base.ReleaseManagedResource();
        }

		#endregion
	}
}
