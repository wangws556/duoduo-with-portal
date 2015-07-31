using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Client.ViewModel
{
    [Serializable]
    [SnippetPropertyINPC(field = "name", property = "Name", type = "string", defaultValue = "string.Empty")]
	[SnippetPropertyINPC(field = "imageVMs", property = "ImageVMs", type = "ObservableCollection<ImageViewModel>", defaultValue = "null")]
    public partial class ImageGroupViewModel:ViewModelBase
    {
        public ImageGroupViewModel(string name)
        {
            this.name.SetValue(name);
            this.imageVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<ImageViewModel>());
        }
        public int Id { get; set; }
    }   
}
