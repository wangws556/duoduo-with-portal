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
        public ActionResult BackendCommandManagement()
        {
            return PartialView("PartialViews/BackendCommandManagement", new YoYoJson.CommandModel { CommandType = BuiltIns.BackendCommandType });
        }
        public ActionResult FrontendCommandManagement()
        {
            return PartialView("PartialViews/FrontendCommandManagement", new YoYoJson.CommandModel { CommandType = BuiltIns.FrontendCommandType });
        }
        public ActionResult UserCommandManagement()
        {
            return PartialView("PartialViews/UserCommandManagement", new YoYoJson.CommandModel { CommandType = BuiltIns.UserCommandType });
        }

        public JsonResult GetRoleCommands(string page, string pageSize, int cmdType, bool includeAll = false)
        {
            List<CommandModel> cmdMs = new List<CommandModel>();
            string message = string.Empty;
            bool success = false;
            int total = 0;
            string aId = Request.Form["aid"];
            string srId = Request.Form["srid"];
            int appId = -1, sroleId = -1, troleId = -1;
            bool good = true;
            if (!int.TryParse(aId, out appId) || !int.TryParse(srId, out sroleId))
            {
                good = false;
            }
            if (cmdType == BuiltIns.UserCommandType)
            {
                string trId = Request.Form["trid"];
                if (!int.TryParse(trId, out troleId))
                {
                    good = false;
                }
            }
            if (good)
            {
				string con = "([CommandType]&" + cmdType + ">0) AND ([Command_Application_Id]=1 OR [Command_Application_Id]=" + appId + ") AND [SourceRole_Id]=" + sroleId;
                if (cmdType == BuiltIns.UserCommandType)
                {
                    con += " AND [TargetRole_Id]=" + troleId;
                }
                try
                {
                    var cmds = GetEntities<Command>(null, null, out total, "([CommandType]&" + cmdType + ">0)");
                    var roleCommands = GetEntities<RoleCommandView>(page, pageSize, out total, con);
                    foreach (var cmd in cmds)
                    {
                        Command c = cmd as Command;
                        if (!includeAll)
                        {
                            if (BuiltIns.ExcludeCommandIds.Contains(c.Id))
                            {
                                total--;
                                continue;
                            }
                        }
                        CommandModel model = new CommandModel(c);
                        foreach (var rc in roleCommands)
                        {
                            RoleCommandView rcv = rc as RoleCommandView;
                            if (rcv.Command_Id == c.Id)
                            {
                                model.Enabled = true;
                                model.IsManagerCmd = rcv.IsManagerCommand?1:0;
                                break;
                            }
                        }
                        cmdMs.Add(model);
                    }
                    success = true;
                }
                catch (DatabaseException exception)
                {
                    message = exception.Message;
                }
            }
            return Json(new {Success = success, Rows = cmdMs.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveRoleCommands(List<CommandModel> cmds,int cmdType, int aid, int srid, string trid)
        {
            if (cmds == null)
            {
                cmds = new List<CommandModel>();
            }
            int tid = -1;
            if (cmdType == BuiltIns.UserCommandType)
            {
                if (!int.TryParse(trid, out tid))
                {
                    return null;
                }
            }
			string con = "([CommandType]&" + cmdType + ">0) AND ([Command_Application_Id]=1 OR [Command_Application_Id]=" + aid + ") AND [SourceRole_Id]=" + srid;
            if (cmdType == BuiltIns.UserCommandType)
            {
                con += " AND [TargetRole_Id]=" + tid;
            }
            int total = 0;
            string message = string.Empty;
            bool success = false;
            try
            {
                var roleCmds = GetEntities<RoleCommandView>(null, null, out total, con);
                foreach (var c in cmds)
                {
                    var roleCommand = roleCmds.FirstOrDefault(rc => (rc as RoleCommandView).Command_Id == c.Id) as RoleCommandView;
                    if (roleCommand == null)
                    {
                        AddEntity<RoleCommand>(new RoleCommand
                        {
                            Application_Id = aid,
                            SourceRole_Id = srid,
                            Command_Id = c.Id,
                            IsManagerCommand = Convert.ToBoolean(c.IsManagerCmd),
                            TargetRole_Id = (cmdType == BuiltIns.UserCommandType ? tid : BuiltIns.AllRole.Id)
                        });
                    }
                    else if (roleCommand.IsManagerCommand != Convert.ToBoolean(c.IsManagerCmd))
                    {
                        roleCommand.IsManagerCommand = Convert.ToBoolean(c.IsManagerCmd);
                        UpdateEntity(roleCommand);
                    }
                }
                foreach (var rc in roleCmds)
                {
                    RoleCommandView rcv = rc as RoleCommandView;
                    if (cmds.FirstOrDefault(c => c.Id == rcv.Command_Id) == null)
                    {
                        DeleteEntity(rcv);
                    }
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new { Success = success, Message = message},JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewRoleCommand()
        {
            try
            {
                RoleCommand rCmd = AddEntity<RoleCommand>(new RoleCommand { SourceRole_Id = BuiltIns.DefaultRole.Id, TargetRole_Id = BuiltIns.DefaultRole.Id, Command_Id = BuiltIns.DefaultUserCommand.Id, IsManagerCommand = false, IsBuiltIn = false });
                RoleCommandView view = new RoleCommandView(rCmd, BuiltIns.DefaultUserCommand);
                return Json(new {Success = true, Model = new RoleCommandModel(view),Message = string.Empty},JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = false,  Message = exception.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult NewBackendRoleCommand()
        {
            try
            {
                RoleCommand rCmd = AddEntity<RoleCommand>(new RoleCommand { SourceRole_Id = BuiltIns.DefaultRole.Id, TargetRole_Id = BuiltIns.AllRole.Id, Command_Id = BuiltIns.DefaultBackendCommand.Id, IsManagerCommand = false, IsBuiltIn = false });
                RoleCommandView view = new RoleCommandView(rCmd, BuiltIns.DefaultBackendCommand);
                return Json(new { Success = true, Model = new RoleCommandModel(view), Message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = true,  Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}