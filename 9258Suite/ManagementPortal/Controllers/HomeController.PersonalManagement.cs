using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using YoYoStudio.Exceptions;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        //{name:name,nickName:nickName,age:age,email:email,country:country,state:state,city:city,gender:gender}
        [HttpPost]
        public JsonResult SavePersonal()
        {
            User user = Session["Me"] as User;
            string message = string.Empty;
            bool success = false;
            if (user != null)
            {
                user.Name = Request.Form["name"];
                user.NickName = Request.Form["nickName"];
                user.Age = int.Parse(Request.Form["age"]);
                user.Email = Request.Form["email"];
                user.Country = Request.Form["country"];
                user.State = Request.Form["state"];
                user.City = Request.Form["city"];
                user.Gender = bool.Parse(Request.Form["gender"]);
                try
                {
                    UpdateEntity(user);
                    success = true;
                }
                catch (DatabaseException exception)
                {
                    message = exception.Message;
                }
                
            }
            return Json(new { Success = success, Message = message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PersonalInfoManagement()
        {
            User user = Session["Me"] as User;
            if (user != null)
            {
                return PartialView("PartialViews/PersonalInfoManagement", new UserModel(user));
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
        }

        public ActionResult PasswordManagement()
        {
            return PartialView("PartialViews/PasswordManagement", new UserModel());
        }

        [HttpPost]
        public JsonResult ChangePassword()
        {
            string oldPwd = Request.Form["oldPwd"];
            string newPwd = Request.Form["newPwd"];
            if (!string.IsNullOrEmpty(oldPwd) && !string.IsNullOrEmpty(newPwd))
            {
                return Json(new { Success = WebSecurity.ChangePassword(User.Identity.Name, oldPwd, newPwd) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}