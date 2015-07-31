using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Exceptions;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;
using YoYoStudio.Resource;
using YoYoJson = YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        public ActionResult ExchangeManagement()
        {
            return PartialView("PartialViews/ExchangeManagement",new YoYoJson.JsonModel());
        }

        public JsonResult GetExchangeHistories(string page, string pageSize)
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            int total = 0;
            string message = string.Empty;
            bool success = false;
            List<ExchangeHistoryModel> models = new List<ExchangeHistoryModel>();
            if (GetToken(out userId, out token))
            {
                int start = -1, count = -1;
                int p = 0, s = 0;
                if (int.TryParse(page, out p) && int.TryParse(pageSize, out s))
                {
                    start = (p - 1) * s + 1;
                    count = s;
                }
                string condition = "";
                string appId = Request.Form["aid"];
                string exchangeCache = Request.Form["exchangeCache"];
                int id = -1;
                if (int.TryParse(appId, out id))
                {
                    condition = "([Application_Id] =" + id + ")";
                    try
                    {
                        List<ExchangeHistory> exchangeHistories = client.GetExchangeHistories(userId, token, condition, Convert.ToBoolean(exchangeCache), start, count);
                        total = client.GetExchangeHistoryCount(userId, token, "", Convert.ToBoolean(exchangeCache));
                        exchangeHistories.ForEach(h =>
                        {
                            models.Add(new ExchangeHistoryModel(h));
                        });
                        success = true;
                    }
                    catch (DatabaseException exception)
                    {
                        message = exception.Message;
                    }
                }
            }
            return Json(new {Success = success, Rows = models.ToArray(), Total = total,Message = message}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExchangeHistoryForSettlement(string page, string pageSize)
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            string message = string.Empty;
            bool success = false;
            List<ExchangeHistoryModel> models = new List<ExchangeHistoryModel>();
            if (GetToken(out userId, out token))
            {
                int start = -1, count = -1;
                int p = 0, s = 0;
                if (int.TryParse(page, out p) && int.TryParse(pageSize, out s))
                {
                    start = (p - 1) * s + 1;
                    count = s;
                }
                try
                {
                    List<ExchangeHistory> exchangeHistories = client.GetExchangeHistoryForSettlement(userId, token, "Status <> " + (int)RequestStatus.None, start, count);
                    foreach (var history in exchangeHistories)
                    {
                        models.Add(new ExchangeHistoryModel(history));
                    }
                    success = true;
                }
                catch (DatabaseException exception)
                {
                    message = exception.Message;
                }
            }
            return Json(new {Success = success, Rows = models.ToArray(), Message = message}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewExchangeHistory(string score,string money, string cache,string appId)
        { 
            int userId;
            string token;
            ExchangeHistory eHistory = null;
             string message = string.Empty;
            bool success = false;
            if(GetToken(out userId,out token))
            {
                try
                {
                    eHistory = AddEntity<ExchangeHistory>(new ExchangeHistory
                    {
                        User_Id = userId,
                        ApplyTime = DateTime.Now,
                        Application_Id = int.Parse(appId),
                        Score = int.Parse(score),
                        Money = int.Parse(money),
                        SettlementTime = DateTime.Now.AddDays(10),
                        Cache = int.Parse(cache),
                        OptUser_Id = BuiltIns.Administrator.Id,
                        Status = (int)RequestStatus.Submitted
                    });
                    success = true;
                }
                catch (DatabaseException excpetion)
                {
                    message = excpetion.Message;
                }
            }
            return Json(new { Success = success, Model = new ExchangeHistoryModel(eHistory), Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CancelExchange(List<ExchangeHistoryModel> models)
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            string message = string.Empty;
            bool success = false;
            if (GetToken(out userId, out token))
            {
                List<ExchangeHistory> histories = new List<ExchangeHistory>();
                foreach(ExchangeHistoryModel model in models)
                {
                    if(model.Status == (int)RequestStatus.Submitted)
                        histories.Add(model.GetConcretModelEntity<ExchangeHistory>());
                }
                if (histories.Count > 0 && histories.Count == models.Count)
                {
                    try
                    {
                        client.CancelExchangeCache(userId, token, histories);
                        success = true;
                    }
                    catch (DatabaseException exception)
                    {
                        message = exception.Message;
                    }
                }
            }
            return Json(new {Success = success, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConfirmExchangeCache(List<ExchangeHistoryModel> models)
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            string message = string.Empty;
            bool success = false;
            if (GetToken(out userId, out token))
            {
                List<ExchangeHistory> histories = new List<ExchangeHistory>();
                foreach (ExchangeHistoryModel model in models)
                {
                    if (model.Status == (int)RequestStatus.Processed)
                        histories.Add(model.GetConcretModelEntity<ExchangeHistory>());
                }
                try
                {
                    client.ConfirmExchangeCache(userId, token, histories);
                    success = true;
                }
                catch (DatabaseException exception)
                {
                    message = exception.Message;
                }
            }
            return Json(new {Success = success, Message = message}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SettleExchangeCache(List<ExchangeHistoryModel> models)
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            string message = string.Empty;
            bool success = false;
            if (GetToken(out userId, out token))
            {
                List<ExchangeHistory> histories = new List<ExchangeHistory>();
                foreach (ExchangeHistoryModel model in models)
                {
                    if(model.Status == (int) RequestStatus.Submitted)
                        histories.Add(model.GetConcretModelEntity<ExchangeHistory>());
                }
                try
                {
                    client.SettleExchangeCache(userId, token, histories);
                    success = true;
                }
                catch (DatabaseException exception)
                {
                    message = exception.Message;
                }
            }
            return Json(new { Success = success, Message = message }, JsonRequestBehavior.AllowGet);
        }

		public JsonResult GetExchangeHistoryCommandApplications(bool includeAll = false)
		{
			return GetApplicationsForCommand(BuiltIns.QueryExchangeCommand.Id, includeAll);
		}
    }
}