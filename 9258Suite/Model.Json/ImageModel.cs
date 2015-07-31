using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model.Json
{  
    public class ImageModel : JsonModel
    {
        public string Ext { get; set; }
        public string OwnerType { get; set; }
        public string Message { get; set; }

        public ImageModel() : this(string.Empty) { }
        public ImageModel(string ownertype)
            : this(ownertype,null)
        {
        }
        public ImageModel(string ownertype, Image image)
            : base(image)
        {
            if (image != null)
            {
                Name = image.Name;
                Ext = image.Ext;
            }
            OwnerType = ownertype;
        }

        protected override YoYoStudio.Model.ModelEntity CreateModelEntity()
        {
            return new Image { Name = Name, Ext = Ext };
        }

        public int OwnerId { get; set; }

    }
}