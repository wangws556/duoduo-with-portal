using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using YoYoStudio.Common;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Json;
using System.Runtime.InteropServices;
using YoYoStudio.Model;

namespace YoYoStudio.Client.ViewModel
{
    [ComVisible(true)]
    [Serializable]
    [SnippetPropertyINPC(field = "roomVMs", property = "RoomVMs", type = "ObservableCollection<RoomViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "name", property = "Name", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "subRoomGroupVMs", property = "SubRoomGroupVMs", type = "ObservableCollection<RoomGroupViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "parentRoomGroupVM", property = "ParentRoomGroupVM", type = "RoomGroupViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "enabled", property = "Enabled", type = "bool", defaultValue = "false")]
    public partial class RoomGroupViewModel : ImagedEntityViewModel
    {
        public RoomGroupViewModel(RoomGroup rg)
            : base(rg)
        {
            name.SetValue(rg.Name);
            ParentRoomGroupId = rg.ParentGroup_Id.HasValue ? rg.ParentGroup_Id.Value : -1;
        }

        public RoomGroupViewModel RootRoomGroupVM
        {
            get
            {
                if (ParentRoomGroupVM != null)
                {
                    return ParentRoomGroupVM.RootRoomGroupVM;
                }
                return this;
            }
        }

        public int ParentRoomGroupId {get;private set;}

        private int onlineUserCount = -1;

        public int OnlineUserCount
        {
            get
            {
                if (onlineUserCount < 0)
                {
                    onlineUserCount = GetOnlineUserCount();
                }
                return onlineUserCount;
            }
            set
            {
                onlineUserCount = value;
            }
        }

        public int GetOnlineUserCount()
        {
            int onlineUserCount = 0;
            if (SubRoomGroupVMs != null)
            {
                foreach (var g in SubRoomGroupVMs)
                {
                    onlineUserCount += g.OnlineUserCount;
                }
            }
            if (RoomVMs != null)
            {
                foreach (var r in RoomVMs)
                {
                    onlineUserCount += r.OnlineUserCount;
                }
            }
            return onlineUserCount;
        }

        public override void Initialize()
        {
            subRoomGroupVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<RoomGroupViewModel>(ApplicationVM.LocalCache.AllRoomGroupVMs.Where(rgv => rgv.ParentRoomGroupId == Id)));
            roomVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<RoomViewModel>(ApplicationVM.LocalCache.AllRoomVMs.Where(r => r.RoomGroupId == Id)));
            parentRoomGroupVM.SetValue(ApplicationVM.LocalCache.AllRoomGroupVMs.FirstOrDefault(g => g.Id == ParentRoomGroupId));
            base.Initialize();
        }

        private string location;
        public string Location
        {
            get
            {
                if (string.IsNullOrEmpty(location))
                {
                    if (ParentRoomGroupVM == null)
                    {
                        location = Name;
                    }
                    else
                    {
                        location = ParentRoomGroupVM.Location + Const.PathDelimiter + Name;
                    }
                }
                return location;
            }
        }

        public override object ToJson()
        {
            TreeNodeModel root = new TreeNodeModel() { id = Id, name = Name, value = Id.ToString(), url = Location, count = OnlineUserCount };
			if (ImageVM != null && ImageVM.Id > 0 && (!string.IsNullOrEmpty(ImageVM.StaticImageFile)))
            {
				root.icon = ImageVM.StaticImageFile;
            }
            if (RoomVMs != null && RoomVMs.Count > 0)
            {
                root.nodes = new List<NodeModel>();
                foreach (var r in RoomVMs)
                {
					if (!string.IsNullOrEmpty(r.ServiceIp))
					{
						root.nodes.Add(r.ToJson() as NodeModel);
					}
                }
            }
            if (SubRoomGroupVMs != null && SubRoomGroupVMs.Count > 0)
            {
                root.items = new List<TreeNodeModel>();
                foreach (var srg in SubRoomGroupVMs)
                {
                    root.items.Add(srg.ToJson() as TreeNodeModel);
                }
            }
            return root;
        }
    }
}

