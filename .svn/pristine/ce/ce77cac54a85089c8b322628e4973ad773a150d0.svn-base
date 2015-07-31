using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model.Core;
using YoYoStudio.Model;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class ApplicationModel : JsonModel
    {
        public string HomeAddress { get; set; }

        public ApplicationModel() { }
       
        public ApplicationModel(Application app):base(app)
        {
            if (app != null)
            {
                Name = app.Name;
                HomeAddress = app.HomeAddress;
                Description = app.Description;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new Application {  Name = Name, Description = Description, HomeAddress = HomeAddress };
        }

    }    
}