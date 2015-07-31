using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.Common.Web;
using YoYoStudio.DataService.Client;
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
	[CookieAuthorize]
	public partial class HomeController : BaseController
	{
		public ActionResult Index()
		{            
			return View();
		}
        

        public JsonResult GetFunctionTree(string test)
        {            
            int userId;
            string token;
            string message = string.Empty;
            bool success = false;
            List<TreeNodeModel> nodes = new List<TreeNodeModel>();
            if (GetToken(out userId, out token))
            {
                try
                {
                    DSClient client = new DSClient(Models.Const.ApplicationId);
                    var commands = client.GetCommands(userId, token, "", -1, -1);
                    var myCommands = client.GetRoleCommandsForUser(userId, token, userId);
                    var applications = client.GetApplications(userId, token, "", -1, -1).Where(c => !BuiltIns.ExcludeApplicationIds.Contains(c.Id)).ToList();

                    var me = client.GetUser(userId, token, userId);

                    Session["Me"] = me;
                    Session["Commands"] = myCommands.Select(t => t.Command_Id);

                    nodes.Add(GetCommandNode(commands, myCommands));
                    nodes.Add(GetApplicationNode(applications, commands, myCommands));
                    nodes.Add(GetPersonalNode());
                    success = true;
                }
                catch (DatabaseException exception)
                {
                    message = exception.Message;
                }
            }
            return Json(new { Success = success, Rows = nodes.ToArray(),Message = message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadImage(string ownertype, int id, int? imageid)
        {
            if (!imageid.HasValue)
            {
                imageid = -1;
            }
            return RedirectToAction("Upload", "Image", new ImageModel(ownertype) { OwnerId = id, Id = imageid.Value,OldId=imageid.Value });
        }

        [HttpPost]
        public JsonResult SaveSetting(string key, string value)
        {
            Session[key] = value;
            return Json(new { success = true });
        }
        [HttpPost]
        public JsonResult GetSetting(string key)
        {
            return Json(new {success=true,value=Session[key]});
        }
    }
}
