using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult ExchangeRateManagement()
        {
            return PartialView("PartialViews/ExchangeRateManagement", new YoYoJson.JsonModel());
        }

        public JsonResult GetAllExchangeRate(string page, string pageSize,bool latest = false,int appId = -1)
        {
            int total = 0;
            List<ExchangeRateModel> models = new List<ExchangeRateModel>();
            try
            {
                var exchangeRateModels = GetEntities<ExchangeRate>(page, pageSize, out total, GetQueryCondition());
                if (latest)
                {
                    ExchangeRateModel eModel = null;
                    foreach (var eRate in exchangeRateModels)
                    {
                        eModel = new ExchangeRateModel(eRate as ExchangeRate);
                        if (DateTime.Compare(Convert.ToDateTime(eModel.ValidTime), DateTime.Now) > 0)
                        {
                            if (appId == -1 || appId == eModel.Application_Id)
                                return Json(new { Success = true,Model = eModel, Total = 1,Message = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                else
                {
                    foreach (var eRate in exchangeRateModels)
                    {
                        ExchangeRateModel eModel = new ExchangeRateModel(eRate as ExchangeRate);
                        if (appId == -1 || appId == eModel.Application_Id)
                            models.Add(new ExchangeRateModel(eRate as ExchangeRate));
                    }
                }

                return Json(new { Success = true, Rows = models.ToArray(), Total = total, Message = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = false, Rows = models.ToArray(), Total = total, Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult NewExchangeRate()
        {
            string message = string.Empty;
            bool result = false;
            ExchangeRate eRate = null;
            try
            {
                eRate = AddEntity<ExchangeRate>(new ExchangeRate { Id = -1, ScoreToMoney = 80, ScoreToCache = 40, MoneyToCache = 60, ValidTime = DateTime.Now.AddDays(5), Application_Id = BuiltIns.AllApplication.Id });
                result = true;
            }
            catch (DatabaseException de)
            {
                message = de.Message;
            }
            return Json(new { Success = result, Model = new ExchangeRateModel(eRate), Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveExchangeRate(List<ExchangeRateModel> models)
        {
            for (int i = 0; i < models.Count; i++ )
            {
                if (models[i].ValidTime == null)
                    models[i].ValidTime = DateTime.Now.ToShortDateString();
            }
            return SaveEntities<ExchangeRate>(models);
        }
		public JsonResult GetExchangeRateCommandApplications(bool includeAll = false)
		{
            return GetApplicationsForCommand(BuiltIns.DefineExchangeRateCommand.Id, includeAll);
		}
    }
}