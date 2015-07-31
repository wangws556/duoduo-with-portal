using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Threading;

namespace YoYoStudio.Common
{
    public class Utility
    {
        public enum ImageExtension { PNG, GIF, JPG }
        public const string  Red5MusicDirectory = @"D:\Red5\webapps\oflaDemo\streams";

        public static string GetMD5String(string str)
        {
            MD5 md5 = MD5.Create();
            string md5String = "";
            byte[] bytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < bytes.Length; i++)
            {
                md5String += bytes[i].ToString("x");
            }
            return md5String;
        }
        public static string GetMacAddress()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString().ToLower() == "true")
                {
                    return mo["MacAddress"].ToString();
                }
            }
            return "";
        }

        public static byte[] GetImageBytes(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            return ms.ToArray();
        }

        public static byte[] GetImageBytesFromFile(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] byData = new byte[fs.Length];
                fs.Read(byData, 0, byData.Length);
                fs.Close();
                return byData;
            }
            return null;
        }

        public static BitmapImage CreateBitmapSourceFromFile(string path)
        {
            BitmapImage result = null;
            if (!String.IsNullOrEmpty(path))
            {
                result = new BitmapImage();
                result.BeginInit();
                result.UriSource = new Uri(path);
                result.EndInit();
            }
            return result;
        }

        public static BitmapSource CreateBitmapSourceFromStream(Stream stream)
        {
            //return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
            //System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            stream.Position = 0;
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.StreamSource = stream;
            img.EndInit();
            return img;
        }

        public static BitmapSource CreateBitmapSource(System.Drawing.Image source)
        {
            MemoryStream ms = new MemoryStream();
            source.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return CreateBitmapSourceFromStream(ms);
        }

        public static BitmapImage BytesToBitmapImage(byte[] array)
        {
            if (array != null)
            {
                using (var ms = new System.IO.MemoryStream(array))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
            return null;
        }

        public static BitmapImage BitmapSourceToBitmapImage(BitmapSource bs)
        {
            BitmapImage bImg = new BitmapImage();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            using (MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                encoder.Frames.Add(BitmapFrame.Create(bs));
                encoder.Save(memoryStream);
                bImg.BeginInit();
                bImg.StreamSource = new System.IO.MemoryStream(memoryStream.ToArray());
                bImg.EndInit();
            }
            return bImg;
        }

        public static byte[] BitmapImageToByteArray(BitmapImage bmp)
        {
            byte[] data;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static void SaveImageToFile(string absolutFileName, ImageExtension type, BitmapSource image)
        {
            try
            {
                using (FileStream stream = new FileStream(absolutFileName, FileMode.OpenOrCreate))
                {
                    BitmapEncoder encoder = GetBitmapEncoder(type);
                    if (encoder != null)
                    {
                        encoder.Frames.Add(BitmapFrame.Create(image));
                        encoder.Save(stream);
                    }
                }
            }
            catch (Exception)
            { }
        }

        private static BitmapEncoder GetBitmapEncoder(ImageExtension type)
        {
            BitmapEncoder encoder = null;
            switch (type)
            {
                case ImageExtension.PNG:
                    encoder = new PngBitmapEncoder();
                    break;
                case ImageExtension.GIF:
                    encoder = new GifBitmapEncoder();
                    break;
                case ImageExtension.JPG:
                    encoder = new JpegBitmapEncoder();
                    break;
                default:
                    break;
            }
            return encoder;
        }

        #region Minimize, Memory Release
        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);


        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurrentProcess();


        public static void MinimizeRelease()
        {
            IntPtr pHandle = GetCurrentProcess();
            SetProcessWorkingSetSize(pHandle, -1, -1);


            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        #endregion

        public static void MusicToFLV(string sourceFile, string outputFile)
        {
            using (Process pro = new Process())
            {
                pro.StartInfo.FileName = getMusicConvertBat();
                pro.StartInfo.UseShellExecute = true;
                pro.StartInfo.CreateNoWindow = true;
                pro.StartInfo.Verb = "runas";
                pro.StartInfo.Arguments = string.Format("{0} {1}", sourceFile, outputFile);
                try
                {
                    pro.Start();
                    if (pro.WaitForExit(20000))
                    {
                        // Process completed. Check process.ExitCode here.
                        if (pro.ExitCode != 0)
                        {
                            //TOTO: Error happens
                            pro.Kill();
                        }
                    }
                    else
                    {
                        // Timed out.
                        pro.Kill();
                    }
                }
                catch(Exception e)
                {
                    //System.Windows.MessageBox.Show(e.Message);
                }
                
            }
        }

        private static string getMusicConvertBat()
        { 
            if(Is64System())
                return AppDomain.CurrentDomain.BaseDirectory + "ToFLV64.bat";
            else
                return AppDomain.CurrentDomain.BaseDirectory + "ToFLV32.bat";
        }

        public static List<FileInfo> GetMusicsOnRed5Locally()
        {
            List<FileInfo> result = new List<FileInfo>();
            DirectoryInfo directInfo = new DirectoryInfo(Red5MusicDirectory);
            if(directInfo.Exists)
            {
                foreach (FileInfo fileInfo in directInfo.GetFiles())
                {
                    if (fileInfo.Extension == ".mp3" || fileInfo.Extension == ".flv")
                    {
                        result.Add(fileInfo);
                    }
                }
            }
            return result;
        }

        public static void DeleteMusicsOnRed5Locally(List<string> toDeleted)
        {
            List<FileInfo> files = GetMusicsOnRed5Locally();
            if(files.Count > 0)
            {
                foreach (string delete in toDeleted)
                {
                    FileInfo fileInfo = files.FirstOrDefault(r => r.Name == delete);
                    if (fileInfo != null)
                    {
                        fileInfo.Delete();
                    }
                }
            }
        }

        public static void UploadFileOnRed5Locally(List<Byte[]> toUpload)
        {
            for (int i = 0; i < toUpload.Count; i++)
            {
                List<Byte> byteList = toUpload[i].ToList();
                Byte[] nameBytes = new Byte[4];
                byteList.CopyTo(0, nameBytes,0,4);
                string fileName = System.Text.Encoding.UTF8.GetString(nameBytes);
                using (Stream stream = new FileStream(Path.Combine(Red5MusicDirectory, fileName), FileMode.OpenOrCreate))
                { 
                    Byte[] fileContent = new Byte[byteList.Count-4];
                    byteList.CopyTo(4, fileContent, 0, fileContent.Length);
                    stream.Write(fileContent, 0, fileContent.Length);
                }
            }
        }

        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public static bool Is64System()
        {
            return IntPtr.Size == 8;
        }

        public static string GetOSDisk()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 3);
        }
    }
}
