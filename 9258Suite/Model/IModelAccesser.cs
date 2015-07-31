using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Common.ORM;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model
{
    public interface IModelAccesser
    {
		List<T> GetAll<T>(string condition = "", string order = "", int start = -1, int count = -1) where T : ModelEntity, new();

		void Add<T>(T obj) where T : ModelEntity, new();

		void Delete<T>(T obj) where T : ModelEntity, new();

		void Update<T>(T obj) where T : ModelEntity, new();

		void Get<T>(T obj) where T : ModelEntity, new();

		int GetCount<T>(string condition = "") where T : ModelEntity, new();
        
        int ExecuteSPNonQuery(string spName, List<SPParameter> args);
        object ExecuteSPScalar(string spName, List<SPParameter> args);
        int ExecuteSPReturn(string spName, List<SPParameter> args);
        List<T> ExecuteSPReader<T>(string spName, List<SPParameter> args) where T : ModelEntity, new();
        List<string> ExecuteCommand(string cmdText);
    }
}
