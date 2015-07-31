using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
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
        public ActionResult UserManagement()
        {
            return PartialView("PartialViews/UserManagement", new YoYoJson.JsonModel());
        }

        public ActionResult UserDepositHistory()
        {
            return PartialView("PartialViews/UserDepositHistory", new YoYoJson.JsonModel());
        }

        public ActionResult UserExchangeHistory()
        {
            return PartialView("PartialViews/UserExchangeHistory", new YoYoJson.JsonModel());
        }

        public JsonResult GetCurrentUserInfo(string page,string pageSize, int appId)
        {
            string token;
            int userId;
            UserInfoModel result = null;
            string message = string.Empty;
            bool success = false;
            if (GetToken(out userId, out token))
            {
                int total = 0;

                try
                {
                    var userInfos = GetEntities<UserApplicationInfo>(page, pageSize, out total, GetQueryCondition());
                    foreach (var info in userInfos)
                    {
                        UserApplicationInfo userInfo = info as UserApplicationInfo;
                        if (userInfo.User_Id == userId && userInfo.Application_Id == appId)
                        {
                            result = new UserInfoModel(userInfo);
                            break;
                        }
                    }
                    success = true;
                }
                catch (DatabaseException exception)
                {
                    message = exception.Message;
                }
            }
            return Json(new { Success = success, Model = result, Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserInfos(string page, string pageSize, bool includeAll = false)
        {
            int total = 0;
            string message = string.Empty;
            bool success = false;
            List<UserInfoModel> models = new List<UserInfoModel>();
            try
            {
                var userInfos = GetEntities<UserApplicationInfo>(page, pageSize, out total, GetQueryCondition());

                foreach (var info in userInfos)
                {
                    UserApplicationInfo userInfo = info as UserApplicationInfo;
                    if (!includeAll)
                    {
                        if (BuiltIns.ExcludeUserIds.Contains(userInfo.User_Id))
                        {
                            total--;
                            continue;
                        }
                    }
                    models.Add(new UserInfoModel(userInfo));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = models.ToArray(), Total = total,Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsers(string page, string pageSize)
        {
            int total = 0;
            List<UserModel> models = new List<UserModel>();
            string message = string.Empty;
            bool success = false;
            try
            {
                var UserModels = GetEntities<User>(page, pageSize, out total, GetQueryCondition());

                foreach (var userM in UserModels)
                {
                    models.Add(new UserModel(userM as User));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = models.ToArray(), Total = total, Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(int id)
        {
            try
            {
                User u = GetEntity<User>(new int[] { id });
                return Json(new { Success = true, Rows = new User[] { u }, Message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (DatabaseException exception)
            {
                return Json(new { Success = false, Message = exception.Message}, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult ResetPassword(int id)
        {
            int userid = -1;
            string token = string.Empty;
            if (GetToken(out userid, out token))
            {
                try
                {
                    DSClient client = new DSClient(Models.Const.ApplicationId);
                    string pwd = client.ResetPassword(userid, token, id);
                    return Json(new { Success = true, Password = pwd }, JsonRequestBehavior.AllowGet);
                }
                catch (DatabaseException exception)
                {
                    return Json(new { Success = false, password = string.Empty }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }

        private JsonResult Deposit(string money, bool isAgent)
        {
            int userid = -1;
            string token = string.Empty;
            int m = 0;
            if (int.TryParse(money, out m))
            {
                if (GetToken(out userid, out token))
                {
                    string uid = Request.Form["uid"];
                    string aid = Request.Form["aid"];
                    int user = -1, app = -1;
                    if (int.TryParse(uid, out user) && int.TryParse(aid, out app))
                    {
                        try
                        {
                            DSClient client = new DSClient(Models.Const.ApplicationId);
                            if (client.Deposit(userid, token, app, user, m, isAgent))
                            {
                                return Json(new { Success = true, Money = money }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DatabaseException exception)
                        {
                            return Json(new { Success = false, Message = exception.Message }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Deposit(string id)
        {
            return Deposit(id,false);
        }

        [HttpPost]
        public JsonResult AgentDeposit(string id)
        {
            return Deposit(id, true);
        }

        [HttpPost]
        public JsonResult ScoreDeposit(string id)
        {
            int userid = -1;
            string token = string.Empty;
            int score = 0;
            if (int.TryParse(id, out score))
            {
                if (GetToken(out userid, out token))
                {
                    string uid = Request.Form["uid"];
                    string aid = Request.Form["aid"];
                    int user = -1, app = -1;
                    if (int.TryParse(uid, out user) && int.TryParse(aid, out app))
                    {
                        try
                        {
                            DSClient client = new DSClient(Models.Const.ApplicationId);
                            client.ScoreDeposit(userid, token, app, user, score);
                            return Json(new { Success = true, Score = score }, JsonRequestBehavior.AllowGet);
                        }
                        catch (DatabaseException exception)
                        {
                            return Json(new { Success = false, Score = score }, JsonRequestBehavior.AllowGet);
                        }

                    }
                }
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveUsers(List<UserInfoModel> users)
        {
            return SaveEntities<UserApplicationInfo>(users);
        }

        public JsonResult GetUserDepositHistories(string page, string pageSize)
        {
            int total = 0;
            bool success = false;
            string message = string.Empty;
            List<UserDepositHistoryModel> models = new List<UserDepositHistoryModel>();
            try
            {
                var histories = GetEntities<DepositHistory>(page, pageSize, out total, GetQueryCondition());

                foreach (var h in histories)
                {
                    models.Add(new UserDepositHistoryModel(h as DepositHistory));
                }
                success = true;
            }
            catch (DatabaseException exception)
            {
                message = exception.Message;
            }
            return Json(new {Success = success, Rows = models.ToArray(), Total = total, Message= message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserScoreHistories(string page, string pageSize)
        {
            int total = 0;
            List<ExchangeHistoryModel> models = new List<ExchangeHistoryModel>();
            bool success = false;
            string message = string.Empty;
            try
            {
                var histories = GetEntities<ExchangeHistory>(page, pageSize, out total, GetQueryCondition());

                foreach (var h in histories)
                {
                    models.Add(new ExchangeHistoryModel(h as ExchangeHistory));
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
        public JsonResult UpDownUserRoles(List<UserInfoModel> users, int roleId)
        {
            if (users != null)
            {
                try
                {
                    foreach (var user in users)
                    {
                        user.Role_Id = roleId;
                        UpdateEntity(user.GetConcretModelEntity<UserApplicationInfo>());
                    }

                    return Json(new { Success = true, Message = string.Empty}, JsonRequestBehavior.AllowGet);
                }
                catch (DatabaseException exception)
                {
                    return Json(new { Success = false, Message = exception.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }

		public JsonResult GetUserCommandApplications(bool includeAll = false)
		{
			return GetApplicationsForCommand(BuiltIns.DefineUserCommand.Id, includeAll);
		}

		public JsonResult GetUserDepositCommandApplications(bool includeAll = false)
		{
			return GetApplicationsForCommand(BuiltIns.QueryDepositCommand.Id, includeAll);
		}

    }
}