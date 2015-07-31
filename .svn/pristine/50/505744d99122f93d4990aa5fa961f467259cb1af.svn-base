using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoYoStudio.ManagementTool.ViewModel;
using YoYoStudio.Model;

namespace YoYoStudio.ManagementTool
{
    /// <summary>
    /// Interaction logic for SmileManagement.xaml
    /// </summary>
    public partial class SmileManagement : UserControl
    {
        public SmileManagement()
        {
            DataContext = new SmileManagementViewModel();
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDlg.Description = "选择图片所在的文件夹...";
            folderBrowserDlg.ShowNewFolderButton = true;
            folderBrowserDlg.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("请等待图片上传结束");
                Cursor = Cursors.Wait;
                string folerName = folderBrowserDlg.SelectedPath;
                var result = UploadPic(folerName);
                SmileManagementViewModel vm = DataContext as SmileManagementViewModel;
                vm.AddSmiles(result, BuiltIns.SmileImageType.Id);
                MessageBox.Show("上传结束");
                Cursor = Cursors.Arrow;
            }
        }

        private Dictionary<string, byte[]> UploadPic(string directory)
        {
            Dictionary<string, byte[]> result = new Dictionary<string, byte[]>();
            string[] fileNames = Directory.GetFiles(directory);
            if (fileNames != null && fileNames.Length > 0)
            {
                foreach (string file in fileNames)
                {
                    FileInfo info = new FileInfo(file);
                    System.Drawing.Imaging.ImageFormat format = null;
                    switch (info.Extension)
                    {
                        case ".gif":
                            format = System.Drawing.Imaging.ImageFormat.Gif;
                            break;
                        case ".bmp":
                            format = System.Drawing.Imaging.ImageFormat.Bmp;
                            break;
                        case ".jpg":
                            format = System.Drawing.Imaging.ImageFormat.Jpeg;
                            break;
                        case ".png":
                            format = System.Drawing.Imaging.ImageFormat.Png;
                            break;
                    }
                    if (format != null)
                    {
                        System.Drawing.Image img;
                        MemoryStream ms = new MemoryStream();
                        img = System.Drawing.Image.FromFile(info.FullName);
                        img.Save(ms, format);
                        result.Add(file, ms.ToArray());
                    }
                }
            }
            string[] directories = Directory.GetDirectories(directory);
            if (directories != null && directories.Length > 0)
            {
                foreach (string dir in directories)
                {
                    foreach (KeyValuePair<string, byte[]> pair in UploadPic(dir))
                    {
                        result.Add(pair.Key, pair.Value);
                    }
                }
            }
            return result;
        }
    }
}
