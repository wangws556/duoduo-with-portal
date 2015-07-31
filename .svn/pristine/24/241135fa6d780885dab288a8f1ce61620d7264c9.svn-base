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
        public ActionResult CommandManagement()
        {
            var result = PartialView("PartialViews/CommandManagement", new YoYoJson.JsonModel());
            return result;
        }        

        public JsonResult GetCommands(string page, string pageSize, int cmdType,bool includeAll=false)
        {
            List<CommandModel> cmdMs = new List<CommandModel>();
            int total = 0;
            string message = string.Empty;
            bool success = false;
            string condition = GetQueryCondition();
            string con = "([CommandType]&" + cmdType + ">0)";
            con = string.IsNullOrEmpty(condition) ? con : (condition + " AND " + con);
            try
            {

                var cmds = GetEntities<Command>(page, pageSize, out total, con);
                if (cmds != null && cmds.Count() > 0)
                {
                    foreach (var c in cmds)
                    {
                        Command cmd = c as Command;
                        if (!includeAll)
                        {
                            if (BuiltIns.ExcludeCommandIds.Contains(cmd.Id))
                            {
                                total--;
                                continue;
                            }
                        }
                        cmdMs.Add(new CommandModel(cmd));
                    }
                    success = true;
                    return Json(new {Success = success, Rows = cmdMs.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new { Success = success, Message = message }, JsonRequestBehavior.AllowGet); ;
        }

        [HttpPost]
        public JsonResult SaveCommands(List<CommandModel> commands)
        {
            return SaveEntities<Command>(commands);
        }
        [HttpPost]
        public JsonResult NewCommand()
        {
            string message = string.Empty;
            bool success = false;
            Command cmd = null;
            try
            {
                cmd = AddEntity<Command>(new Command { Application_Id = BuiltIns.AllApplication.Id, Name = "默认权限", CommandType = BuiltIns.UserCommandType, IsBuiltIn = false });
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new{Success = success, Model = new CommandModel(cmd), Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMyCommands(int? appid)
        {
            int userId;
            string token;
            string message = string.Empty;
            bool success = false;
            if (GetToken(out userId, out token))
            {
                try
                {
                    DSClient client = new DSClient(Models.Const.ApplicationId);
                    var myCommands = client.GetRoleCommandsForUser(userId, token, userId);

                    bool hasAll = myCommands.FirstOrDefault(rc => rc.Application_Id == BuiltIns.AllApplication.Id && rc.Command_Id == BuiltIns.AllCommand.Id) != null;
                    if (!hasAll && appid.HasValue)
                    {
                        hasAll = myCommands.FirstOrDefault(rc => rc.Application_Id == appid.Value && rc.Command_Id == BuiltIns.AllCommand.Id) != null;
                    }
                    success = true;
                    if (hasAll)
                    {
                        return Json(new { Success = success, hasAll = true,Message = message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var ids = appid.HasValue ? myCommands.Where(rc => rc.Application_Id == appid.Value).Select(rc => rc.Command_Id) : myCommands.Select(rc => rc.Command_Id);
                        return Json(new {Success = success, hasAll = false, cmdIds = ids.ToArray(), Message = message }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (DatabaseException exception)
                {
                    message = exception.Message;
                }

            }
            return Json(new { Success = success,Message = message }, JsonRequestBehavior.AllowGet);
        }

		public JsonResult GetBackendCommandApplications(bool includeAll = false)
		{
			return GetApplicationsForCommand(BuiltIns.DefineBackendCommandCommand.Id, includeAll);
		}
		public JsonResult GetFrontendCommandApplications(bool includeAll = false)
		{
			return GetApplicationsForCommand(BuiltIns.DefineFrontendCommandCommand.Id, includeAll);
		}
		public JsonResult GetUserCommandCommandApplications(bool includeAll = false)
		{
			return GetApplicationsForCommand(BuiltIns.DefineUserCommandCommand.Id, includeAll);
		}
		public JsonResult GetCommandApplications(bool includeAll =false)
		{
			return GetApplicationsForCommand(BuiltIns.DefineCommandCommand.Id,includeAll);
		}
    }
}