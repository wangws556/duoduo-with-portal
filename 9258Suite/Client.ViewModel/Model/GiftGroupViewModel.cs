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
    [SnippetPropertyINPC(field = "giftVMs", property = "GiftVMs", type = "ObservableCollection<GiftViewModel>", defaultValue = "null")]
    public partial class GiftGroupViewModel : ImagedEntityViewModel
    {
        public GiftGroupViewModel(GiftGroup giftGroup)
            : base(giftGroup)
        {
            name.SetValue(giftGroup.Name);
        }

        public override void Initialize()
        {
            giftVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<GiftViewModel>(ApplicationVM.LocalCache.AllGiftVMs.Where(g => g.GiftGroupVM == this)));
            base.Initialize();
        }

        #region Json

        public override object ToJson()
        {
            GiftGroupModel model = new GiftGroupModel(GetConcretEntity<GiftGroup>());
            if (GiftVMs != null && GiftVMs.Count > 0)
            {
                model.Gifts = new List<GiftModel>();
                foreach (var g in GiftVMs)
                {
                    model.Gifts.Add(g.ToJson() as GiftModel);
                }
            }
            return model;
        }
        #endregion
    }
}
