using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Common.ORM;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model
{
	[Flags]
	public enum PersistentState
	{
		Loaded = 1,
		Added = 2,
		Changed = 4,
		Deleted = 8,
	}
    [DataContract]
	[Serializable]
	public abstract class ModelEntity : Entity
	{       
        public ModelEntity()
        {
            
        }
       
        public virtual void AdjustId(int suggestedId)
        {

        }

        public virtual void BeforeSave()
        {
        }

	}
    [DataContract]
    [Serializable]
    public abstract class IdedEntity : ModelEntity
    {
        [DataMember]
        public abstract int Id { get; set; }
        [DataMember]
        public int OldId { get; set; }

        public override void AdjustId(int suggestedId)
        {
            if (Id < suggestedId)
            {
                Id = suggestedId;
            }
        }        
    }

    [DataContract]
    [Serializable]
    public abstract class BuiltInEntity : IdedEntity
    {
        [Column(Type = DbType.Boolean)]
        [DataMember]
        public bool IsBuiltIn { get; set; }

		public override void AdjustId(int suggestedId)
		{
			if (!IsBuiltIn)
			{
				base.AdjustId(suggestedId);
			}
		}
    }


	[DataContract]
	[Serializable]
    public abstract class BuiltInImagedEntity : ImagedEntity
	{
        [Column(Type = DbType.Boolean)]
        [DataMember]
        public bool IsBuiltIn { get; set; }

		public override void AdjustId(int suggestedId)
		{
			if (!IsBuiltIn)
			{
				base.AdjustId(suggestedId);
			}
		}
	}

    [DataContract]
    [Serializable]
    public abstract class ImagedEntity:IdedEntity
    {
        [DataMember]
        [Column(Type = DbType.Int32)]
        public virtual int? Image_Id { get; set; }

        public virtual ImageType GetImageType()
        {
            return BuiltIns.AllImageType;
        }
    }
}
