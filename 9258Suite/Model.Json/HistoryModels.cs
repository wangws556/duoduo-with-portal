using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Resource;

namespace YoYoStudio.Model.Json
{
    public class UserDepositHistoryModel : JsonModel
    {
        public UserDepositHistoryModel()
            : this(null)
        {
        }
        public UserDepositHistoryModel(DepositHistory history)
            : base(history)
        {
            if (history != null)
            {
                OptUser_Id = history.OptUser_Id;
                User_Id = history.User_Id;
                Application_Id = history.Application_Id;
                Money = history.Money;
                Time = history.Time;
                IsAgent = history.IsAgent.HasValue?(history.IsAgent.Value?1:0):0;
                Score = history.Score;
            }
        }

        public int IsAgent { get; set; }
        public int OptUser_Id { get; set; }
        public int User_Id { get; set; }
        public int Application_Id { get; set; }
        public int? Money { get; set; }
        public int? Score { get; set; }
        public DateTime Time { get; set; }  

    }	    

    public class GiftInOutHistoryModel : JsonModel
    {
        public GiftInOutHistoryModel()
            : this(null)
        { }

        public GiftInOutHistoryModel(GiftInOutHistory history)
            : base(history)
        {
            if (history != null)
            {
                SourceUser_Id = history.SourceUser_Id;
                TargetUser_Id = history.TargetUser_Id;
                Gift_Id = history.Gift_Id;
                Count = history.Count;
                Time = history.Time;
                Room_Id = history.Room_Id;
            }
        }

        public int SourceUser_Id { get; set; }
        public int TargetUser_Id { get; set; }
        public int Gift_Id { get; set; }
        public int Count { get; set; }
        public DateTime Time { get; set; }
        public int Room_Id { get; set; }
    }

    public class ExchangeHistoryModel : JsonModel
    {
        public ExchangeHistoryModel()
            : this(null)
        { }
        public ExchangeHistoryModel(ExchangeHistory history)
            : base(history)
        {
            if (history != null)
            {
                OptUser_Id = history.OptUser_Id;
                User_Id = history.User_Id;
                Application_Id = history.Application_Id;
                ApplyTime = history.ApplyTime;
                SettlementTime = history.SettlementTime;
                Score = history.Score;
                Money = history.Money;
                Cache = history.Cache;
                Status = history.Status;				
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new ExchangeHistory
            {
                OptUser_Id = OptUser_Id,
                User_Id = User_Id,
                Application_Id = Application_Id,
                ApplyTime = ApplyTime,
                SettlementTime = SettlementTime,
                Score = Score,
                Money = Money,
                Cache = Cache,
                Status = Status
            };
        }

        public int OptUser_Id { get; set; }
        public int User_Id { get; set; }
        public int Application_Id { get; set; }
        public DateTime ApplyTime { get; set; }
        public DateTime SettlementTime { get; set; }
        public int? Score { get; set; }
        public int? Money { get; set; }
        public int? Cache { get; set; }
		public int? Status { get; set; }
    }
}