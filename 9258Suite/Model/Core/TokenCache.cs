using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.ORM;

namespace YoYoStudio.Model.Core
{
    [Table]
    [Serializable]
    public class TokenCache : ModelEntity
    {
        [Column(Type= System.Data.DbType.Int32,IsPrimaryKey=true)]
        public int User_Id { get; set; }
        [Column(Type = System.Data.DbType.Int32, IsPrimaryKey = true)]
        public int Application_Id { get; set; }
        [Column(Type = System.Data.DbType.String)]
        public string Token { get; set; }
    }
}
