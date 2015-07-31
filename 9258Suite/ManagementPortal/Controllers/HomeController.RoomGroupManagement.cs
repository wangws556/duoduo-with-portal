using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Exceptions;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Json;
using YoYoJson = YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        public ActionResult RoomGroupManagement()
        {
            return PartialView("PartialViews/RoomGroupManagement", new YoYoJson.JsonModel());
        }

        public JsonResult GetRoomGroups(string page, string pageSize)
        {
            int total = 0;
            string message = string.Empty;
            bool success = false;
            List<RoomGroupModel> roomGroupModels = new List<RoomGroupModel>();
            try
            {
                var roomGroups = GetEntities<RoomGroup>(page, pageSize, out total, GetQueryCondition());

                foreach (var roomG in roomGroups)
                {
                    roomGroupModels.Add(new RoomGroupModel(roomG as RoomGroup));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = roomGroupModels.ToArray(), EmptyGroup = new RoomGroupModel { Id = -1, Name = "无" }, Total = total,Message = message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowRooms()
        {
            return PartialView("PartialViews/RoomGroupManagement", new YoYoJson.JsonModel());
        }

        [HttpPost]
        public JsonResult SaveRoomGroups(List<RoomGroupModel> roomGroups)
        {
            return SaveEntities<RoomGroup>(roomGroups); 
        }

        [HttpPost]
        public JsonResult NewRoomGroup()
        {
            try
            {
                RoomGroup roomG = AddEntity<RoomGroup>(new RoomGroup { Name = "默认房间组" });
                return Json(new {Success = true,Model = new RoomGroupModel(roomG),ChatMessage = string.Empty}, JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = false, Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}