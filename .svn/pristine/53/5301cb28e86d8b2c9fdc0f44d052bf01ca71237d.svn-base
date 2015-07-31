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
using YoYoJson = YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        public ActionResult ApplicationManagement()
        {
            var result = PartialView("PartialViews/ApplicationManagement", new YoYoStudio.Model.Json.JsonModel());
            return result;
        }

        [HttpPost]
        public JsonResult SaveApplications(List<ApplicationModel> apps)
        {
            return SaveEntities<Application>(apps);            
        }

		private JsonResult GetApplicationsForCommand(int cmdid, bool includeAll = false)
		{
			int userId = -1;
			string token = string.Empty;
            bool success = false;
            string message = string.Empty;
            List<ApplicationModel> models = null;
			if (GetToken(out userId, out token))
			{
                try
                {
                    DSClient client = new DSClient(Models.Const.ApplicationId);
                    var apps = client.GetApplicationsForCommand(userId, token, cmdid);
                    models = new List<ApplicationModel>();
                    if (apps != null && apps.Count() > 0)
                    {
                        foreach (var app in apps)
                        {
                            Application application = app as Application;
                            if (!includeAll)
                            {
                                if (BuiltIns.ExcludeApplicationIds.Contains(application.Id))
                                {
                                    continue;
                                }
                            }
                            models.Add(new ApplicationModel(application));
                        }
                        success = true;
                        
                    }
                }
                catch (DatabaseException exception)
                {
                    message = exception.Message;
                }
                return Json(new { Success = success, Rows = models.ToArray(), Message = message }, JsonRequestBehavior.AllowGet);
			}
            return null;
		}

        public JsonResult GetApplications(string page, string pageSize, bool includeAll = false)
        {
            int total = 0;
            bool success = false;
            string message = string.Empty;
            List<ApplicationModel> models = new List<ApplicationModel>();
            try
            {
                var apps = GetEntities<Application>(page, pageSize, out total, "");
                
                if (apps != null && apps.Count() > 0)
                {
                    foreach (var app in apps)
                    {
                        Application application = app as Application;
                        if (!includeAll)
                        {
                            if (BuiltIns.ExcludeApplicationIds.Contains(application.Id))
                            {
                                total--;
                                continue;
                            }
                        }
                        models.Add(new ApplicationModel(application));
                    }
                    success = true;
                   
                }
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new { Success = success, Rows = models.ToArray(), Status = YoYoJson.JsonConst.GetStatusArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewApplication()
        {
            bool success = false;
            string message = string.Empty;
            Application app = null;
            try
            {
                app = AddEntity<Application>(new Application { Name = "默认应用程序" });
                success = true;
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }

            return Json(new {Success = success, Model = new ApplicationModel(app), Message = message}, JsonRequestBehavior.AllowGet);
        }
    }
}