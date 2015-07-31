using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Wpf.ViewModel;
using Snippets;
using YoYoStudio.Resource;
using YoYoStudio.Common.ORM;
using YoYoStudio.Common;
using System.Threading.Tasks;
using YoYoStudio.Model.Core;
using System.IO;
using YoYoStudio.Model;

namespace YoYoStudio.ManagementTool.ViewModel
{
    [SnippetPropertyINPC(field = "imageGroups", property = "ImageGroups", type = "ObservableCollection<string>", defaultValue = "new ObservableCollection<string>()")]
    [SnippetPropertyINPC(field = "selectedImageGroup", property = "SelectedImageGroup", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "newGroup", property = "NewGroup", type = "string", defaultValue = "string.Empty")]
    public partial class SmileManagementViewModel : StampManagementViewModel
    {
        

        public SmileManagementViewModel()
        {
            SelectImageGroupCommandVM = new SecureCommandViewModel(SelectImageGroupCommandExecute);
            
            DeleteSelectedImageGroupCommandVM = new SecureCommandViewModel(DeleteSelectedImageGroupCommandExecute) { Title = Text.DeleteSelectedImageGroup };
            NewImageGroupCommandVM = new SecureCommandViewModel(NewImageGroupCommandExecute) { Title = Text.NewGroup };
            ImageGroups.Add(Text.Select);
            SelectedImageGroup = Text.Select;

            foreach (var g in orMapper.ExecuteCommandReader("select distinct(ImageGroup) from Image where [ImageType_Id] = " + BuiltIns.SmileImageType.Id))
            {
                ImageGroups.Add(g);
            }
        }

        public void AddSmiles(Dictionary<string, byte[]> imgs, int imgTypeId)
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
                    Image image = new Image { Id = maxid, Name = name, Ext = ext, TheImage = img.Value, ImageGroup = SelectedImageGroup, ImageType_Id = imgTypeId };
                    orMapper.Insert<Image>(image);
                    Images.Add(new ImageViewModel { Id = image.Id, Title = image.Name, ImageGroup = image.ImageGroup, TheImage = image.TheImage });
                }
                catch { }
            }
        }

        #region SelectImageGroupCommand
        public SecureCommandViewModel SelectImageGroupCommandVM { get; set; }
        private void SelectImageGroupCommandExecute(SecureCommandArgs args)
        {
            if (!string.IsNullOrEmpty(SelectedImageGroup) && SelectedImageGroup != Text.Select)
            {
                Images.Clear();
                var imgs = orMapper.Search<Image>("[ImageGroup]='" + SelectedImageGroup+"'");
                imgs.ForEach(img =>
                    {
                        Images.Add(new ImageViewModel { Id = img.Id, Title = img.Name, ImageGroup = img.ImageGroup, TheImage = img.TheImage });
                    });
            }
        }
        #endregion

        #region DeleteSelectedImageGroupCommand
        public SecureCommandViewModel DeleteSelectedImageGroupCommandVM { get; set; }
        private void DeleteSelectedImageGroupCommandExecute(SecureCommandArgs args)
        {
            if (!string.IsNullOrEmpty(SelectedImageGroup))
            {
                orMapper.ExecuteCommand("delete from Image where [ImageGroup]='" + SelectedImageGroup + "'");
            }
            ImageGroups.Remove(SelectedImageGroup);
            Images.Clear();
            SelectedImageGroup = Text.Select;
        }
        #endregion

        #region NewImageGroupCommand
        public SecureCommandViewModel NewImageGroupCommandVM { get; set; }
        private void NewImageGroupCommandExecute(SecureCommandArgs args)
        {
            if (!string.IsNullOrEmpty(NewGroup))
            {
                ImageGroups.Add(NewGroup);
                SelectedImageGroup = NewGroup;
            }
        }
        #endregion
    }
}
