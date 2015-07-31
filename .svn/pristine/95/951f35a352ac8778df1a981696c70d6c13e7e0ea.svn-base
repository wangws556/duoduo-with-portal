using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model.Json
{
    public class UserIdModel : JsonModel
    {
        public UserIdModel():this(null)
        {
        }

        public UserIdModel(UserIdList userId)
            : base(userId)
        {
            if (userId != null)
            {
                User_Id = userId.User_Id;
                Application_Id = userId.Application_Id;
                Owner_Id = userId.Owner_Id;
                Role_Id = userId.Role_Id;
                IsUsed = userId.IsUsed?1:0;
            }
        }

        public int User_Id { get; set; }
        public int Application_Id { get; set; }
        public int? Owner_Id { get; set; }
        public int Role_Id { get; set; }
        public int IsUsed { get; set; }

        protected override YoYoStudio.Model.ModelEntity CreateModelEntity()
        {
            return new UserIdList { IsUsed = IsUsed == 1, Owner_Id = Owner_Id, Role_Id = Role_Id, User_Id = User_Id, Application_Id = Application_Id };
        }
    }
}