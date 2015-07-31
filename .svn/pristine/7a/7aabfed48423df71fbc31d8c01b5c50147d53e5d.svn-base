using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace YoYoStudio.Common.Wpf.ViewModel
{
    [ComVisible(true)]
    [Serializable]
    public class ImageViewModel : TitledViewModel
    {
        public string AbsolutePathWithoutExt { private get; set; }
        public string RelativePathWithoutExt { private get; set; }
        public bool IsSelected { get; set; }
        public byte[] TheImage { get; set; }
        public object Tag { get; set; }

        public string Ext { get; set; }
		public string ImageGroup { get; set; }
		public int Id { get; set; }

		public string StaticImageFile
		{
			get
			{
				return RelativePathWithoutExt + Ext;
			}
		}

		public string DynamicImageFile
		{
			get
			{
				if (System.IO.File.Exists(AbsolutePathWithoutExt + ".gif"))
				{
					return RelativePathWithoutExt + ".gif";
				}
				else
				{
					return StaticImageFile;
				}
			}
		}

		public string GetAbsoluteFile(bool s)
		{
			if (System.IO.File.Exists(AbsolutePathWithoutExt + ".gif") && (!s))
			{
				return AbsolutePathWithoutExt + ".gif";
			}
			else
			{
				return AbsolutePathWithoutExt + Ext;
			}
		}
    }
}
