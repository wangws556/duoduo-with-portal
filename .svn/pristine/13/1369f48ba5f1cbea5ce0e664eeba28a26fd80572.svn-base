using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using YoYoStudio.Common;
using YoYoStudio.Common.ORM;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;
using YoYoStudio.Persistent;

namespace YoYoStudio.DataService.Library
{
    public class DirectModelAccesser :IModelAccesser
    {
        public ReaderWriterLockSlim slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        private string connectionString;
        private IORMapper orMapper = null;
        public static Byte[] defaultImageBytes = Utility.GetImageBytes(YoYoStudio.DataService.Library.Properties.Resources.NotFound);
        public static Image defaultImage = new Image { Ext = "png", ImageType_Id = BuiltIns.AllImageType.Id, Name = "NotFound", TheImage = defaultImageBytes };
        
        public DirectModelAccesser()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CentralData"].ConnectionString;
            orMapper = Singleton<EntityAccesserFactory>.Instance.GetEntityAccesser(EntityAccesserType.SqlServer, connectionString);
        }

        #region IModelAccesser Implementations

		public List<T> GetAll<T>(string condition = "", string order = "", int start = -1, int count = -1) where T : ModelEntity, new()
        {
            return orMapper.Search<T>(condition,order,start,count);
        }

		public void Get<T>(T obj) where T : ModelEntity, new()
        {
            orMapper.Load<T>(obj);
        }

		public void Add<T>(T obj) where T : ModelEntity, new()
        {
			if (obj is IdedEntity)
			{
				int c = orMapper.GetMax<T>();
				obj.AdjustId(c + 1);
			}
            ImagedEntity imgeObj = obj as ImagedEntity;
			if (imgeObj != null)
            {
				if (!imgeObj.Image_Id.HasValue || imgeObj.Image_Id.Value <= 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            slim.EnterWriteLock();
                            int id = orMapper.GetMax<Image>();
                            defaultImage.Id = id + 1;
							defaultImage.ImageType_Id = imgeObj.GetImageType().Id;
                            orMapper.Insert<Image>(defaultImage);
							imgeObj.Image_Id = defaultImage.Id;
							orMapper.Insert<T>(obj);
                            scope.Complete();
                        }
                        finally
                        {
                            slim.ExitWriteLock();
                        }
                    }
                }
                else
                {
                    orMapper.Insert<T>(obj);
                }
            }
            else
            {
                orMapper.Insert<T>(obj);
            }
        }

		public void Delete<T>(T obj) where T : ModelEntity, new()
        {
            orMapper.Delete<T>(obj);
        }

        public void Update<T>(T obj) where T : ModelEntity, new()
        {
            obj.BeforeSave();
            if (obj is IdedEntity)
            {
                IdedEntity idEntity = obj as IdedEntity;
                if (idEntity.OldId > 0)
                {
                    if (idEntity.Id != idEntity.OldId)
                    {
                        T o = new T();
                        IdedEntity newEntity = o as IdedEntity;
                        newEntity.Id = idEntity.Id;
                        orMapper.Load<T>(o);
                        if (!o.Loaded)
                        {
                            //NewId not exist, create the new one and delete the old one
                            using (TransactionScope scope = new TransactionScope())
                            {
                                newEntity.Id = idEntity.OldId;
                                orMapper.Delete<T>(o);
                                orMapper.Insert<T>(obj);
                                scope.Complete();
                                return;
                            }
                        }
                        else
                        {
                            //NewId exist
                            return;
                        }
                    }
                }
            }
            orMapper.Update<T>(obj);
        }

		public int GetCount<T>(string condition = "") where T : ModelEntity, new()
		{
			return orMapper.GetCount<T>(condition);
		}
               

        public int ExecuteSPNonQuery(string spName, List<SPParameter> args)
        {
            return orMapper.ExecuteSPNonQuery(spName, args);
        }

        public int ExecuteSPReturn(string spName, List<SPParameter> args)
        {
            return orMapper.ExecuteSPReturn(spName, args);
        }

        public object ExecuteSPScalar(string spName, List<SPParameter> args)
        {
            return orMapper.ExecuteSPScalar(spName, args);
        }

        public List<T> ExecuteSPReader<T>(string spName, List<SPParameter> args) where T : ModelEntity, new()
        {
            return orMapper.ExecuteSPReader<T>(spName, args);
        }

        public List<string> ExecuteCommand(string cmdText)
        {
            return orMapper.ExecuteCommandReader(cmdText);
        }

        #endregion


        
    }
}
