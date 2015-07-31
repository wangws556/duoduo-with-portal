using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class ExchangeRateModel:JsonModel
    {
        public int Application_Id { get; set; }
        public String ValidTime { get; set; }
        public int? ScoreToMoney { get; set; }
        public int? MoneyToCache { get; set; }
        public int? ScoreToCache { get; set; }

        public ExchangeRateModel() { }
        public ExchangeRateModel(ExchangeRate eRate)
            : base(eRate)
        {
            if (eRate != null)
            {
                Application_Id = eRate.Application_Id;
                ValidTime = eRate.ValidTime.ToShortDateString();
                ScoreToMoney = eRate.ScoreToMoney;
                MoneyToCache = eRate.MoneyToCache;
                ScoreToCache = eRate.ScoreToCache;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new ExchangeRate
            {
                Application_Id = Application_Id,
                ValidTime = Convert.ToDateTime(ValidTime),
                ScoreToMoney = ScoreToMoney,
                MoneyToCache = MoneyToCache,
                ScoreToCache = ScoreToCache
            };
        }
    }
}