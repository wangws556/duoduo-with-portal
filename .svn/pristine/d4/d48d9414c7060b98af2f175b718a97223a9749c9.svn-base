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
        public ActionResult BlockManagement()
        {
            return PartialView("PartialViews/BlockManagement", new YoYoJson.JsonModel());
        }

        public JsonResult GetBlockList(string page, string pageSize)
        {
            int total = 0;
            List<BlockListModel> models = null;
            bool success = false;
            string message = string.Empty;
            try
            {
                var blockListModels = GetEntities<BlockList>(page, pageSize, out total, GetQueryCondition());
                models = new List<BlockListModel>();
                foreach (var blockList in blockListModels)
                {
                    models.Add(new BlockListModel(blockList as BlockList));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = models.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBlockHistory(string page, string pageSize)
        {
            int total = 0;
            bool success = false;
            string message = string.Empty;
            List<BlockHistoryModel> models = null;
            try
            {
                var blockHistoryModels = GetEntities<BlockHistory>(page, pageSize, out total, GetQueryCondition());
                models = new List<BlockHistoryModel>();
                foreach (var blockHistory in blockHistoryModels)
                {
                    models.Add(new BlockHistoryModel(blockHistory as BlockHistory));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
                 
            return Json(new {Success = success, Rows = models.ToArray(), Total = total,Message = message}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteBlockList(string blockListId, string blockUserId, string isBlock, string blockType)
        {
            string message = string.Empty;
            bool success = false;
            int id = -1;
            if(int.TryParse(blockListId,out id))
            {
                try
                {
                    DeleteEntity(new BlockList { Id = id });
                    success = true;
                }
                catch (DatabaseException ex)
                {
                    message = ex.Message;
                }
            }
            return Json(new{ Success = success, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewBlockList()
        {
            BlockList list = new BlockList { Application_Id = BuiltIns.AllApplication.Id, BlockType_Id = BuiltIns.BlockUserType.Id, Content = string.Empty };
            string message = string.Empty;
            bool success = false;
            try
            {
                AddEntity<BlockList>(list);
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success,Model = new BlockListModel(list), Message = message}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveBlockList(List<BlockListModel> blocks)
        {
            return SaveEntities<BlockList>(blocks);
        }
		public JsonResult GetBlockCommandApplications(bool includeAll = false)
		{
			return GetApplicationsForCommand(BuiltIns.BlockCommand.Id, includeAll);
		}
    }
}