using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Exceptions;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Json;
using YoYoJson = YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        public ActionResult GiftGroupManagement()
        {
            return PartialView("PartialViews/GiftGroupManagement", new YoYoJson.JsonModel());
        }

        public JsonResult GetGiftGroups(string page, string pageSize)
        {
            int total = 0;
            string message = string.Empty;
            List<GiftGroupModel> giftGroupModels = new List<GiftGroupModel>();
            bool success = false;
            try
            {
                var giftGroups = GetEntities<GiftGroup>(page, pageSize, out total, GetQueryCondition());

                foreach (var giftG in giftGroups)
                {
                    giftGroupModels.Add(new GiftGroupModel(giftG as GiftGroup));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = giftGroupModels.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveGiftGroups(List<GiftGroupModel> giftGroups)
        {
            return SaveEntities<GiftGroup>(giftGroups);
        }

        [HttpPost]
        public JsonResult NewGiftGroup()
        {

            try
            {
                GiftGroup giftG = AddEntity<GiftGroup>(new GiftGroup { Name = "默认礼物组" });
                return Json(new { Success = true, Model = new GiftGroupModel(giftG), Message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = false, Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}