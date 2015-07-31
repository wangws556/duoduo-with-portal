using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model.Json
{
    public class ClientUserModel : UserModel
    {
        public ClientUserModel()
        {
        }

        public ClientUserModel(User user, UserApplicationInfo userInfo)
            : base(user)
        {
            if (userInfo != null)
            {
                Application_Id = userInfo.Application_Id;
                User_Id = userInfo.User_Id;
                if (userInfo.AgentMoney.HasValue)
                    AgentMoney = userInfo.AgentMoney;
                else
                    AgentMoney = 0;
                if (userInfo.Money.HasValue)
                    Money = userInfo.Money;
                else
                    Money = 0;
                if (userInfo.Score.HasValue)
                    Score = userInfo.Score;
                else
                    Score = 0;
                Role_Id = userInfo.Role_Id;
            }
        }

        public int Application_Id { get; set; }
        public int User_Id { get; set; }
        public int? Money { get; set; }
        public int? AgentMoney { get; set; }
        public int? Score { get; set; }
        public int Role_Id { get; set; }
		public string RoleImageUrl { get; set; }
		public string CameraImageUrl { get; set; }
        public bool CanReceiveGift { get; set; }
        public bool CanSendHornMsg { get; set; }
        public bool CanSendHallHornMsg { get; set; }
        public bool CanSendGlobalHornMsg { get; set; }
        public bool CanSendGift { get; set; }
        public bool IsOnMic { get; set; }
    }

    public class UserModel : JsonModel
    {
        public UserModel()
            : this(null)
        {
        }

        public UserModel(User user)
            : base(user)
        {
            if (user != null)
            {
                Age = user.Age;
                ApplicationCreated_Id = user.ApplicationCreated_Id;
                PasswordQuestion = user.PasswordQuestion;
                PasswordAnswer = user.PasswordAnswer;
                NickName = user.NickName;
                Name = user.Name;
                Email = user.Email;
                Country = user.Country;
                State = user.State;
                City = user.City;
                if (user.Gender.HasValue)
                {
                    Gender = user.Gender.Value ? 1 : 0;
                }
                else
                {
                    Gender = 1;
                }
                LastLoginTime = user.LastLoginTime;
                Id = user.Id;      
                Image_Id = user.Image_Id;
            }
        }

        public string NickName { get; set; }
        public int ApplicationCreated_Id { get; set; }
        public string Password { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int? Age { get; set; }
        public int Gender { get; set; }
        public DateTime? LastLoginTime { get; set; }

        protected override YoYoStudio.Model.ModelEntity CreateModelEntity()
        {
            return new User
            {
                Age = Age,
                ApplicationCreated_Id = ApplicationCreated_Id,
                City = City,
                Country = Country,
                Email = Email,
                Gender = Gender==1,
                Id = Id,
                LastLoginTime = LastLoginTime,
                Image_Id = Image_Id,
                Name = Name,
                NickName = NickName,
                PasswordQuestion = PasswordQuestion,
                PasswordAnswer = PasswordAnswer,
                State = State
            };
        }
    }

    public class UserInfoModel : JsonModel
    {
        public UserInfoModel()
            : this(null)
        {
        }

        public UserInfoModel(UserApplicationInfo info):base(info)
        {
            if (info != null)
            {
                Application_Id = info.Application_Id;
                User_Id = info.User_Id;
                AgentMoney = info.AgentMoney;
                Money = info.Money;
                Score = info.Score;
                Role_Id = info.Role_Id;
            }
        }

        public int Application_Id { get; set; }
        public int User_Id { get; set; }
        public int? Money { get; set; }
        public int? AgentMoney { get; set; }
        public int? Score { get; set; }
        public int Role_Id { get; set; }

        protected override YoYoStudio.Model.ModelEntity CreateModelEntity()
        {
            return new UserApplicationInfo { Application_Id = Application_Id, User_Id = User_Id, Role_Id = Role_Id, Money = Money, AgentMoney = AgentMoney, Score = Score };
        }
    }
}