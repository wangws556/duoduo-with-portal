using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.Exceptions;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;
using YoYoJson = YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        public ActionResult BlockTypeManagement()
        {
            return PartialView("PartialViews/BlockTypeManagement", new YoYoJson.JsonModel());
        }

        public JsonResult GetBlockTypes(string page, string pageSize)
        {
            int total = 0;
            string message = string.Empty;
            List<BlockTypeModel> models = null;
            bool success = false;
            try
            {
                var blockTypeModels = GetEntities<BlockType>(page, pageSize, out total, GetQueryCondition());
                models = new List<BlockTypeModel>();
                foreach (var blockType in blockTypeModels)
                {
                    models.Add(new BlockTypeModel(blockType as BlockType));
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
        public JsonResult NewBlockType()
        {
            try
            {
                BlockType blockType = AddEntity<BlockType>(new BlockType { Name = "默认禁封类型", Days = 3 });

                return Json(new { Success = true, Model = new BlockTypeModel(blockType), Message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            { 
                return Json(new {Success = true, Message = exception.Message},JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveBlockType(List<BlockTypeModel> blockTypes)
        {
            return SaveEntities<BlockType>(blockTypes);
        }
    }
}