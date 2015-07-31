using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class RoleCommandModel : JsonModel
    {
        public int Command_Id { get; set; }

        public bool IsManagerCommand { get; set; }

        public int SourceRole_Id { get; set; }

        public int TargetRole_Id { get; set; }

        public int Application_Id { get; set; }

        public string Command_Name { get; set; }

        public int CommandType { get; set; }

        public int? Money { get; set; }

        public int? Score { get; set; }

		public int Command_Application_Id { get; set; }

        public string ActionName { get; set; }

        public RoleCommandModel():this(null) { }

        public RoleCommandModel(RoleCommandView rc)
            : base(rc)
        {
            if (rc != null)
            {
                Command_Id = rc.Command_Id;
                IsManagerCommand = rc.IsManagerCommand;
                SourceRole_Id = rc.SourceRole_Id;
                TargetRole_Id = rc.TargetRole_Id;
                Application_Id = rc.Application_Id;
                Command_Name = rc.Command_Name;
                CommandType = rc.CommandType;
                Money = rc.Money;
				Command_Application_Id = rc.Command_Application_Id;
                ActionName = rc.ActionName;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new RoleCommand
            {
                SourceRole_Id = SourceRole_Id,
                TargetRole_Id = TargetRole_Id,
                Command_Id = Command_Id,
                IsManagerCommand = Convert.ToBoolean(IsManagerCommand),
				Application_Id = Application_Id
            };
        }
    }

}