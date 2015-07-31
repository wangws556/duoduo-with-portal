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
        public ActionResult RoleManagement()
        {
            return PartialView("PartialViews/RoleManagement", new YoYoJson.JsonModel());
        }

		public JsonResult GetRoleCommandApplications(bool includeAll=false)
		{
            return GetApplicationsForCommand(BuiltIns.DefineRoleCommand.Id, includeAll);
		}

        public JsonResult GetApplicationRoles(string page, string pageSize, bool includeAll = false)
        {
            string appId = Request.Form["aid"];
            int id = -1;
            if (int.TryParse(appId, out id))
            {
                return GetRoles(page, pageSize, includeAll, "([Application_Id]=1 or [Application_Id]=" + id + ")");
            }
            return null;
        }

        public JsonResult GetRoles(string page, string pageSize, bool includeAll = false)
        {
            return GetRoles(page, pageSize, includeAll, GetQueryCondition());
        }

        private JsonResult GetRoles(string page, string pageSize, bool includeAll, string condition)
        {
            List<RoleModel> roleModels = new List<RoleModel>();
            int total = 0;
            string message = string.Empty;
            bool success = false;
            try
            {
                var roles = GetEntities<Role>(page, pageSize, out total, condition);
                foreach (var r in roles)
                {
                    Role role = r as Role;
                    if (!includeAll)
                    {
                        if (BuiltIns.ExcludeRoleIds.Contains(role.Id))
                        {
                            total--;
                            continue;
                        }
                    }
                    roleModels.Add(new RoleModel(role));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = roleModels.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveRoles(List<RoleModel> roles)
        {
            return SaveEntities<Role>(roles);
        }
        [HttpPost]
        public JsonResult NewRole()
        {
            try
            {
                Role role = AddEntity<Role>(new Role { Name = "默认用户级别", Application_Id = BuiltIns.AllApplication.Id });
                return Json(new { Success = true, Model = new RoleModel(role), Message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = false, Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}