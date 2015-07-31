using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Common.ORM;

namespace YoYoStudio.Model.Core
{
    [DataContract]
    [Table]
    public class ImageType : BuiltInEntity
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Name { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Description { get; set; }

    }
    [DataContract]
    [KnownType(typeof(Image))]
    public class ImageWithoutBody : BuiltInEntity
    {
        [DataMember]
        [Column(IsPrimaryKey = true, Type = DbType.Int32)]
        public override int Id { get; set; }
        [DataMember]
        [Column(Type = DbType.Int32)]
        public int ImageType_Id { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string ImageGroup { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Name { get; set; }
        [DataMember]
        [Column(Type = DbType.String)]
        public string Ext { get; set; }

    }

    [DataContract]
    [Table]
    public class Image : ImageWithoutBody 
    {        
        [DataMember]
        [Column(Type = DbType.Binary)]
        public byte[] TheImage { get; set; }

        [DataMember]
        [Column(Type = DbType.Binary,AllowNull=true)]
        public byte[] GifImage { get; set; }

        public ImageWithoutBody RemoveBody()
        {
            return new ImageWithoutBody { Id = Id, Ext = Ext, ImageGroup = ImageGroup, ImageType_Id = ImageType_Id, IsBuiltIn = IsBuiltIn, Name = Name };
        }
    }
}
