using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Model.Media;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        public ActionResult MusicManagement()
        {
            return PartialView("PartialViews/MusicManagement",new YoYoStudio.Model.Json.JsonModel());
        }

        public JsonResult GetMusics(string page, string pageSize)
        {
            string message = string.Empty;
            List<MusicInfo> result = new List<MusicInfo>();
            try
            {
                DSClient client = new DSClient(Models.Const.ApplicationId);
                int userId;
                string token;
                if (GetToken(out userId, out token))
                {
                    int start = -1;
                    int p = 0, s = 0;
                    if (int.TryParse(page, out p) && int.TryParse(pageSize, out s))
                    {
                        start = (p - 1) * s;
                    }
                    List<MusicInfo> musics = client.GetMusics(Models.Const.ApplicationId, userId, token);
                   
                    for (int i = start; i < musics.Count && i < s; i++)
                    {
                        result.Add(musics[i]);
                    }
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return Json(new { Rows = result.ToArray() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteMusics(List<MusicInfo> toDeleted)
        {
            string message = string.Empty;
            try
            {
                DSClient client = new DSClient(Models.Const.ApplicationId);
                int userId;
                string token;
                if (GetToken(out userId, out token))
                {
                    client.DeleteMusics(Models.Const.ApplicationId, userId,token, toDeleted);
                    message = "Success";
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return Json(new {Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadMusics(List<Byte[]> toUpload)
        {
            string message = string.Empty;
            try
            {
                DSClient client = new DSClient(Models.Const.ApplicationId);
                int userId;
                string token;
                if (GetToken(out userId, out token))
                {
                    client.UploadMusics(Models.Const.ApplicationId, userId, token, toUpload);
                    message = "Success";
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}
