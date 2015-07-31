using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Json;

namespace YoYoStudio.Client.ViewModel
{
    [Serializable]
    [SnippetPropertyINPC(field = "name", property = "Name", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "price", property = "Price", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "score", property = "Score", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "money", property = "Money", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "unit", property = "Unit", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "description", property = "Description", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "giftGroupVM", property = "GiftGroupVM", type = "GiftGroupViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "runWay",property = "RunWay", type = "int",defaultValue="0")]
    [SnippetPropertyINPC(field = "roomBroadCast", property = "RoomBroadCast", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "worldBroadCast", property = "WorldBroadCast", type = "int", defaultValue = "0")]
    public partial class GiftViewModel : ImagedEntityViewModel
    {
        public GiftViewModel(Gift gift)
            : base(gift)
        {
            name.SetValue(gift.Name);
            price.SetValue(gift.Price);
            score.SetValue(gift.Score);
            money.SetValue(gift.Money.HasValue ? gift.Money.Value : 0);
            unit.SetValue(gift.Unit);
            description.SetValue(gift.Description);
            runWay.SetValue(gift.RunWay.HasValue ? gift.RunWay.Value : 0);
            roomBroadCast.SetValue(gift.RoomBroadcast.HasValue ? gift.RoomBroadcast.Value : 0);
            worldBroadCast.SetValue(gift.WorldBroadcast.HasValue ? gift.WorldBroadcast.Value : 0);
        }

        public override void Initialize()
        {
            var giftGroupId = GetConcretEntity<Gift>().GiftGroup_Id;
            if (giftGroupId.HasValue && giftGroupId.Value > 0)
            {
                giftGroupVM.SetValue(ApplicationVM.LocalCache.AllGiftGroupVMs.FirstOrDefault(g => g.Id == giftGroupId.Value));
            }
            base.Initialize();
        }

        public override object ToJson()
        {
			return new GiftModel(GetConcretEntity<Gift>())
			{
				icon = ImageVM.StaticImageFile,
				GifIcon = ImageVM.DynamicImageFile
			};
        }
    }

    
}
