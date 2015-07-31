using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model.Json;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.ViewModel
{
    public partial class RoomWindowViewModel
    {
        public string GetMenuData(string uId)
        {
            int userId = -1;
            if (int.TryParse(uId, out userId))
            {
                UserViewModel uvm = GetUser(userId);
                if (uvm != null)
                {
                    var menus = ApplicationVM.GetUserListMenus();
                    foreach (var m in menus)
                    {
                        m.disabled = !Me.HasCommand(RoomVM.Id, m.id, uvm.RoleVM.Id);
                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    return js.Serialize(menus);
                }
            }
            return string.Empty;
        }

        private string giftGroupJson = string.Empty;

        public string GiftGroupsJson
        {
            get
            {
                if (string.IsNullOrEmpty(giftGroupJson))
                {
                    List<GiftGroupModel> groups = new List<GiftGroupModel>();
                    ApplicationVM.LocalCache.AllGiftGroupVMs.ForEach(g => groups.Add(g.ToJson() as GiftGroupModel));
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    giftGroupJson = js.Serialize(groups);
                }
                return giftGroupJson;
            }
        }

        private string stampImagesJson = string.Empty;

        public string StampImagesJson
        {
            get
            {
                if (string.IsNullOrEmpty(stampImagesJson))
                {
                    List<ImageGroupViewModel> faces = new List<ImageGroupViewModel>();
                    foreach (ImageViewModel imageVM in StampImageVMs)
                    {
                        var groupName = imageVM.ImageGroup;
                        var item = faces.FirstOrDefault(r => r.Name == groupName);
                        if (item != null)
                            item.ImageVMs.Add(imageVM);
                        else
                        {
                            ImageGroupViewModel groupVM = new ImageGroupViewModel(groupName);
                            groupVM.ImageVMs.Add(imageVM);
                            faces.Add(groupVM);
                        }
                    }

                    JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
                    stampImagesJson = js.Serialize(faces);
                }
                return stampImagesJson;
            }
        }

        private List<string> motionImagesJson = new List<string>();

        public List<string> MotionImagesJson
        {
            get
            {
                if (motionImagesJson.Count == 0)
                {
                    List<ImageGroupViewModel> faces = new List<ImageGroupViewModel>();
                    int id = 0;
                    foreach (ImageViewModel imageVM in MotionImageVMs)
                    {
                        var groupName = imageVM.ImageGroup;
                        var item = faces.FirstOrDefault(r => r.Name == groupName);
                        if (item != null)
                            item.ImageVMs.Add(imageVM);
                        else
                        {
                            ImageGroupViewModel groupVM = new ImageGroupViewModel(groupName) { Id = id++ };
                            groupVM.ImageVMs.Add(imageVM);
                            faces.Add(groupVM);
                        }
                    }
					//string path = Environment.CurrentDirectory + "\\Images\\9\\" + Text.CustomizedMotion + "\\";
					//if (!Directory.Exists(path))
					//{
					//	Directory.CreateDirectory(path);
					//}
					//else
					//{
					//	faces.Add(GetFaces(path));
					//}

                    JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
                    foreach (var g in faces)
                    {
                        motionImagesJson.Add(js.Serialize(g));
                    }
                }
                return motionImagesJson;
            }
        }

        private string fontFamiliesJson = string.Empty;
        public string FontFamiliesJson
        {
            get
            {
                if (string.IsNullOrEmpty(fontFamiliesJson))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    fontFamiliesJson = js.Serialize(FontFamilies);
                }
                return fontFamiliesJson;
            }
        }

        private string fontSizesJson = string.Empty;

        public string FontSizesJson
        {
            get
            {
                if (string.IsNullOrEmpty(fontSizesJson))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    fontSizesJson = js.Serialize(FontSizes);
                }
                return fontSizesJson;
            }
        }

        public string UsersJson
        {
            get
            {
                List<ClientUserModel> result = new List<ClientUserModel>();
                foreach (var user in UserVMs)
                {
                    result.Add(user.ToJson() as ClientUserModel);
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(result);
            }
        }
    }
}
