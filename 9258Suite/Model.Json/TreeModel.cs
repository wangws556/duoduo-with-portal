using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class TreeNodeModel : NodeModel
    {
        public string value { get; set; }
        public string url { get; set; }
        public bool isexpand { get; set; }
        public int iconsize { get; set; }
        public bool disabled { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }
        public string html { get; set; }

        public List<TreeNodeModel> items { get; set; }        

        public List<NodeModel> nodes { get; set; }

        public TreeNodeModel()
        {
            iconsize = 16;
        }

    }

    [Serializable]
    public class NodeModel
    {
        public NodeModel()
        {
            count = 0;
            rootid = -1;
        }

        public int rootid { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int count { get; set; }
        public int maxcount { get; set; }

        public string label
        {
            get
            {
                return string.Format("{0}[{1}]", name, count);
            }
        }
    }

	[Serializable]
	public class MenuModel
	{
		public int id { get; set; }
		public string label { get; set; }
        public string value { get; set; }
		public string img { get; set; }
		public bool disabled { get; set; }
		public List<MenuModel> items { get; set; }
	}
}