using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Exceptions;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;
using YoYoJson = YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        #region Room management

        public ActionResult RoomManagement()
        {
            return PartialView("PartialViews/RoomManagement", new YoYoJson.JsonModel());
        }

        public JsonResult GetRooms(string page, string pageSize)
        {
            int total = 0;
            string message = string.Empty;
            bool success = false;
            List<RoomModel> rModels = new List<RoomModel>();
            try
            {
                var rooms = GetEntities<Room>(page, pageSize, out total, GetQueryCondition());

                foreach (var room in rooms)
                {
                    Room r = room as Room;
                    RoomModel rM = new RoomModel(r);
                    rModels.Add(rM);
                    int t;
                    var roomRoles = GetEntities<RoomRole>(page, pageSize, out  t, "");
                    rM.RoomRoles = new List<RoomRoleModel>();
                    foreach (var rR in roomRoles)
                    {
                        rM.RoomRoles.Add(new RoomRoleModel(rR as RoomRole));
                    }
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = rModels.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoomConfigs(int roomId)
        {
            return null;
        }

        public JsonResult GetRoom(int roomId)
        {
            try
            {
                Room r = GetEntity<Room>(new int[] { roomId });
                return Json(new {Success = true,Rows = new Room[]{r},Message = string.Empty},JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = false, Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAgents(string page, string pageSize)
        {
            List<UserModel> users = new List<UserModel>();
            int total = 0;
            string message = string.Empty;
            bool success = false;
            try
            {
                var userAppInfos = GetEntities<UserApplicationInfo>(page, pageSize, out total, "[Role_Id] = " + BuiltIns.AgentRole.Id + "AND [Application_Id] = " + BuiltIns._9258ChatApplication.Id);
                foreach (var userAppInfo in userAppInfos)
                {
                    UserApplicationInfo user = userAppInfo as UserApplicationInfo;
                    users.Add(new UserModel(GetEntity<User>(new int[] { user.User_Id })));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = users.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetRoomRole()
        {
            string token = string.Empty;
            string message = "";
                string roomId = Request.Form["roomId"];
                string roomAdminId = Request.Form["roomAdminId"];
                string roomDirId = Request.Form["roomDirId"];
                if (roomAdminId != "" && roomDirId != null)
                {
                    try
                    {
                        //AddEntity<RoomRole>(new RoomRole{Role_Id = int.Parse(roomAdminId), Room_Id = int.Parse(roomId), User_Id
                        //client.AddRoomRole(userid, token, new List<RoomRole>{
                        //    GetEntity<RoomRole>(new int[] { int.Parse(roomId), int.Parse(roomAdminId), BuiltIns.RoomAdministratorRole.Id }),
                        //    GetEntity<RoomRole>(new int[] { int.Parse(roomId), int.Parse(roomDirId), BuiltIns.RoomDirectorRole.Id })});
                        //result = true;
                    }
                    catch(Exception ex)
                    {
                        message = ex.Message;
                    }
                }
            return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRoomRole()
        {
            int userid = -1;
            string token = string.Empty;
            string message = "";
            bool result = false;
            if (GetToken(out userid, out token))
            {
                string roomId = Request.Form["roomId"];
                string roomAdminId = Request.Form["roomAdminId"];
                string roomDirId = Request.Form["roomDirId"];
                DSClient client = new DSClient(Models.Const.ApplicationId);
                try
                {
                    client.DeleteRoomRole(userid, token, int.Parse(roomId), int.Parse(roomAdminId), BuiltIns._9258RoomAdministratorRole.Id);
                    client.DeleteRoomRole(userid, token, int.Parse(roomId), int.Parse(roomDirId), BuiltIns._9258RoomDirectorRole.Id);
                    result = true;
                }
                catch (DatabaseException ex)
                {
                    message = ex.Message;
                }
            }
            return Json(new { Success = result, Message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AssignRooms()
        {
            int userid = -1;
            string token = string.Empty;
            string message = "";
            bool result = false;
            if (GetToken(out userid, out token))
            {
                string startId = Request.Form["startId"];
                string endId = Request.Form["endId"];
                string agentId = Request.Form["agentId"];

                DSClient client = new DSClient(Models.Const.ApplicationId);
                try
                {
                    client.AssignRoomsToAgent(userid, token, int.Parse(startId), int.Parse(endId), int.Parse(agentId));
                    result = true;
                }
                catch (DatabaseException ex)
                {
                    message = ex.Message;
                }
            }
            return Json(new { Success = result, Message = message }, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public JsonResult SaveRooms(List<RoomModel> rooms)
        {
            //need to adjust the HostUser_id and AgentUser_Id because the type of the column in database
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].HostUser_Id == 0)
                    rooms[i].HostUser_Id = null;
                if (rooms[i].AgentUser_Id == 0)
                    rooms[i].AgentUser_Id = null;
            }
            return SaveEntities<Room>(rooms);
        }
        [HttpPost]
        public JsonResult NewRoom()
        {
            try
            {
                Room room = AddEntity<Room>(new Room { Name = "默认房间" });
                return Json(new { Success = true, Model = new RoomModel(room), Message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = false,  Message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

    }
}