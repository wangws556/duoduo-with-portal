using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Exceptions;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;
using YoYoJson = YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        #region Gift
        public ActionResult GiftManagement()
        {
            return PartialView("PartialViews/GiftManagement", new YoYoJson.JsonModel());
        }

        public JsonResult GetGifts(string page, string pageSize)
        {
            int total = 0;
            string message = string.Empty;
            bool success = false;
            List<GiftModel> giftModels = new List<GiftModel>();
            try
            {
                var gifts = GetEntities<Gift>(page, pageSize, out total, GetQueryCondition());
                foreach (var gift in gifts)
                {
                    giftModels.Add(new GiftModel(gift as Gift));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = giftModels.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveGifts(List<GiftModel> gifts)
        {
            return SaveEntities<Gift>(gifts);
        }

        [HttpPost]
        public JsonResult NewGift()
        {
            try
            {
                Gift gift = AddEntity<Gift>(new Gift { Name = "默认礼物", Unit = "个" });
                return Json(new {Success = true, Model = new GiftModel(gift), Message = string.Empty}, JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = false, Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Gift receive/give  Inquiry

        public ActionResult GiftInOutInquiryManagement()
        {
            return PartialView("PartialViews/GiftInOutInquiryManagement", new YoYoJson.JsonModel());
        }

        public JsonResult GetGiftsInOutHistories(string page, string pageSize)
        {
            int total = 0;
            List<GiftInOutHistoryModel> models = new List<GiftInOutHistoryModel>();
              string message = string.Empty;
            bool success = false;
            try
            {
                var histories = GetEntities<GiftInOutHistory>(page, pageSize, out total, GetQueryCondition());
                
                foreach (var h in histories)
                {
                    models.Add(new GiftInOutHistoryModel(h as GiftInOutHistory));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = models.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}