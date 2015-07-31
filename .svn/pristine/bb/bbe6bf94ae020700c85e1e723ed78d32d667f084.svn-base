using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class GiftModel:JsonModel
    {
        public int Price { get; set; }
        public int Score { get; set; }
        public int? RunWay { get; set; }
        public int? RoomBroadcast { get; set; }
        public int? WorldBroadcast { get; set; }
        public string Unit { get; set; }
        public int? GiftGroup_Id { get; set; }
        public int? Money { get; set; }
		public string GifIcon { get; set; }

        public GiftModel() : this(null) { }

        public GiftModel(Gift gift)
            :base(gift)
        {
            if (gift != null)
            {
                Name = gift.Name;
                Description = gift.Description;
                Price = gift.Price;
                Score = gift.Score;
                RunWay = gift.RunWay;
                RoomBroadcast = gift.RoomBroadcast;
                WorldBroadcast = gift.WorldBroadcast;
                Unit = gift.Unit;
                GiftGroup_Id = gift.GiftGroup_Id;
                Money = gift.Money;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new Gift
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Score = Score,
                RunWay = RunWay,
                RoomBroadcast = RoomBroadcast,
                WorldBroadcast = WorldBroadcast,
                Unit = Unit,
                GiftGroup_Id = GiftGroup_Id,
                Money = Money
            };
        }
    }

    [Serializable]
    public class GiftGroupModel : JsonModel
    {
        public GiftGroupModel() : this(null) { }

        public GiftGroupModel(GiftGroup giftGroup):base(giftGroup)
        {
            if (giftGroup != null)
            {
                Name = giftGroup.Name;
                Description = giftGroup.Description;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new GiftGroup
            {
                Name = Name,
                Description = Description,
            };
        }

        public List<GiftModel> Gifts { get; set; }
    }
}