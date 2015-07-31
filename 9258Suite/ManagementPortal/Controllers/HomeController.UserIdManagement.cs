using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using YoYoStudio.Common;
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
        public ActionResult UserIdManagement()
        {
            return PartialView("PartialViews/UserIdManagement", new YoYoJson.JsonModel());
        }

        public JsonResult GetUserIdLists(string page, string pageSize)
        {
            int total = 0;
            List<UserIdModel> models = new List<UserIdModel>();
            string message = string.Empty;
            bool success = false;
            try
            {
                var userIds = GetEntities<UserIdList>(page, pageSize, out total, GetQueryCondition());

                foreach (var userid in userIds)
                {
                    models.Add(new UserIdModel(userid as UserIdList));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = models.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveUserIdLists(List<UserIdModel> ids)
        {
            return SaveEntities<UserIdList>(ids);
        }

        [HttpPost]
        public JsonResult AssignAgentUserIds(string app, string start, string end, string agent)
        {
            int userId = -1;
            string token = "";
            bool result = false;
            string message = string.Empty;
            if (GetToken(out userId, out token))
            {
                int a, s, e, ag;
                if (int.TryParse(app, out a) && int.TryParse(start, out s) && int.TryParse(end, out e) && int.TryParse(agent, out ag))
                {
                    DSClient client = new DSClient(Models.Const.ApplicationId);
                    try
                    {
                        client.AssignAgentUserIds(userId, token, a, s, e, ag);
                        result = true;
                    }
                    catch (DatabaseException exception)
                    {
                        message = exception.Message;
                    }
                }
            }
            return Json(new { Success = result, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddUserIds(string start, string end, string app, string role, string owner, string password,bool isDirect)
        {
            int userId = -1;
            string token = "";
            bool result = false;
            string message = string.Empty;
            if (GetToken(out userId, out token))
            {
                int s,e,a,r,o;
                if (int.TryParse(start, out s) && int.TryParse(end, out e) && int.TryParse(app, out a) && int.TryParse(role, out r))
                {
                    if (s <= BuiltIns.UserDefinedUserStartId)
                    {
                        return Json(new { Success = false, Message="用户Id不能小于100！" }, JsonRequestBehavior.AllowGet);
                    }
                    o = -1;
                    int.TryParse(owner, out o);
                    DSClient client = new DSClient(Models.Const.ApplicationId);
                    try
                    {
                        client.AddUserIdLists(userId, token, s, e, userId, o, r, a, Utility.GetMD5String(password), isDirect);
                        result = true;
                    }
                    catch (DatabaseException exception)
                    {
                        message = exception.Message;
                    }
                }
            }
            
            return Json(new { Success = result,Message = message }, JsonRequestBehavior.AllowGet);
        }

		public JsonResult GetUserIdCommandApplications(bool includeAll = false)
		{
			return GetApplicationsForCommand(BuiltIns.DefineUserIdCommand.Id, includeAll);
		}
    }
}