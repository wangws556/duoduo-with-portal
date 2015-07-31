using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using YoYoStudio.Model.Core;
using YoYoStudio.Persistent;
using YoYoStudio.Common;
using System.Configuration;
using System.ServiceModel.Channels;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Common.ORM;
using System.Transactions;
using YoYoStudio.Exceptions;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using YoYoStudio.Common.Wcf;
using YoYoStudio.Model.Media;
using System.IO;

namespace YoYoStudio.DataService.Library
{
    [ServiceBehavior(AutomaticSessionShutdown = true, IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall, ConfigurationName = Const.DataServiceName)]
    public partial class DataService : WcfService, IDataService, IErrorHandler, IServiceBehavior
    {
        private IModelAccesser modelAccesser = new DirectModelAccesser();

        #region Private Methods

        private void CheckToken(int appid, int userId, string token)
        {
            if (userId == BuiltIns.ReadOnlyUser.Id)
            {
                return;
            }
            TokenCache tc = new TokenCache { User_Id = userId, Application_Id = appid };
            modelAccesser.Get(tc);
            if (tc.Loaded)
            {
                if (tc.Token == token)
                {
                    return;
                }
                throw new FaultException(Resource.Messages.InvalidToken);
            }
            else
            {
                tc.Application_Id = BuiltIns.AllApplication.Id;
                modelAccesser.Get(tc);
                if (tc.Loaded)
                {
                    if (tc.Token == token)
                    {
                        return;
                    }
                }
            }
            throw new FaultException(Resource.Messages.InvalidToken);
        }

        private void CheckCommand(int appid, int cmdTargetAppId, int userId, int cmdId, int targetRoleId)
        {
            if (HasCommand(appid, cmdTargetAppId, userId, cmdId, targetRoleId))
            {
                return;
            }
            throw new FaultException(Resource.Messages.NoPrivilege);
        }
        private void CheckRoleCommand(int appid, int userId, RoleCommand rc)
        {
            var cmd = new Command { Id = rc.Command_Id };
            int cmdIdToCheck = -1;
            modelAccesser.Get<Command>(cmd);
            switch (cmd.CommandType)
            {
                case BuiltIns.BackendCommandType:
                    cmdIdToCheck = BuiltIns.DefineBackendCommandCommand.Id;
                    break;
                case BuiltIns.FrontendCommandType:
                    cmdIdToCheck = BuiltIns.DefineFrontendCommandCommand.Id;
                    break;
                case BuiltIns.UserCommandType:
                    cmdIdToCheck = BuiltIns.DefineUserCommandCommand.Id;
                    break;
            }
            CheckCommand(appid, rc.Application_Id, userId, cmdIdToCheck, BuiltIns.AllRole.Id);
        }

        private string clientAddress = string.Empty;

        private string ClientAddress
        {
            get
            {
                if (string.IsNullOrEmpty(clientAddress))
                {
                    clientAddress = GetClientRemoteEndPoint().Address;
                }
                return clientAddress;
            }
        }

        private string GetUserToken(User user, string password, int appId)
        {
            TokenCache tc = new TokenCache { Application_Id = appId, User_Id = user.Id };
            modelAccesser.Get(tc);
            if (user.IsBuiltIn)
            {
                if (tc.Loaded)
                {
                    return tc.Token;
                }
            }
            string rawToken = user.Id.ToString() + ClientAddress + password + Guid.NewGuid().ToString();
            byte[] rawTokenBytes = System.Text.UTF8Encoding.Default.GetBytes(rawToken);
            string token = Convert.ToBase64String(rawTokenBytes);
            tc.Token = token;
            if (tc.Loaded)
            {
                modelAccesser.Update(tc);
            }
            else
            {
                modelAccesser.Add(tc);
            }
            return token;
        }

        #endregion

        [OperationBehavior]
        public string Login(int appid, int userId, string password)
        {
            try
            {
                string token = string.Empty;
                User user = new User { Id = userId };
                modelAccesser.Get<User>(user);
                if (user.Loaded)
                {
                    if ((user.Password == password) || (user.Password == Utility.GetMD5String(password)))
                    {
                        token = GetUserToken(user, password, appid);
                        logger.Debug("Login OK : AppId - " + appid + " , UserId - " + userId + " , Token - " + token);
                    }
                }
                logger.Debug("Exit Login : AppId - " + appid + " , UserId - " + userId);
                return token;
            }
            catch (Exception ex)
            {
                logger.Error("Login Error: AppId - " + appid + " , UserId - " + userId, ex);
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void LogOff(int appid, int userId)
        {
            TokenCache tc = new TokenCache { User_Id = userId, Application_Id = appid };
            modelAccesser.Delete(tc);
        }
        [OperationBehavior]
        public void KeepAlive()
        {
            System.Threading.Thread.Sleep(1);
        }

        #region Application

        [OperationBehavior]
        public List<Application> GetApplications(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<Application>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public List<Application> GetApplicationsForCommand(int appid, int userId, string token, int cmdid)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.ExecuteSPReader<Application>("GetApplicationsForCommand", new List<SPParameter>{
                new SPParameter{ Name="userid",Type = System.Data.DbType.Int32,Value = userId},
                new SPParameter{Name = "cmdid",Type = System.Data.DbType.Int32, Value = cmdid}
            });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }

        }

        [OperationBehavior]
        public Application AddApplication(int appid, int userId, string token, Application application)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.DefineApplicationCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add<Application>(application);
                return application;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void DeleteApplication(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.DefineApplicationCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete<Application>(new Application { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateApplication(int appid, int userId, string token, Application application)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.DefineApplicationCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update<Application>(application);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Application GetApplication(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                Application application = new Application { Id = id };
                modelAccesser.Get<Application>(application);
                return application.Loaded ? application : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetApplicationCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                return modelAccesser.GetCount<Application>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region Commands
        [OperationBehavior]
        public void DeleteCommand(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                Command cmd = new Command { Id = id };
                modelAccesser.Get<Command>(cmd);
                if (cmd.Loaded)
                {
                    CheckCommand(appid, cmd.Application_Id, userId, BuiltIns.DefineCommandCommand.Id, BuiltIns.AllRole.Id);
                    modelAccesser.Delete(cmd);
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateCommand(int appid, int userId, string token, Command command)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, command.Application_Id, userId, BuiltIns.DefineCommandCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update(command);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Command AddCommand(int appid, int userId, string token, Command command)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, command.Application_Id, userId, BuiltIns.DefineCommandCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(command);
                return command;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Command GetCommand(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                Command command = new Command { Id = id };
                modelAccesser.Get(command);
                return command.Loaded ? command : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<Command> GetCommands(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<Command>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetCommandCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                return modelAccesser.GetCount<Command>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region Role
        [OperationBehavior]
        public List<Role> GetRoles(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<Role>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void DeleteRole(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                Role r = new Role { Id = id };
                modelAccesser.Get<Role>(r);
                if (r.Loaded)
                {
                    CheckCommand(appid, r.Application_Id, userId, BuiltIns.DefineRoleCommand.Id, BuiltIns.AllRole.Id);
                    modelAccesser.Delete(r);
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateRole(int appid, int userId, string token, Role role)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, role.Application_Id, userId, BuiltIns.DefineRoleCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update(role);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Role AddRole(int appid, int userId, string token, Role role)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, role.Application_Id, userId, BuiltIns.DefineRoleCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(role);
                return role;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Role GetRole(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                Role role = new Role { Id = id };
                modelAccesser.Get(role);
                return role.Loaded ? role : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetRoleCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                return modelAccesser.GetCount<Role>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region RoleCommand

        [OperationBehavior]
        public void DeleteRoleCommand(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                RoleCommand rc = new RoleCommand { Id = id };
                modelAccesser.Get<RoleCommand>(rc);
                if (rc.Loaded)
                {
                    CheckRoleCommand(appid, userId, rc);
                    modelAccesser.Delete(new RoleCommand { Id = id });
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateRoleCommand(int appid, int userId, string token, RoleCommand roleCommand)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckRoleCommand(appid, userId, roleCommand);
                modelAccesser.Update(roleCommand);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public RoleCommand AddRoleCommand(int appid, int userId, string token, RoleCommand roleCommand)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckRoleCommand(appid, userId, roleCommand);
                modelAccesser.Add(roleCommand);
                return roleCommand;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public RoleCommand GetRoleCommand(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                RoleCommand rc = new RoleCommand { Id = id };
                modelAccesser.Get<RoleCommand>(rc);
                return rc.Loaded ? rc : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<RoleCommand> GetRoleCommands(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<RoleCommand>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetRoleCommandCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<RoleCommand>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<RoleCommandView> GetRoleCommandsForUser(int appid, int userId, string token, int uid)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.ExecuteSPReader<RoleCommandView>("GetRoleCommandsForUser", new List<SPParameter>{
                new SPParameter{Direction=System.Data.ParameterDirection.Input,Name="userid",Value=uid}
            });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public List<RoleCommandView> GetRoleCommandViews(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<RoleCommandView>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetRoleCommandViewCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<RoleCommandView>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public RoleCommandView GetRoleCommandView(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                RoleCommandView view = new RoleCommandView { Id = id };
                modelAccesser.Get<RoleCommandView>(view);
                return view.Loaded ? view : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }

        }

        [OperationBehavior]
        public bool HasCommand(int appid, int cmdTargetAppId, int userId, string token, int cmdId, int targetRoleId)
        {
            try
            {
                CheckToken(appid, userId, token);
                return HasCommand(appid, cmdTargetAppId, userId, cmdId, targetRoleId);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        private bool HasCommand(int currentAppId, int cmdTargetAppId, int userId, int cmdId, int targetRoleId)
        {
            if (currentAppId <= 0 || currentAppId == BuiltIns.AllApplication.Id)
            {
                return false;
            }
            //When calling from management portal, currentAppId is BuiltIns.BackendApplication.Id
            //when calling from other applications, currentAppId == cmdTargetAppId or BuiltIns.AllApplication.Id == currentAppId
            if (currentAppId == BuiltIns.BackendApplication.Id
                || currentAppId == cmdTargetAppId
                || cmdTargetAppId == BuiltIns.AllApplication.Id)
            {

                Command cmd = new Command { Id = cmdId };
                modelAccesser.Get<Command>(cmd);
                if (cmd.Loaded)
                {
                    switch (cmd.CommandType)
                    {
                        case BuiltIns.BackendCommandType:
                            if (currentAppId != BuiltIns.BackendApplication.Id)
                            {
                                return false;
                            }
                            break;
                        default:
                            break;
                    }
                }
                return modelAccesser.ExecuteSPReturn("HasCommand", new List<SPParameter>{
                new SPParameter{ Direction= System.Data.ParameterDirection.Input, Name = "userid", Type= System.Data.DbType.Int32, Value=userId},                
                new SPParameter{ Direction= System.Data.ParameterDirection.Input, Name = "cmdid", Type= System.Data.DbType.Int32, Value=cmdId},
                new SPParameter{ Direction= System.Data.ParameterDirection.Input, Name = "targetappid", Type= System.Data.DbType.Int32, Value=cmdTargetAppId},
                new SPParameter{ Direction= System.Data.ParameterDirection.Input, Name = "targetroleid", Type= System.Data.DbType.Int32, Value=targetRoleId},
            }) > 0;
            }
            return false;

        }

        #endregion

        #region RoomGroup
        [OperationBehavior]
        public void DeleteRoomGroup(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomGroupCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete(new RoomGroup { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateRoomGroup(int appid, int userId, string token, Model.Chat.RoomGroup roomGroup)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomGroupCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update(roomGroup);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public RoomGroup AddRoomGroup(int appid, int userId, string token, Model.Chat.RoomGroup roomGroup)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomGroupCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(roomGroup);
                return roomGroup;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<RoomGroup> GetRoomGroups(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<RoomGroup>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public RoomGroup GetRoomGroup(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                RoomGroup roomGroup = new RoomGroup { Id = id };
                modelAccesser.Get(roomGroup);
                return roomGroup.Loaded ? roomGroup : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetRoomGroupCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                return modelAccesser.GetCount<RoomGroup>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region Room
        [OperationBehavior]
        public void DeleteRoom(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete(new Room { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Room UpdateRoom(int appid, int userId, string token, Model.Chat.Room room)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update(room);
                return room;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Room AddRoom(int appid, int userId, string token, Model.Chat.Room room)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(room);
                return room;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<Room> GetRooms(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<Room>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Room GetRoom(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                Room room = new Room { Id = id };
                modelAccesser.Get(room);
                return room.Loaded ? room : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetRoomCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<Room>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void AssignRoomsToAgent(int appid, int userId, string token, int startId, int endId, int agentId)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.ExecuteSPNonQuery("AssignRoomsToAgent", new List<SPParameter>{
                new SPParameter{Direction = System.Data.ParameterDirection.Input,Name="startId",Value=startId},
                new SPParameter{Direction = System.Data.ParameterDirection.Input,Name="endId",Value=endId},
                new SPParameter{Direction=System.Data.ParameterDirection.Input,Name = "agentId",Value = agentId}
            });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region RoomRole

        [OperationBehavior]
        public RoomRole AddRoomRole(int appid, int userId, string token, RoomRole roomRole)
        {
            try
            {
                CheckToken(appid, userId, token);
                //CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add<RoomRole>(roomRole);
                return roomRole;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public RoomRole GetRoomRole(int appid, int userId, string token, int room_Id, int user_Id, int role_Id)
        {
            try
            {
                CheckToken(appid, userId, token);
                RoomRole rRole = new RoomRole { Room_Id = room_Id, User_Id = user_Id, Role_Id = role_Id };
                modelAccesser.Get(rRole);
                return rRole.Loaded ? rRole : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public void DeleteRoomRole(int appid, int userId, string token, int room_Id, int user_Id, int role_Id)
        {
            try
            {
                CheckToken(appid, userId, token);
                //CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete(new RoomRole { Room_Id = room_Id, User_Id = user_Id, Role_Id = role_Id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public List<RoomRole> GetRoomRoles(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<RoomRole>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public int GetRoomRoleCount(int appid, int userId, string token, string condition)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<RoomRole>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public void UpdateRoomRole(int appid, int userId, string token, RoomRole rr)
        {
            try
            {
                CheckToken(appid, userId, token);
                //CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update<RoomRole>(rr);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region User
        [OperationBehavior]
        public void DeleteUser(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.DefineUserCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete(new User { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateUser(int appid, int userId, string token, User user)
        {
            try
            {
                CheckToken(appid, userId, token);
                if (userId != user.Id)
                {
                    CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.DefineUserCommand.Id, BuiltIns.AllRole.Id);
                }
                modelAccesser.Update(user);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public User AddUser(int appid, int userId, string token, User user)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, user.ApplicationCreated_Id, userId, BuiltIns.DefineUserCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(user);
                return user;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public User GetUser(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                User user = new User { Id = id };
                modelAccesser.Get(user);
                return user.Loaded ? user : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<User> GetUsers(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<User>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetUserCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                return modelAccesser.GetCount<User>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public string ResetPassword(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                User user = new User { Id = id };
                modelAccesser.Get<User>(user);
                user.Password = Utility.GetMD5String(id.ToString());
                modelAccesser.Update<User>(user);
                return id.ToString();
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public User Register(int appid, int userId, string account, string pwd, int sex)
        {
            try
            {
                User newUser = null;
                using (TransactionScope scope = new TransactionScope())
                {
                    var userIdList = new UserIdList { Application_Id = appid, User_Id = userId };
                    modelAccesser.Get(userIdList);
                    if (!userIdList.Loaded)
                    {
                        userIdList.Application_Id = BuiltIns.AllApplication.Id;
                        userIdList.User_Id = userId;
                        modelAccesser.Get(userIdList);
                    }
                    if (userIdList.Loaded)
                    {
                        userIdList.IsUsed = true;
                        modelAccesser.Update<UserIdList>(userIdList);

                        newUser = new User { Id = userId, ApplicationCreated_Id = appid, Name = account, NickName = account, Password = pwd, Gender = sex == 0 };
                        modelAccesser.Add<User>(newUser);
                        UserApplicationInfo userInfo = new UserApplicationInfo { Application_Id = appid, Role_Id = BuiltIns.RegisterUserRole.Id, User_Id = userId, Money = 0, AgentMoney = 0, Score = 0 };
                        modelAccesser.Add<UserApplicationInfo>(userInfo);
                    }
                    scope.Complete();
                }

                return newUser;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public void UpdateUserProfileInfo(int appid, User user, string token, Image img)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    UpdateUser(appid, user.Id, token, user);
                    UpdateImage(appid, user.Id, token, img);
                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region GiftGroup
        [OperationBehavior]
        public void DeleteGiftGroup(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineGiftGroupCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete(new GiftGroup { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateGiftGroup(int appid, int userId, string token, GiftGroup giftGroup)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineGiftGroupCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update(giftGroup);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public GiftGroup AddGiftGroup(int appid, int userId, string token, GiftGroup giftGroup)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineGiftGroupCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(giftGroup);
                return giftGroup;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public GiftGroup GetGiftGroup(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                GiftGroup giftGroup = new GiftGroup { Id = id };
                modelAccesser.Get(giftGroup);
                return giftGroup.Loaded ? giftGroup : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<GiftGroup> GetGiftGroups(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<GiftGroup>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetGiftGroupCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                return modelAccesser.GetCount<GiftGroup>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        #endregion

        #region Gift

        [OperationBehavior]
        public void DeleteGift(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineGiftCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete(new Gift { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateGift(int appid, int userId, string token, Gift gift)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineGiftCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update(gift);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Gift AddGift(int appid, int userId, string token, Gift gift)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineGiftCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(gift);
                return gift;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Gift GetGift(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                Gift gift = new Gift { Id = id };
                modelAccesser.Get(gift);
                return gift.Loaded ? gift : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<Gift> GetGifts(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<Gift>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetGiftCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<Gift>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region Image
        [OperationBehavior]
        public void DeleteImage(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                modelAccesser.Delete<Image>(new Image { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateImage(int appid, int userId, string token, Image image)
        {
            try
            {
                CheckToken(appid, userId, token);
                modelAccesser.Update<Image>(image);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void AddImage(int appid, int userId, string token, Image image)
        {
            try
            {
                CheckToken(appid, userId, token);
                modelAccesser.Add<Image>(image);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public Image GetImage(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                Image image = new Image { Id = id };
                modelAccesser.Get<Image>(image);
                return image.Loaded ? image : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<ImageWithoutBody> GetImages(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                var imgs = modelAccesser.GetAll<Image>(condition, "", start, count);
                List<ImageWithoutBody> result = new List<ImageWithoutBody>();
                imgs.ForEach(i => result.Add(i.RemoveBody()));
                return result;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetImageCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<Image>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region BlockType

        [OperationBehavior]
        public List<BlockType> GetBlockTypes(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<BlockType>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetBlockTypeCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<BlockType>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public BlockType GetBlockType(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                BlockType t = new BlockType { Id = id };
                modelAccesser.Get<BlockType>(t);
                return t.Loaded ? t : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public BlockType AddBlockType(int appid, int userId, string token, BlockType blockType)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.BlockTypeCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(blockType);
                return blockType;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public void DeleteBlockType(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.BlockTypeCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete(new BlockType { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public void UpdateBlockType(int appid, int userId, string token, BlockType blockType)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.BlockTypeCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update(blockType);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region ExchangeRate

        [OperationBehavior]
        public List<ExchangeRate> GetAllExchangeRate(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<ExchangeRate>(condition, "[ValidTime] ASC", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetExchangeRateCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<ExchangeRate>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public ExchangeRate AddExchangeRate(int appid, int userId, string token, ExchangeRate eRate)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, eRate.Application_Id, userId, BuiltIns.DefineExchangeRateCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(eRate);
                return eRate;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void DeleteExchangeRate(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                ExchangeRate rate = new ExchangeRate { Id = id };
                modelAccesser.Get<ExchangeRate>(rate);
                if (rate.Loaded)
                {
                    CheckCommand(appid, rate.Application_Id, userId, BuiltIns.DefineExchangeRateCommand.Id, BuiltIns.AllRole.Id);
                    modelAccesser.Delete(rate);
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateExchangeRate(int appid, int userId, string token, ExchangeRate eRate)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, eRate.Application_Id, userId, BuiltIns.DefineExchangeRateCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update(eRate);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region Block

        [OperationBehavior]
        public List<BlockList> GetBlockLists(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<BlockList>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetBlockListCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<BlockList>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public BlockList GetBlockList(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                BlockList list = new BlockList { Id = id };
                modelAccesser.Get<BlockList>(list);
                return list.Loaded ? list : null;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public List<BlockList> GetUserBlockList(int appid, int userId, string token, string blockContent)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<BlockList>("[Content]='" + blockContent+"'", "", -1, -1);
            }
            catch (Exception ex)
            {
                logger.Error("GetUserBlockList : AppId - " + appid + " , UserId - " + userId + " , Token - " + token,ex);
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public BlockList AddBlockList(int appid, int userId, string token, BlockList blockL)
        {
            try
            {
                CheckToken(appid, userId, token);
                //CheckCommand(appid, blockL.Application_Id, userId, BuiltIns.BlockCommand.Id, BuiltIns.AllRole.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    modelAccesser.Add(blockL);
                    if (!string.IsNullOrEmpty(blockL.Content))
                    {
                        modelAccesser.Add<BlockHistory>(new BlockHistory
                        {
                            Application_Id = blockL.Application_Id,
                            BlockType_Id = blockL.BlockType_Id,
                            Content = blockL.Content,
                            IsBlock = true,
                            OptUser_Id = userId,
                            Time = DateTime.Now
                        });
                    }
                    scope.Complete();
                }

                return blockL;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void DeleteBlockList(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                BlockList blockL = new BlockList { Id = id };
                modelAccesser.Get<BlockList>(blockL);
                if (blockL.Loaded)
                {
                    CheckCommand(appid, blockL.Application_Id, userId, BuiltIns.BlockCommand.Id, BuiltIns.AllRole.Id);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        modelAccesser.Add<BlockHistory>(new BlockHistory
                        {
                            Application_Id = blockL.Application_Id,
                            BlockType_Id = blockL.BlockType_Id,
                            Content = blockL.Content,
                            IsBlock = false,
                            OptUser_Id = userId,
                            Time = DateTime.Now
                        });
                        modelAccesser.Delete(blockL);
                        scope.Complete();
                    }
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateBlockList(int appid, int userId, string token, BlockList blockL)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, blockL.Application_Id, userId, BuiltIns.BlockCommand.Id, BuiltIns.AllRole.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    modelAccesser.Update<BlockList>(blockL);
                    if (!string.IsNullOrEmpty(blockL.Content))
                    {
                        modelAccesser.Add<BlockHistory>(new BlockHistory
                        {
                            Application_Id = blockL.Application_Id,
                            BlockType_Id = blockL.BlockType_Id,
                            Content = blockL.Content,
                            IsBlock = true,
                            OptUser_Id = userId,
                            Time = DateTime.Now
                        });
                    }
                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<BlockHistory> GetBlockHistory(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<BlockHistory>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetBlockHistoryCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<BlockHistory>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region UserIdList
        [OperationBehavior]
        public int AddUserIdLists(int appid, int userId, string token, int start, int end, int optUserId, int ownerId, int roleId, int userIdAppid, string password, bool isDirect)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, userIdAppid, userId, BuiltIns.DefineUserIdCommand.Id, BuiltIns.AllRole.Id);
                int result = 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    result = modelAccesser.ExecuteSPNonQuery("AddUserIds", new List<SPParameter>{
                new SPParameter{ Direction= System.Data.ParameterDirection.Input, Name="start", Value=start>=BuiltIns.UserDefinedUserStartId?start:BuiltIns.UserDefinedUserStartId},
                new SPParameter{Direction = System.Data.ParameterDirection.Input, Name ="end", Value=end},
                new SPParameter{Direction = System.Data.ParameterDirection.Input, Name="roleid",Value=roleId},                
                new SPParameter{Direction = System.Data.ParameterDirection.Input, Name="applicationid",Value = userIdAppid},
                new SPParameter{Direction = System.Data.ParameterDirection.Input, Name="ownerid", Value=ownerId},
                new SPParameter{Direction = System.Data.ParameterDirection.Input, Name="password", Value=password},
                new SPParameter{Direction = System.Data.ParameterDirection.Input, Name="isdirect", Value=isDirect}
            });
                    UserIdHistory history = new UserIdHistory { Application_Id = userIdAppid, EndId = end, OptUser_Id = optUserId, Role_Id = roleId, StartId = start, Time = DateTime.Now };
                    modelAccesser.Add<UserIdHistory>(history);
                    scope.Complete();
                }
                return result;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<UserIdList> GetUserIdLists(int appid, int userId, string token, int userIdAppid, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                string con = condition;
                if (userIdAppid != BuiltIns.AllApplication.Id)
                {
                    con = string.IsNullOrEmpty(condition) ?
                    "[Application_Id] = " + userIdAppid + " OR [Application_Id] = " + BuiltIns.AllApplication.Id :
                    condition + " AND ([Application_Id] = " + userIdAppid + " OR [Application_Id] = " + BuiltIns.AllApplication.Id + ")";
                }
                con += string.IsNullOrEmpty(con) ? "[User_Id] > " + BuiltIns.UserDefinedUserStartId : " AND [User_Id] > " + BuiltIns.UserDefinedUserStartId;
                return modelAccesser.GetAll<UserIdList>(con, "", start, count);
            }
            catch (Exception ex)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetUserIdListCount(int appid, int userId, string token, int userIdAppid, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                string con = condition;
                if (userIdAppid != BuiltIns.AllApplication.Id)
                {
                    con = string.IsNullOrEmpty(condition) ?
                    "[Application_Id] = " + userIdAppid + " OR [Application_Id] = " + BuiltIns.AllApplication.Id :
                    condition + " AND ([Application_Id] = " + userIdAppid + " OR [Application_Id] = " + BuiltIns.AllApplication.Id + ")";
                }
                return modelAccesser.GetCount<UserIdList>(con);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateUserIdList(int appid, int userId, string token, UserIdList idlist)
        {
            try
            {
                CheckToken(appid, userId, token);
                modelAccesser.Update<UserIdList>(idlist);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public UserIdList AddUserIdList(int appid, int userId, string token, UserIdList idlist)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, idlist.Application_Id, userId, BuiltIns.DefineUserIdCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add(idlist);
                return idlist;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void DeleteUserIdList(int appid, int userId, string token, int userIdAppid, int userid)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, userIdAppid, userId, BuiltIns.DefineUserIdCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete<UserIdList>(new UserIdList { Application_Id = userIdAppid, User_Id = userid });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int AssignAgentUserIds(int appid, int userId, string token, int userIdAppid, int start, int end, int agent)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, userIdAppid, userId, BuiltIns.AllocateAgentUserIdCommand.Id, BuiltIns.AllRole.Id);
                return modelAccesser.ExecuteSPNonQuery("AssignAgentUserIds", new List<SPParameter>{
                new SPParameter{Name="appid",Type= System.Data.DbType.Int32, Value=userIdAppid},
                new SPParameter{Name="start", Type= System.Data.DbType.Int32, Value=start},
                new SPParameter{Name="end", Type = System.Data.DbType.Int32, Value=end},
                new SPParameter{Name="agent",Type= System.Data.DbType.Int32,Value=agent}
            });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetNextAvailableUserId(int appid, int userId, string token, int userIdAppid, int roleId)
        {
            lock (userIdLock)
            {
                string con = "[IsUsed] = 0 AND [Role_Id] = " + roleId + " AND [User_Id] <> " + currentUserId;
                if (userIdAppid != BuiltIns.AllApplication.Id)
                {
                    con += "AND ([Application_Id] = " + userIdAppid + " OR [Application_Id] = " + BuiltIns.AllApplication.Id + ")";
                }
                var userIds = GetUserIdLists(appid, userId, token, userIdAppid, con, 1, 1);
                if (userIds != null && userIds.Count > 0)
                {
                    currentUserId = userIds[0].User_Id;
                }
                return currentUserId;
            }
        }

        #endregion

        #region UserApplicationInfo
        [OperationBehavior]
        public UserApplicationInfo GetUserInfo(int appid, int userId, string token, int id, int userInfoAppid)
        {
            try
            {
                CheckToken(appid, userId, token);
                UserApplicationInfo info = new UserApplicationInfo { Application_Id = userInfoAppid, User_Id = id };
                modelAccesser.Get<UserApplicationInfo>(info);
                if (!info.Loaded)
                {
                    info.Application_Id = BuiltIns.AllApplication.Id;
                    modelAccesser.Get(info);
                    if (info.Loaded)
                    {
                        return info;
                    }
                }
                else
                {
                    return info;
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
            return null;
        }
        [OperationBehavior]
        public void DeleteUserInfo(int appid, int userId, string token, int id, int userInfoAppid)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, userInfoAppid, userId, BuiltIns.DefineUserCommand.Id, BuiltIns.AllRole.Id);
                UserApplicationInfo info = new UserApplicationInfo { Application_Id = userInfoAppid, User_Id = id };
                modelAccesser.Delete<UserApplicationInfo>(info);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public UserApplicationInfo AddUserInfo(int appid, int userId, string token, UserApplicationInfo info)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, info.Application_Id, userId, BuiltIns.DefineUserCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add<UserApplicationInfo>(info);
                return info;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void UpdateUserInfo(int appid, int userId, string token, UserApplicationInfo info)
        {
            try
            {
                CheckToken(appid, userId, token);
                if (userId != info.User_Id)
                {
                    CheckCommand(appid, info.Application_Id, userId, BuiltIns.DefineUserCommand.Id, BuiltIns.AllRole.Id);
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    UserApplicationInfo oldInfo = new UserApplicationInfo { Application_Id = info.Application_Id, User_Id = info.User_Id };
                    modelAccesser.Get<UserApplicationInfo>(oldInfo);
                    if (oldInfo.Loaded)
                    {
                        if (oldInfo.Role_Id != info.Role_Id)
                        {
                            CheckCommand(appid, info.Application_Id, userId, BuiltIns.UserRoleUpDownCommand.Id, BuiltIns.AllRole.Id);
                            UserIdList idList = new UserIdList { User_Id = info.User_Id, Application_Id = info.Application_Id };
                            modelAccesser.Get<UserIdList>(idList);
                            if (idList.Loaded)
                            {
                                idList.Role_Id = info.Role_Id;
                                idList.IsUsed = true;
                                modelAccesser.Update<UserIdList>(idList);
                            }
                            else
                            {
                                idList.IsUsed = true;
                                idList.Owner_Id = -1;
                                idList.Role_Id = info.Role_Id;
                                modelAccesser.Add<UserIdList>(idList);
                            }
                        }
                        modelAccesser.Update<UserApplicationInfo>(info);
                        scope.Complete();
                    }
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<UserApplicationInfo> GetUserInfos(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<UserApplicationInfo>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetUserInfoCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<UserApplicationInfo>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void ScoreDeposit(int appid, int userId, string token, int depositAppid, int id, int score)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, depositAppid, userId, BuiltIns.ScoreDepositCommand.Id, BuiltIns.AllRole.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    UserApplicationInfo info = new UserApplicationInfo { Application_Id = depositAppid, User_Id = id };
                    modelAccesser.Get(info);
                    if (!info.Score.HasValue)
                    {
                        info.Score = 0;
                    }
                    info.Score += score;
                    modelAccesser.Update(info);
                    DepositHistory history = new DepositHistory { Application_Id = depositAppid, OptUser_Id = userId, User_Id = id, Score = score, Time = DateTime.Now };
                    modelAccesser.Add<DepositHistory>(history);
                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void ScoreExchange(int appid, int userId, string token, int scoreExchangeAppid, int id, int score, int money)
        {
            try
            {
                CheckToken(appid, userId, token);
                //ToDo: Check everyone can exchange score?
                //CheckCommand(appid, scoreExchangeAppid, userId, BuiltIns.ScoreToMoneyCommand.Id, BuiltIns.AllRole.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    UserApplicationInfo info = new UserApplicationInfo { Application_Id = scoreExchangeAppid, User_Id = id };
                    modelAccesser.Get(info);
                    if (!info.Score.HasValue || info.Score < score)
                    {
                        throw new FaultException<ScoreNotEnoughtException>(new ScoreNotEnoughtException());
                    }
                    info.Score -= score;
                    if (!info.Money.HasValue)
                    {
                        info.Money = 0;
                    }
                    info.Money += money;
                    modelAccesser.Update(info);
                    ExchangeHistory history = new ExchangeHistory { Application_Id = scoreExchangeAppid, OptUser_Id = userId, User_Id = id, Score = score, Money = money, ApplyTime = DateTime.Now, SettlementTime=DateTime.Now,Status= 2};
                    modelAccesser.Add<ExchangeHistory>(history);
                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public bool Deposit(int appid, int userId, string token, int depositAppid, int id, int money, bool isAgent)
        {
            CheckToken(appid, userId, token);
            CheckCommand(appid, depositAppid, userId, isAgent ? BuiltIns.AgentDepositCommand.Id : BuiltIns.UserDepositCommand.Id, BuiltIns.AllRole.Id);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    UserApplicationInfo user = new UserApplicationInfo { Application_Id = depositAppid, User_Id = id };
                    modelAccesser.Get<UserApplicationInfo>(user);
                    if (isAgent)
                    {
                        if (!user.AgentMoney.HasValue)
                        {
                            user.AgentMoney = 0;
                        }
                        user.AgentMoney += money;
                    }
                    else
                    {
                        if (!user.Money.HasValue)
                        {
                            user.Money = 0;
                        }
                        user.Money += money;
                    }
                    modelAccesser.Update<UserApplicationInfo>(user);
                    DepositHistory history = new DepositHistory { Application_Id = depositAppid, IsAgent = isAgent, Money = money, OptUser_Id = userId, User_Id = id, Time = DateTime.Now };
                    modelAccesser.Add<DepositHistory>(history);
                    scope.Complete();
                    return true;
                }
            }
            catch { return false; }
        }

        #endregion

        #region History
        [OperationBehavior]
        public List<ExchangeHistory> GetExchangeHistories(int appid, int userId, string token, string condition, bool exchangeCache, int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                if (exchangeCache)
                    CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.ExchangeCommand.Id, BuiltIns.AllRole.Id);
                else
                    CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.QueryExchangeCommand.Id, BuiltIns.AllRole.Id);
                return modelAccesser.GetAll<ExchangeHistory>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetExchangeHistoryCount(int appid, int userId, string token, string condition, bool exchangeCache = false)
        {
            try
            {
                CheckToken(appid, userId, token);
                if (exchangeCache)
                    CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.ExchangeCommand.Id, BuiltIns.AllRole.Id);
                else
                    CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.QueryExchangeCommand.Id, BuiltIns.AllRole.Id);
                return modelAccesser.GetCount<ExchangeHistory>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public List<DepositHistory> GetDepositHistories(int appid, int userId, string token, string condition, int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.QueryDepositCommand.Id, BuiltIns.AllRole.Id);
                return modelAccesser.GetAll<DepositHistory>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public int GetDepositHistoryCount(int appid, int userId, string token, string condition)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.QueryDepositCommand.Id, BuiltIns.AllRole.Id);
                return modelAccesser.GetCount<DepositHistory>(condition);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void DeleteDepositHistory(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.QueryDepositCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete<DepositHistory>(new DepositHistory { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public void DeleteExchangeHistory(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.QueryExchangeCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete<ExchangeHistory>(new ExchangeHistory { Id = id });
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public void CancelExchangeCache(int appid, int userId, string token, List<ExchangeHistory> history)
        {
            try
            {
                if (history.Count > 0)
                {
                    CheckToken(appid, userId, token);
                    CheckCommand(appid, history[0].Application_Id, userId, BuiltIns.ExchangeCommand.Id, BuiltIns.AllRole.Id);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        history.ForEach(h =>
                            {
                                if (h.Status <= 2)
                                {
                                    UserApplicationInfo uInfo = new UserApplicationInfo { User_Id = h.User_Id, Application_Id = h.Application_Id };
                                    modelAccesser.Get<UserApplicationInfo>(uInfo);
                                    uInfo.Score += h.Score;
                                    uInfo.Money += h.Money;
                                    modelAccesser.Update<UserApplicationInfo>(uInfo);
                                    modelAccesser.Delete<ExchangeHistory>(h);
                                }
                            });
                        scope.Complete();
                    }
                }
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public List<ExchangeHistory> GetExchangeHistoryForSettlement(int appid, int userId, string token, string condition, int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns.AllApplication.Id, userId, BuiltIns.SettlementCommand.Id, BuiltIns.AllRole.Id);
                return modelAccesser.GetAll<ExchangeHistory>(condition, "", start, count);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public void SettlementExchange(int appid, int userId, string token, ExchangeHistory history)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, history.Application_Id, userId, BuiltIns.SettlementCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update<ExchangeHistory>(history);
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public ExchangeHistory AddExchangeHistory(int appid, int userId, string token, ExchangeHistory history)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, history.Application_Id, userId, BuiltIns.ExchangeCommand.Id, BuiltIns.AllRole.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    modelAccesser.Add<ExchangeHistory>(history);
                    UserApplicationInfo uInfo = new UserApplicationInfo { User_Id = history.User_Id, Application_Id = history.Application_Id };
                    modelAccesser.Get<UserApplicationInfo>(uInfo);
                    uInfo.Score -= history.Score;
                    uInfo.Money -= history.Money;
                    modelAccesser.Update<UserApplicationInfo>(uInfo);
                    scope.Complete();
                }
                return history;
            }
            catch (Exception)
            {
                throw new DatabaseException();
            }
        }

        [OperationBehavior]
        public void SettleExchangeCache(int appid, int userId, string token, List<ExchangeHistory> history)
        {
            if (history.Count > 0)
            {
                try
                {
                    CheckToken(appid, userId, token);
                    CheckCommand(appid, history[0].Application_Id, userId, BuiltIns.SettlementCommand.Id, BuiltIns.AllRole.Id);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        foreach (ExchangeHistory h in history)
                        {
                            if (h.Status == (int)RequestStatus.Submitted)
                            {
                                modelAccesser.Get<ExchangeHistory>(h);
                                h.Status = (int)RequestStatus.Processed;
                                modelAccesser.Update<ExchangeHistory>(h);
                            }
                        }
                        scope.Complete();
                    }
                }
                catch (Exception)
                {
                    throw new DatabaseException();
                }
            }
        }

        [OperationBehavior]
        public void ConfirmExchangeCache(int appid, int userId, string token, List<ExchangeHistory> history)
        {
            if (history.Count > 0)
            {
                try
                {
                    CheckToken(appid, userId, token);
                    CheckCommand(appid, history[0].Application_Id, userId, BuiltIns.ExchangeCommand.Id, BuiltIns.AllRole.Id);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        foreach (ExchangeHistory h in history)
                        {
                            if (h.Status == (int)RequestStatus.Processed)
                            {
                                modelAccesser.Get<ExchangeHistory>(h);
                                h.Status = (int)RequestStatus.Closed;
                                modelAccesser.Update<ExchangeHistory>(h);
                            }
                        }
                        scope.Complete();
                    }
                }
                catch (Exception)
                {
                    throw new DatabaseException();
                }
            }
        }

        #endregion

        #region IErrorHandler

        public bool HandleError(Exception error)
        {
            //log the error
            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (error is DatabaseException)
            {
                FaultException<DatabaseExceptionMsg> faultException = new FaultException<DatabaseExceptionMsg>(
                    new DatabaseExceptionMsg(YoYoStudio.Resource.Messages.GeneralError), new FaultReason(YoYoStudio.Resource.Messages.GeneralError));
                MessageFault messageFault = faultException.CreateMessageFault();
                fault = System.ServiceModel.Channels.Message.CreateMessage(version, messageFault, faultException.Action);

            }
        }

        #endregion

        #region IServiceBehavior

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(this);
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            return;
        }

        #endregion

        #region RoomConfig
        [OperationBehavior]
        public void DeleteRoomConfig(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Delete<RoomConfig>(new RoomConfig { Id = id });
            }
            catch
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public RoomConfig UpdateRoomConfig(int appid, int userId, string token, RoomConfig config)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Update<RoomConfig>(config);
                return config;
            }
            catch
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public RoomConfig AddRoomConfig(int appid, int userId, string token, RoomConfig config)
        {
            try
            {
                CheckToken(appid, userId, token);
                CheckCommand(appid, BuiltIns._9258ChatApplication.Id, userId, BuiltIns.DefineRoomCommand.Id, BuiltIns.AllRole.Id);
                modelAccesser.Add<RoomConfig>(config);
                return config;
            }
            catch
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public RoomConfig GetRoomConfig(int appid, int userId, string token, int id)
        {
            try
            {
                CheckToken(appid, userId, token);
                RoomConfig config = new RoomConfig { Id = id };
                modelAccesser.Get<RoomConfig>(config);
                return config.Loaded ? config : null;
            }
            catch
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public List<RoomConfig> GetRoomConfigs(int appid, int userId, string token, string condition = "", int start = -1, int count = -1)
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetAll<RoomConfig>(condition, "", start, count);
            }
            catch
            {
                throw new DatabaseException();
            }
        }
        [OperationBehavior]
        public int GetRoomConfigCount(int appid, int userId, string token, string condition = "")
        {
            try
            {
                CheckToken(appid, userId, token);
                return modelAccesser.GetCount<RoomConfig>(condition);
            }
            catch
            {
                throw new DatabaseException();
            }
        }

        #endregion

        #region Musics

        public List<MusicInfo> GetMusics(int appid, int userId, string token)
        {
            try
            {
                CheckToken(appid, userId, token);
                List<MusicInfo> result = new List<MusicInfo>();
                List<FileInfo> musics = Utility.GetMusicsOnRed5Locally();
                foreach (FileInfo info in musics)
                {
                    MusicInfo musicInfo = new MusicInfo();
                    musicInfo.Name = info.Name;
                    musicInfo.LastModified = info.LastWriteTime;
                    musicInfo.Size = info.Length;
                    result.Add(musicInfo);
                }
                return result;
            }
            catch { return new List<MusicInfo>(); }

        }

        public void DeleteMusics(int appid, int userId, string token, List<MusicInfo> toDelete)
        {
            try
            {
                CheckToken(appid, userId, token);
                List<string> fileNames = new List<string>();
                foreach (var item in toDelete)
                {
                    fileNames.Add(item.Name);
                }
                Utility.DeleteMusicsOnRed5Locally(fileNames);
            }
            catch { }
        }

        public void UploadMusics(int appid, int userId, string token, List<Byte[]> toUpload)
        {
            try
            {
                CheckToken(appid, userId, token);
                Utility.UploadFileOnRed5Locally(toUpload);
            }
            catch { }
        }

        #endregion

    }
}
