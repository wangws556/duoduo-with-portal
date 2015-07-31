using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;
using YoYoStudio.Resource;

namespace YoYoStudio.Model.Json
{
    public class JsonConst
    {
        public const string Update = "update";
        public const string Add = "add";
        public const string Delete = "delete";

        public static Array GetStatusArray()
        {
            return new object[]{new {Id=(int)RequestStatus.None, Name=""}, 
				new {Id=(int)RequestStatus.NotSubmitted, Name=Text.ResourceManager.GetString(RequestStatus.NotSubmitted.ToString())},
 				new {Id=(int)RequestStatus.Submitted, Name=Text.ResourceManager.GetString(RequestStatus.Submitted.ToString())},
				new {Id=(int)RequestStatus.Processed, Name=Text.ResourceManager.GetString(RequestStatus.Processed.ToString())},
				new {Id=(int)RequestStatus.Closed, Name=Text.ResourceManager.GetString(RequestStatus.Closed.ToString())}
			};
        }
    }

    [Serializable]
	public class JsonModel
	{
        protected ModelEntity entity;

        public int Id { get; set; }

        public int OldId { get; set; }

        public string Name { get; set; }

		public string Title { get; set; }

        public string __status { get; set; }

        public string Description { get; set; }

        public int? Image_Id { get; set; }

        public string TagId { get; private set; }

        public bool ReadOnly { get; private set; }

        public string icon { get; set; }

		public JsonModel():this(null)
		{
			
		}

        public JsonModel(ModelEntity entity)
        {
            this.entity = entity;
            Title = Text.Title;
            TagId = "Model" + Guid.NewGuid().ToString();
            if (entity is IdedEntity)
            {
                OldId = (entity as IdedEntity).Id;
                Id = OldId;
            }
            if (entity is BuiltInEntity)
            {
                ReadOnly = (entity as BuiltInEntity).IsBuiltIn;
            }
            else if (entity is BuiltInImagedEntity)
            {
                ReadOnly = (entity as BuiltInImagedEntity).IsBuiltIn;
            }
            if (entity is ImagedEntity)
            {
                Image_Id = (entity as ImagedEntity).Image_Id;
            }
        }

        public PersistentState GetEntityState()
        {
            if (!string.IsNullOrEmpty(__status))
            {
                switch (__status)
                {
                    case JsonConst.Add:
                        return PersistentState.Added;
                    case JsonConst.Update:
                        return PersistentState.Changed;
                    case JsonConst.Delete:
                        return PersistentState.Deleted;                        
                }
            }
            return PersistentState.Loaded;
        }

        public T GetConcretModelEntity<T>() where T : ModelEntity
        {
            if (entity == null)
            {
                entity = CreateModelEntity();
            }
            if (entity is IdedEntity)
            {
                (entity as IdedEntity).OldId = OldId;
                (entity as IdedEntity).Id = Id;
            }
            if (entity is ImagedEntity)
            {
                (entity as ImagedEntity).Image_Id = Image_Id;
            }
            return entity as T;
        }

        protected virtual ModelEntity CreateModelEntity()
        {
            return null;
        }
	}
}