using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;

namespace YoYoStudio.Client.ViewModel
{
    [Serializable]
    [SnippetPropertyINPC(field="applicationId",property="ApplicationId",type="int",defaultValue="0")]
    [SnippetPropertyINPC(field="validTime",property="ValidTime",type="string",defaultValue="string.Empty")]
    [SnippetPropertyINPC(field="scoreToMoney",property="ScoreToMoney",type="int",defaultValue="0")]
    [SnippetPropertyINPC(field="moneyToCache",property="MoneyToCache",type="int",defaultValue="0")]
    [SnippetPropertyINPC(field = "scoreToCache", property = "ScoreToCache", type = "int", defaultValue = "0")]
    public partial class ExchangeRateViewModel:IdedEntityViewModel
    {
        public ExchangeRateViewModel(ExchangeRate rate)
            : base(rate)
        {
            ApplicationId = rate.Application_Id;
            ValidTime = rate.ValidTime.ToShortDateString();
            ScoreToMoney = rate.ScoreToMoney.HasValue ? rate.ScoreToMoney.Value : 0;
            MoneyToCache = rate.MoneyToCache.HasValue ? rate.MoneyToCache.Value : 0;
            ScoreToCache = rate.ScoreToCache.HasValue ? rate.ScoreToCache.Value : 0;
        }

        public override object ToJson()
        {
            return new ExchangeRateModel(GetConcretEntity<ExchangeRate>())
            {
                Application_Id = ApplicationId,
                ValidTime = ValidTime,
                ScoreToMoney = ScoreToMoney,
                MoneyToCache = MoneyToCache,
                ScoreToCache = ScoreToCache
            };
        }
    }
}
