using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class RoleModel : JsonModel
    {
        public int Application_Id { get; set; }
        

        public RoleModel()
            : this(null)
        {
        }

        public RoleModel(Role role):base(role)
        {
            if (role != null)
            {
                Name = role.Name;
                Description = role.Description;
                Application_Id = role.Application_Id;
                Order = role.Order;
            }
        }

        public int Order { get; set; }

        protected override YoYoStudio.Model.ModelEntity CreateModelEntity()
        {
            return new Role { Order = Order, Name = Name, Application_Id = Application_Id, Description = Description };
        }
    }
}