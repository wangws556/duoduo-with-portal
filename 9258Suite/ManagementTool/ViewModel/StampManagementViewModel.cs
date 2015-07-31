using Snippets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Common.ORM;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;
using YoYoStudio.Resource;

namespace YoYoStudio.ManagementTool.ViewModel
{
    [SnippetPropertyINPC(field = "images", property = "Images", type = "ObservableCollection<ImageViewModel>", defaultValue = "new ObservableCollection<ImageViewModel>()")]

    public partial class StampManagementViewModel : ViewModelBase
    {
        protected IORMapper orMapper = null;

        public StampManagementViewModel()
        {
            DeleteSelectedImageCommandVM = new SecureCommandViewModel(DeleteSelectedImageCommandExecute) { Title = Text.DeleteSelectedImage };
            DeleteAllImageCommandVM = new SecureCommandViewModel(DeleteAllImageCommandExecute) { Title = Text.DeleteAll };
            orMapper = Singleton<MainWindowViewModel>.Instance.OrMapper;
        }

        public void LoadImages()
        {
            Images.Clear();
            var imgs = orMapper.Search<Image>("[ImageType_Id]=" + BuiltIns.StampImageType.Id);
            imgs.ForEach(img =>
            {
                Images.Add(new ImageViewModel { Id = img.Id, Title = img.Name, ImageGroup = img.ImageGroup, TheImage = img.TheImage });
            });
        }

        public void AddStamps(Dictionary<string, byte[]> imgs, int imgTypeId)
        {
            var m = orMapper.ExecuteCommandScalar("select max(id) from Image");
            int maxid = (int)m;
            foreach (var img in imgs)
            {
                try
                {
                    maxid++;
                    string name = Path.GetFileNameWithoutExtension(img.Key);
                    string ext = Path.GetExtension(img.Key);
                    Image image = new Image { Id = maxid, Name = name, Ext = ext, TheImage = img.Value, ImageGroup = "", ImageType_Id = imgTypeId };
                    orMapper.Insert<Image>(image);
                    Images.Add(new ImageViewModel { Id = image.Id, Title = image.Name, ImageGroup = image.ImageGroup, TheImage = image.TheImage });
                }
                catch { }
            }
        }

        #region DeleteSelectedImageCommand
        public SecureCommandViewModel DeleteSelectedImageCommandVM { get; set; }
        private void DeleteSelectedImageCommandExecute(SecureCommandArgs args)
        {
            for (int i = 0; i < Images.Count; )
            {
                if (Images[i].IsSelected)
                {
                    orMapper.Delete(new Image { Id = Images[i].Id });
                    Images.RemoveAt(i);
                    continue;
                }
                i++;
            }
        }
        #endregion

        #region DeleteAllImageCommand
        public SecureCommandViewModel DeleteAllImageCommandVM { get; set; }
        private void DeleteAllImageCommandExecute(SecureCommandArgs args)
        {
            for (int i = 0; i < Images.Count; i++)
            {
                orMapper.Delete(new Image { Id = Images[i].Id });
            }
            Images.Clear();
        }
        #endregion
    }
}
