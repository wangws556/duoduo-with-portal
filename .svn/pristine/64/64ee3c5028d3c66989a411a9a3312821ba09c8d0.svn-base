using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class CommandModel : JsonModel
    {
        public int Application_Id { get; set; }

        public int CommandType { get; set; }

        public string ActionName { get; set; }

        public int? Money { get; set; }

        public bool Enabled { get; set; }

        public int IsManagerCmd { get; set; }

        public CommandModel() { }

        public CommandModel(Command cmd)
            : base(cmd)
        {
            if (cmd != null)
            {
                Application_Id = cmd.Application_Id;
                Name = cmd.Name;
                Description = cmd.Description;
                CommandType = cmd.CommandType;
                ActionName = cmd.ActionName;
                Money = cmd.Money;
            }
        }

        protected override ModelEntity CreateModelEntity()
        {
            return new Command
            {
                Name = Name,
                Description = Description,
                Application_Id = Application_Id,
                CommandType =CommandType,
                ActionName = ActionName,
                Money = Money              
            };
        }
    }
}