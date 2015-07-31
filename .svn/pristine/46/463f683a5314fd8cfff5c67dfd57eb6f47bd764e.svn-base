using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using YoYoStudio.Common.Web;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.Exceptions;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoJson = YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public class BaseController : Controller
    {
        [AllowAnonymous]
        public JsonResult IsAuthenticated()
        {
            bool success = false;
            IPrincipal principal = HttpContext.User;
            if (principal != null)
            {
                success = principal.Identity.IsAuthenticated;
            }
            return Json(new { Success = success }, JsonRequestBehavior.AllowGet);
        }

        protected bool GetToken(out int userId, out string token)
        {
            userId = -1;
            token = string.Empty;
            IPrincipal principal = HttpContext.User;
            if (principal != null)
            {
                TokenIdentity identity = principal.Identity as TokenIdentity;
                if (identity != null && identity.IsAuthenticated)
                {
                    token = identity.Token;
                    if (int.TryParse(identity.Name, out userId))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #region Get Image

        public static System.Drawing.Bitmap NotFound = new System.Drawing.Bitmap(System.Web.HttpContext.Current.Server.MapPath(@"~/Images/NotFound.png"));

        public byte[] GetBytes(System.Drawing.Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Position = 0;
            return ms.ToArray();
        }

        public FileContentResult GetImage(int id)
        {
            if (id > 0)
            {
                DSClient client = new DSClient(Models.Const.ApplicationId);
                int userId;
                string token;
                if (GetToken(out userId, out token))
                {
                    try
                    {
                        Image img = client.GetImage(userId, token, id);
                        return File(img.TheImage, "image/jpeg");
                    }
                    catch
                    {

                    }
                }
            }
            return File(GetBytes(NotFound), "image/jpge");
        }

        #endregion

        #region Entity

        public ModelEntity GetEntity(string typeString, int[] ids)
        {
            string lower = typeString.ToLower();
            if (lower == typeof(Application).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<Application>(ids);
            }
            else if (lower == typeof(Role).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<Role>(ids);
            }
            else if (lower == typeof(Command).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<Command>(ids);
            }
            else if (lower == typeof(RoleCommandView).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<RoleCommandView>(ids);
            }
            else if (lower == typeof(User).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<User>(ids);
            }
            else if (lower == typeof(UserApplicationInfo).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<UserApplicationInfo>(ids);
            }
            else if (lower == typeof(RoomGroup).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<RoomGroup>(ids);
            }
            else if (lower == typeof(Room).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<Room>(ids);
            }
            else if (lower == typeof(GiftGroup).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<GiftGroup>(ids);
            }
            else if (lower == typeof(Gift).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<Gift>(ids);
            }
            else if (lower == typeof(DepositHistory).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<DepositHistory>(ids);
            }
            else if (lower == typeof(ExchangeHistory).ToString().ToLower().Split('.').Last())
            {
                return GetEntity<ExchangeHistory>(ids);
            }
            return null;
        }

        public IEnumerable<ModelEntity> GetEntities<T>(string page, string pageSize, out int total, string condition) where T : ModelEntity
        {
            IEnumerable<ModelEntity> result = new List<ModelEntity>();
            total = 0;
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            if (GetToken(out userId, out token))
            {
                int start = -1, count = -1;
                int p = 0, s = 0;
                if (int.TryParse(page, out p) && int.TryParse(pageSize, out s))
                {
                    start = (p - 1) * s + 1;
                    count = s;
                }
                var t = typeof(T);
                if (t == typeof(Application))
                {
                    result = client.GetApplications(userId, token, condition, start, count).AsEnumerable<ModelEntity>();
                    total = client.GetApplicationCount(userId, token, condition);
                }
                else if (t == typeof(Role))
                {
                    result = client.GetRoles(userId, token, condition, start, count).AsEnumerable<ModelEntity>();
                    total = client.GetRoleCount(userId, token, condition);
                }
                else if (t == typeof(ImageWithoutBody))
                {
                    result = client.GetImages(userId, token, condition, start, count).AsEnumerable<ModelEntity>();
                    total = client.GetImageCount(userId, token, condition);
                }
                else if (t == typeof(Command))
                {
                    result = client.GetCommands(userId, token, condition, start, count).AsEnumerable<ModelEntity>();
                    total = client.GetCommandCount(userId, token, condition);
                }
                else if (t == typeof(RoleCommand))
                {
                    result = client.GetRoleCommands(userId, token, condition, start, count).AsEnumerable<ModelEntity>();
                    total = client.GetRoleCommandCount(userId, token, condition);
                }
                else if (t == typeof(RoleCommandView))
                {
                    result = client.GetRoleCommandViews(userId, token, condition, start, count).AsEnumerable<ModelEntity>();
                    total = client.GetRoleCommandViewCount(userId, token, condition);
                }
                else if (t == typeof(UserIdList))
                {
                    result = client.GetUserIdLists(userId, token, BuiltIns.AllApplication.Id,condition, start, count).AsEnumerable<ModelEntity>();
                    total = client.GetUserIdListCount(userId, token, BuiltIns.AllApplication.Id, condition);
                }
                else if (t == typeof(UserApplicationInfo))
                {
                    result = client.GetUserInfos(userId, token, condition, start, count);
                    total = client.GetUserInfoCount(userId, token, condition);
                }
                else if (t == typeof(User))
                {
                    result = client.GetUsers(userId, token, condition, start, count);
                    total = client.GetUserCount(userId, token, condition);
                }
                else if (t == typeof(RoomGroup))
                {
                    result = client.GetRoomGroups(userId, token, condition, start, count);
                    total = client.GetRoomGroupCount(userId, token, condition);
                }
                else if (t == typeof(Room))
                {
                    result = client.GetRooms(userId, token, condition, start, count);
                    total = client.GetRoomCount(userId, token, condition);
                }
                else if (t == typeof(GiftGroup))
                {
                    result = client.GetGiftGroups(userId, token, condition, start, count);
                    total = client.GetGiftGroupCount(userId, token, condition);
                }
                else if (t == typeof(Gift))
                {
                    result = client.GetGifts(userId, token, condition, start, count);
                    total = client.GetGiftCount(userId, token, condition);
                }
                else if (t == typeof(DepositHistory))
                {
                    result = client.GetDepositHistories(userId, token, condition, start, count);
                    total = client.GetDepositHistoryCount(userId, token, condition);
                }
                else if (t == typeof(ExchangeHistory))
                {
                    result = client.GetExchangeHistories(userId, token, condition,false, start, count);
                    total = client.GetExchangeHistoryCount(userId, token, condition,false);
                }
                else if (t == typeof(BlockType))
                {
                    result = client.GetBlockTypes(userId, token, condition, start, count);
                    total = client.GetBlockTypeCount(userId, token, condition);
                }
                else if (t == typeof(BlockList))
                {
                    result = client.GetBlockLists(userId, token, condition, start, count);
                    total = client.GetBlockListCount(userId, token, condition);
                }
                else if (t == typeof(BlockHistory))
                {
                    result = client.GetBlockHistory(userId, token, condition, start, count);
                    total = client.GetBlockHistoryCount(userId, token, condition);
                }
                else if (t == typeof(ExchangeRate))
                {
                    result = client.GetAllExchangeRate(userId, token, condition, start, count);
                    total = client.GetExchangeRateCount(userId, token, condition);
                }
                else if (t == typeof(RoomRole))
                {
                    result = client.GetRoomRoles(userId, token, condition, start, count);
                    total = client.GetRoomRoleCount(userId, token, condition);
                }
                else if (t == typeof(RoomConfig))
                {
                    result = client.GetRoomConfigs(userId, token, condition, start, count);
                    total = client.GetRoomConfigCount(userId, token, condition);
                }
            }
            return result;
        }

        public int GetEntityCount<T>(string condition)
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            if (GetToken(out userId, out token))
            {
                var t = typeof(T);
                if (t == typeof(Application))
                {
                    return client.GetApplicationCount(userId, token, condition);
                }
                else if (t == typeof(Role))
                {
                    return client.GetRoleCount(userId, token, condition);
                }
                else if (t == typeof(Image))
                {
                    return client.GetImageCount(userId, token, condition);
                }
                else if (t == typeof(Command))
                {
                    return client.GetCommandCount(userId, token, condition);
                }
                else if (t == typeof(RoleCommand))
                {
                    return client.GetRoleCommandCount(userId, token, condition);
                }
                else if (t == typeof(RoleCommandView))
                {
                    return client.GetRoleCommandViewCount(userId, token, condition);
                }
                else if (t == typeof(User))
                {
                    return client.GetUserCount(userId, token, condition);
                }
                else if (t == typeof(UserApplicationInfo))
                {
                    return client.GetUserInfoCount(userId, token, condition);
                }
                else if (t == typeof(RoomGroup))
                {
                    return client.GetRoomGroupCount(userId, token, condition);
                }
                else if (t == typeof(Room))
                {
                    return client.GetRoomCount(userId, token, condition);
                }
                else if (t == typeof(GiftGroup))
                {
                    return client.GetGiftGroupCount(userId, token, condition);
                }
                else if (t == typeof(Gift))
                {
                    return client.GetGiftCount(userId, token, condition);
                }
                else if (t == typeof(DepositHistory))
                {
                    return client.GetDepositHistoryCount(userId, token, condition);
                }
                else if (t == typeof(ExchangeHistory))
                {
                    return client.GetExchangeHistoryCount(userId, token, condition,false);
                }
                else if (t == typeof(ExchangeRate))
                {
                    return client.GetExchangeRateCount(userId,token,condition);
                }
                else if (t == typeof(RoomRole))
                {
                    return client.GetRoomRoleCount(userId, token, condition);
                }
            }
            return -1;
        }

        public T GetEntity<T>(int[] ids) where T : ModelEntity
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            if (GetToken(out userId, out token))
            {
                var t = typeof(T);
                if (t == typeof(Application))
                {
                    Application app = client.GetApplication(userId, token, ids[0]);
                    return app as T;
                }
                else if (t == typeof(Role))
                {
                    Role role = client.GetRole(userId, token, ids[0]);
                    return role as T;
                }
                else if (t == typeof(Image))
                {
                    Image image = client.GetImage(userId, token, ids[0]);
                    return image as T;
                }
                else if (t == typeof(Command))
                {
                    Command cmd = client.GetCommand(userId, token, ids[0]);
                    return cmd as T;
                }
                else if (t == typeof(RoleCommand))
                {
                    RoleCommand rc = client.GetRoleCommand(userId, token, ids[0]);
                    return rc as T;
                }
                else if (t == typeof(RoleCommandView))
                {
                    RoleCommandView rc = client.GetRoleCommandView(userId, token, ids[0]);
                    return rc as T;
                }
                else if (t == typeof(User))
                {
                    User user = client.GetUser(userId, token, ids[0]);
                    return user as T;
                }
                else if (t == typeof(UserApplicationInfo))
                {
                    UserApplicationInfo info = client.GetUserInfo(userId, token, ids[0], ids[1]);
                    return info as T;
                }
                else if (t == typeof(RoomGroup))
                {
                    RoomGroup roomGroup = client.GetRoomGroup(userId, token, ids[0]);
                    return roomGroup as T;
                }
                else if (t == typeof(Room))
                {
                    Room room = client.GetRoom(userId, token, ids[0]);
                    return room as T;
                }
                else if (t == typeof(RoomRole))
                {
                    RoomRole rRole = client.GetRoomRole(userId, token, ids[0], ids[1], ids[2]);
                    return rRole as T;
                }
                else if (t == typeof(GiftGroup))
                {
                    GiftGroup giftGroup = client.GetGiftGroup(userId, token, ids[0]);
                    return giftGroup as T;
                }
                else if (t == typeof(Gift))
                {
                    Gift gift = client.GetGift(userId, token, ids[0]);
                    return gift as T;
                }
                else if (t == typeof(BlockType))
                {
                    BlockType b = client.GetBlockType(userId, token, ids[0]);
                    return b as T;
                }
                else if (t == typeof(BlockList))
                {
                    BlockList b = client.GetBlockList(userId, token, ids[0]);
                    return b as T;
                }
                else if (t == typeof(RoomRole))
                {
                    RoomRole rr = client.GetRoomRole(userId, token, ids[0], ids[1], ids[2]);
                    return rr as T;
                }
                else if (t == typeof(RoomConfig))
                {
                    RoomConfig c = client.GetRoomConfig(userId, token, ids[0]);
                    return c as T;
                }
            }
            return null;
        }

        public void UpdateEntity(ModelEntity entity)
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            if (GetToken(out userId, out token))
            {
                UpdateEntity(client, userId, token, entity);
            }
        }

        public T AddEntity<T>(T entity) where T : ModelEntity
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            if (GetToken(out userId, out token))
            {
                return AddEntity<T>(client, userId, token, entity);
            }
            return null;
        }

        public void DeleteEntity(ModelEntity entity)
        {
            DSClient client = new DSClient(Models.Const.ApplicationId);
            int userId;
            string token;
            if (GetToken(out userId, out token))
            {
                DeleteEntity(client, userId, token, entity);
            }
        }
        
        protected void UpdateEntity(DSClient client, int userId, string token, ModelEntity entity)
        {
            if (entity is Application)
            {
                client.UpdateApplication(userId, token, entity as Application);
            }
            else if (entity is Role)
            {
                client.UpdateRole(userId, token, entity as Role);
            }
            else if (entity is Command)
            {
                client.UpdateCommand(userId, token, entity as Command);
            }
            else if (entity is RoleCommand)
            {
                client.UpdateRoleCommand(userId, token, entity as RoleCommand);
            }
            else if (entity is RoleCommandView)
            {
                client.UpdateRoleCommand(userId, token, (entity as RoleCommandView).GetRoleCommand());
            }
            else if (entity is Image)
            {
                client.UpdateImage(userId, token, entity as Image);
            }
            else if (entity is UserIdList)
            {
                client.UpdateUserIdList(userId, token, entity as UserIdList);
            }
            else if (entity is Room)
            {
                client.UpdateRoom(userId, token, entity as Room);
            }
            else if (entity is RoomGroup)
            {
                client.UpdateRoomGroup(userId, token, entity as RoomGroup);
            }
            else if (entity is Gift)
            {
                client.UpdateGift(userId, token, entity as Gift);
            }
            else if (entity is GiftGroup)
            {
                client.UpdateGiftGroup(userId, token, entity as GiftGroup);
            }
            else if (entity is User)
            {
                client.UpdateUser(userId, token, entity as User);
            }
            else if (entity is UserApplicationInfo)
            {
                client.UpdateUserInfo(userId, token, entity as UserApplicationInfo);
            }
            else if (entity is BlockType)
            {
                client.UpdateBlockType(userId, token, entity as BlockType);
            }
            else if (entity is BlockList)
            {
                client.UpdateBlockList(userId, token, entity as BlockList);
            }
            else if (entity is ExchangeRate)
            {
                client.UpdateExchangeRate(userId, token, entity as ExchangeRate);
            }
            else if (entity is RoomRole)
            {
                client.UpdateRoomRole(userId, token, entity as RoomRole);
            }
            else if (entity is RoomConfig)
            {
                client.UpdateRoomConfig(userId, token, entity as RoomConfig);
            }
        }

        protected T AddEntity<T>(DSClient client, int userId, string token, ModelEntity entity) where T : ModelEntity
        {
            if (entity is Application)
            {
                return client.AddApplication(userId, token, entity as Application) as T;
            }
            else if (entity is Role)
            {
                return client.AddRole(userId, token, entity as Role) as T;
            }
            else if (entity is Command)
            {
                return client.AddCommand(userId, token, entity as Command) as T;
            }
            else if (entity is RoleCommand)
            {
                return client.AddRoleCommand(userId, token, entity as RoleCommand) as T;
            }
            else if (entity is RoleCommandView)
            {
                return client.AddRoleCommand(userId, token, (entity as RoleCommandView).GetRoleCommand()) as T;
            }
            else if (entity is Image)
            {
                client.AddImage(userId, token, entity as Image);
                return entity as T;
            }
            else if (entity is UserIdList)
            {
                return client.AddUserIdList(userId, token, entity as UserIdList) as T;
            }
            else if (entity is Room)
            {
                return client.AddRoom(userId, token, entity as Room) as T;
            }
            else if (entity is RoomGroup)
            {
                return client.AddRoomGroup(userId, token, entity as RoomGroup) as T;
            }
            else if (entity is Gift)
            {
                return client.AddGift(userId, token, entity as Gift) as T;
            }
            else if (entity is GiftGroup)
            {
                return client.AddGiftGroup(userId, token, entity as GiftGroup) as T;
            }
            else if (entity is User)
            {
                return client.AddUser(userId, token, entity as User) as T;
            }
            else if (entity is UserApplicationInfo)
            {
                return client.AddUserInfo(userId, token, entity as UserApplicationInfo) as T;
            }
            else if (entity is BlockList)
            {
                return client.AddBlockList(userId, token, entity as BlockList) as T;
            }
            else if (entity is BlockType)
            {
                return client.AddBlockType(userId, token, entity as BlockType) as T;
            }
            else if (entity is ExchangeRate)
            {
                return client.AddExchangeRate(userId, token, entity as ExchangeRate) as T;
            }
            else if (entity is RoomRole)
            {
                return client.AddRoomRole(userId, token, entity as RoomRole) as T;
            }
            else if (entity is ExchangeHistory)
            {
                return client.AddExchangeHistory(userId, token, entity as ExchangeHistory) as T;
            }
            else if (entity is RoomConfig)
            {
                return client.AddRoomConfig(userId, token, entity as RoomConfig) as T;
            }
            return null;
        }

        protected void DeleteEntity(DSClient client, int userId, string token, ModelEntity entity)
        {
            if (entity is Application)
            {
                client.DeleteApplication(userId, token, (entity as Application).Id);
            }
            else if (entity is Role)
            {
                client.DeleteRole(userId, token, (entity as Role).Id);
            }
            else if (entity is Command)
            {
                client.DeleteCommand(userId, token, (entity as Command).Id);
            }
            else if (entity is RoleCommand)
            {
                client.DeleteRoleCommand(userId, token, (entity as RoleCommand).Id);
            }
            else if (entity is RoleCommandView)
            {
                client.DeleteRoleCommand(userId, token, (entity as RoleCommandView).Id);
            }
            else if (entity is Image)
            {
                client.DeleteImage(userId, token, (entity as Image).Id);
            }
            else if (entity is UserIdList)
            {
                client.DeleteUserIdList(userId, token, (entity as UserIdList).Application_Id, (entity as UserIdList).User_Id);
            }
            else if (entity is User)
            {
                client.DeleteUser(userId, token, (entity as User).Id);
            }
            else if (entity is UserApplicationInfo)
            {
                //delete the user instead
                //client.DeleteUserInfo(userId, token, (entity as UserApplicationInfo).User_Id, (entity as UserApplicationInfo).Application_Id);
                client.DeleteUser(userId, token, (entity as UserApplicationInfo).User_Id);
            }
            else if (entity is Room)
            {
                client.DeleteRoom(userId, token, (entity as Room).Id);
            }
            else if (entity is RoomGroup)
            {
                client.DeleteRoomGroup(userId, token, (entity as RoomGroup).Id);
            }
            else if (entity is Gift)
            {
                client.DeleteGift(userId, token, (entity as Gift).Id);
            }
            else if (entity is GiftGroup)
            {
                client.DeleteGiftGroup(userId, token, (entity as GiftGroup).Id);
            }
            else if (entity is DepositHistory)
            {
                client.DeleteDepositHistory(userId, token, (entity as DepositHistory).Id);
            }
            else if (entity is ExchangeHistory)
            {
                client.DeleteExchangeHistory(userId, token, (entity as ExchangeHistory).Id);
            }
            else if (entity is BlockList)
            {
                client.DeleteBlockList(userId, token, (entity as BlockList).Id);
            }
            else if (entity is BlockType)
            {
                client.DeleteBlockType(userId, token, (entity as BlockType).Id);
            }
            else if (entity is ExchangeRate)
            {
                client.DeleteExchangeRate(userId, token, (entity as ExchangeRate).Id);
            }
            else if (entity is RoomConfig)
            {
                client.DeleteRoomConfig(userId, token, (entity as RoomConfig).Id);
            }
        }

        protected JsonResult SaveEntities<T>(IEnumerable<YoYoJson.JsonModel> models) where T : ModelEntity
        {
            string result = string.Empty;
            bool succeed = false;
            if (models != null)
            {
                DSClient client = new DSClient(Models.Const.ApplicationId);
                int userId;
                string token;
                if (GetToken(out userId, out token))
                {
                    try
                    {
                        foreach (YoYoJson.JsonModel model in models)
                        {
                            T entity = model.GetConcretModelEntity<T>();
                            if (entity != null)
                            {
                                switch (model.GetEntityState())
                                {
                                    case PersistentState.Changed:
                                        UpdateEntity(client, userId, token, entity);
                                        break;
                                    case PersistentState.Added:
                                        UpdateEntity(client, userId, token, entity);
                                        break;
                                    case PersistentState.Deleted:
                                        DeleteEntity(client, userId, token, entity);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        succeed = true;
                    }
                    catch (DatabaseException error)
                    {
                        result = error.Message;
                    }
                }
            }
            return Json(new { Success = succeed, Message = result });
        }

        #endregion
    }
}
