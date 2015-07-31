using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Snippets;
using YoYoStudio.Common;
using YoYoStudio.Common.Notification;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.RoomService.Client;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using YoYoStudio.Model.Json;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.ViewModel
{
    [ComVisible(true)]
	[SnippetPropertyINPC(field="roomGroupVM", property="RoomGroupVM",  type = "RoomGroupViewModel", defaultValue="null")]
    [SnippetPropertyINPC(field = "name", property = "Name", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "enabled", property = "Enabled", type = "bool", defaultValue = "false")]
    [SnippetPropertyINPC(field = "serviceIp", property = "ServiceIp", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "rtmpUrl", property = "RtmpUrl", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "reserveRoom", property = "ReserveRoom", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "recommendRoom", property = "RecommendRoom", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "roomHeader", property = "RoomHeader", type = "string", defaultValue = "string.Empty")]
	public partial class RoomViewModel : ImagedEntityViewModel
	{
        public RoomViewModel(Room room)
            : base(room)
        {
            name.SetValue(room.Name);
            RoomGroupId = room.RoomGroup_Id.HasValue ? room.RoomGroup_Id.Value : -1;
            serviceIp.SetValue(room.ServiceIp);
            rtmpUrl.SetValue("rtmp://" + ServiceIp + "/oflaDemo");
            MaxUserCount = room.MaxUserCount.Value;
        }

        public RoomGroupViewModel RootRoomGroupVM
        {
            get
            {
                return RoomGroupVM.RootRoomGroupVM;
            }
        }

        public int OnlineUserCount { get; set; }

        public int RoomGroupId { get; private set; }

        public int MaxUserCount { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            roomGroupVM.SetValue(ApplicationVM.LocalCache.AllRoomGroupVMs.FirstOrDefault(rg => rg.Id == RoomGroupId));            
        }

        protected override void InitializeResource()
        {
            base.InitializeResource();
            ReserveRoom = Text.ReserveRoom;
            RoomHeader = string.Format("({0}){1}", Id, Name);
            RecommendRoom = Text.RecommendRoom;
        }

		public void Enter()
		{
            if (ApplicationVM.RoomWindowVM != null)
            {
                ApplicationVM.RoomWindowVM.Dispose();
            }
            ApplicationVM.RoomWindowVM = new RoomWindowViewModel(this);
            ApplicationVM.RoomWindowVM.Enter();
		}

        public override object ToJson()
        {
            NodeModel node = new NodeModel() { id = Id, name = Name, count = OnlineUserCount, rootid = RoomGroupVM.RootRoomGroupVM.Id,maxcount = MaxUserCount };
            if (ImageVM != null)
				node.icon = ImageVM.StaticImageFile;
            return node;
        }
    }
}
