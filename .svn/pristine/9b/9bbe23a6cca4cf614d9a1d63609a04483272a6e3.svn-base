using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using YoYoStudio.ChatService.Client;

namespace YoYoStudio.Client.ViewModel
{
    [Serializable]
    public class ClientCache : ChatServiceCache
    {
        public long Version { get; set; }

        private const string cacheFile = "client.cache";

        public static ClientCache LoadCache()
        {
            long ver = client.GetCacheVersion();
            ClientCache cache = null;
            if (File.Exists(cacheFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ClientCache));
                FileStream fs = File.OpenRead(cacheFile);
                try
                {
                    cache = serializer.Deserialize(fs) as ClientCache;                    
                }
                catch { }
                finally
                {
                    fs.Close();
                }
            }
            if (cache == null || cache.Version < ver)
            {
                cache = new ClientCache();
                cache.RefreshCache(null);
                XmlSerializer serializer = new XmlSerializer(typeof(ClientCache));
                File.Delete(cacheFile);
                FileStream fs = File.Create(cacheFile);
                try
                {
                    serializer.Serialize(fs, cache);
                }
                catch { }
                finally
                {
                    fs.Close();
                }
            }
            return cache;
        }
    }
}
