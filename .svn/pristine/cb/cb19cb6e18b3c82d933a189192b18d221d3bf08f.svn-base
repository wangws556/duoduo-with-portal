using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using YoYoStudio.Resource;
using System.Configuration;
using YoYoStudio.Common.ORM;
using YoYoStudio.Persistent;
using YoYoStudio.Common;
using YoYoStudio.Model;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf;
using System.Reflection;
using System.IO;
using YoYoStudio.Model.Core;
using System.Windows;
using System.Transactions;

namespace YoYoStudio.ManagementTool.ViewModel
{
    [SnippetPropertyINPC(field = "commands", property = "Commands", type = "ObservableCollection<SecureCommandViewModel>", defaultValue = "new ObservableCollection<SecureCommandViewModel>()")]
    public partial class MainWindowViewModel : TitledViewModel
    {
        private string connectionString = string.Empty;
        private ORMapper orMapper = null;

        public ORMapper OrMapper
        {
            get
            {
                return orMapper;
            }
        }

        private MainWindowViewModel()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CentralData"].ConnectionString;
            orMapper = Singleton<EntityAccesserFactory>.Instance.GetEntityAccesser(EntityAccesserType.SqlServer, connectionString) as ORMapper;
        }

        public override void Initialize()
        {
            base.Initialize();
            Title = Text.ManagementTool;

            Commands.Add(new SecureCommandViewModel(SmileManagementCommandExecute, CanSmileManagementCommandExecute)
            {
                Title = Text.SmileManagement
            });
            Commands.Add(new SecureCommandViewModel(StampManagementCommandExecute, CanStampManagementCommandExecute)
            {
                Title = Text.StampManagement
            });
            Commands.Add(new SecureCommandViewModel(GenerateImagePackCommandExecute, CanGenerateImagePackCommandExecute)
            {
                Title = Text.GenerateImagePack
            });
            Commands.Add(new SecureCommandViewModel(ImportGifCommandCommandExecute,CanImportGifCommandCommandExecute)
			{
				Title = Text.ImportGif
			});
        }

        #region GenerateImagePackCommand

        private bool CanGenerateImagePackCommandExecute(object parameter)
        {
            return true;
        }

        private void GenerateImagePackCommandExecute(object parameter)
        {
            MessageBox.Show("请等待生成结束");
            string root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),Const.ImageRootFolderName);
            if (Directory.Exists(root))
            {
                Directory.Delete(root,true);
            }
            Directory.CreateDirectory(root);
            foreach (var imgType in BuiltIns.ImageTypes)
            {
                string imgTypeFolder = Path.Combine(root, imgType.Id.ToString());
                Directory.CreateDirectory(imgTypeFolder);
                var groups = orMapper.ExecuteCommandReader("select distinct(ImageGroup) from Image where [ImageType_Id]=" + imgType.Id);
                if (groups != null && groups.Count > 0)
                {
                    foreach (var g in groups)
                    {
                        string groupFolder = Path.Combine(imgTypeFolder, string.IsNullOrEmpty(g) ? Const.DefaultImageGroup : g);
                        Directory.CreateDirectory(groupFolder);
                        var imgs = orMapper.Search<Image>("[ImageType_Id]=" + imgType.Id + " AND [ImageGroup]='" + g + "'");
                        if (imgs != null && imgs.Count > 0)
                        {
                            foreach (var image in imgs)
                            {
								try
								{
									string fullPath = Path.Combine(groupFolder, image.Id + image.Ext);
									FileStream fs = File.Create(fullPath);
									fs.Write(image.TheImage, 0, image.TheImage.Length);
									fs.Flush();
									fs.Close();
									if (image.GifImage != null)
									{
										string gifPath = Path.Combine(groupFolder, image.Id + ".gif");
										FileStream gfs = File.Create(gifPath);
										gfs.Write(image.GifImage, 0, image.GifImage.Length);
										gfs.Flush();
										gfs.Close();
									}
								}
								catch
								{
									continue;
								}
                            }
                        }
                    }
                }
                var nullGroupImages = orMapper.Search<Image>("[ImageType_Id]=" + imgType.Id + " AND [ImageGroup] IS NULL");
                if (nullGroupImages != null && nullGroupImages.Count > 0)
                {
                    string nullGroupFolder = Path.Combine(imgTypeFolder, Const.DefaultImageGroup);
                    Directory.CreateDirectory(nullGroupFolder);
                    foreach (var image in nullGroupImages)
                    {
						try
						{
							string fullPath = Path.Combine(nullGroupFolder, image.Id + image.Ext);
							FileStream fs = File.Create(fullPath);
							fs.Write(image.TheImage, 0, image.TheImage.Length);
							fs.Flush();
							fs.Close();
							if (image.GifImage != null)
							{
								string gifPath = Path.Combine(nullGroupFolder, image.Id + ".gif");
								FileStream gfs = File.Create(gifPath);
								gfs.Write(image.GifImage, 0, image.GifImage.Length);
								gfs.Flush();
								gfs.Close();
							}
						}
						catch
						{
							continue;
						}
                    }
                }
            }
            MessageBox.Show("生成结束");
        }

        #endregion

        #region StampManagementCommand

        private bool CanStampManagementCommandExecute(object parameter)
        {
            return true;
        }

        private void StampManagementCommandExecute(object parameter)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, Actions>>(new EnumNotificationMessage<object, Actions>(Actions.StampManagement));
        }

        #endregion

        #region SmileManagementCommand

        private bool CanSmileManagementCommandExecute(object parameter)
        {
            return true;
        }

        private void SmileManagementCommandExecute(object parameter)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, Actions>>(new EnumNotificationMessage<object, Actions>(Actions.SmileManagement));
        }

        #endregion

        #region ImportGifCommand

        private bool CanImportGifCommandCommandExecute(object parameter)
        {
            return true;
        }

        private void ImportGifCommandCommandExecute(object parameter)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, Actions>>(new EnumNotificationMessage<object, Actions>(Actions.ImportGif));
        }

        internal void ImportGifs(string folerName)
        {
            string condition = "([ImageType_Id] =8 or [ImageType_Id] =9)";
            int count = orMapper.GetCount<Image>(condition);
            int batches = count / 100;
            int left = count % 100;
            for (int i = 0; i < batches;i++ )
            {
                int start = i * 100;
                ProcessImages(start, 100, condition, folerName);
            }
            if (left > 0)
            {
                ProcessImages((batches) * 100, left, condition, folerName);
            }

            Messenger.Default.Send<EnumNotificationMessage<object, Actions>>(new EnumNotificationMessage<object, Actions>(Actions.ImportGifComplete));
        }

        private void ProcessImages(int start, int batch, string condition, string folerName)
        {
            var images = orMapper.Search<Image>(condition, "", start, batch);
			if (images != null && images.Count > 0)
			{
				using (TransactionScope scope = new TransactionScope())
				{
					foreach (var image in images)
					{
						try
						{
							DirectoryInfo di = new DirectoryInfo(folerName);
							var files = di.GetFiles(image.Name + ".gif", SearchOption.AllDirectories);
							if (files != null && files.Length > 0)
							{
								System.Drawing.Image img;
								MemoryStream ms = new MemoryStream();
								img = System.Drawing.Image.FromFile(files[0].FullName);
								img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
								image.GifImage = ms.ToArray();
								orMapper.Update<Image>(image);
							}
						}
						catch
						{
							continue;
						}
					}
					scope.Complete();
				}
			}
        }

        #endregion

        
    }
}
