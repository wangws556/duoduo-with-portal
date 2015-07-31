using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YoYoStudio.SocketService.Music
{
    public class TcpAsynchronousClient
    {
        private string ip;
        private int port;
        private string file;

        public TcpAsynchronousClient() : this(string.Empty) { }

        public TcpAsynchronousClient(string fullPath)
        {
            ip = System.Configuration.ConfigurationManager.AppSettings["TcpServiceIp"];
            port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TcpPort"]);
            file = fullPath;
        }

        public List<string> GetMusics()
        {
            List<string> result = new List<string>(); 
            
            try
            {
                using (TcpClient tcpClient = new TcpClient(ip,port))
                {
                    Byte[] dataToSend = new Byte[1];
                    dataToSend[0] = (byte)TcpPacketType.Get;
                    using (NetworkStream stream = tcpClient.GetStream())
                    {
                        stream.Write(dataToSend, 0, dataToSend.Length);
                        stream.Flush();


                        Byte[] buffer = new Byte[1024];
                        int byteRead;
                        StringBuilder sb = new StringBuilder();
                        while ((byteRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            sb.Append(System.Text.Encoding.UTF8.GetString(buffer));
                        }
                        string songNames = sb.ToString().TrimEnd('\0');
                        result = songNames.Split('#').ToList();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                result.Add(e.ToString());
                return result;
            }
        }

        public void DeleteFile(List<string> names)
        {
            TcpClient tcpClient = null;
            NetworkStream stream = null;
            try
            {
                tcpClient = new TcpClient(ip, port);

                stream = tcpClient.GetStream();

                Byte[] dataToSend = new Byte[1];
                dataToSend[0] = (byte)TcpPacketType.Delete;
                
                StringBuilder sb = new StringBuilder();
                if (names.Count > 0)
                {
                    for (int i = 0; i < names.Count - 1; i++)
                    {
                        sb.Append(names[i] + "#");
                    }
                    sb.Append(names[names.Count - 1]);
                }
                dataToSend = dataToSend.Concat(System.Text.Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
                stream.Write(dataToSend, 0, dataToSend.Length);
                stream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                stream.Close();
                tcpClient.Close();
            }
        }

        public bool UploadFile()
        {
            TcpClient tcpClient = null;
            NetworkStream stream = null;
            try
            {
                tcpClient = new TcpClient(ip,port);

                stream = tcpClient.GetStream();
                Byte[] name = System.Text.Encoding.UTF8.GetBytes(System.IO.Path.GetFileName(file));
                Byte[] dataToSend = new Byte[name.Length + 5];
                dataToSend[0] = (byte)TcpPacketType.Upload;
                Buffer.BlockCopy(BitConverter.GetBytes(name.Length), 0, dataToSend, 1, 4);
                Buffer.BlockCopy(name, 0, dataToSend, 5, name.Length);


                using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
                {
                    int numBytesToRead = Convert.ToInt32(fs.Length);
                    byte[] oFileBytes = new byte[(numBytesToRead)];
                    fs.Read(oFileBytes, 0, numBytesToRead);
                    dataToSend = dataToSend.Concat(oFileBytes).ToArray();
                    stream.Write(dataToSend, 0, dataToSend.Length);
                    stream.Flush();
                }

                //byte[] backData = new byte[4];
                //stream.Read(backData, 0, 4);
                //int length = BitConverter.ToInt32(backData, 0);
                //if (length == dataToSend.Length)
                //{
                    return true;
                //}
                //else
                //{
                //    return false;
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

            finally
            {
                stream.Close();
                tcpClient.Close();
            }
           
        }

    }
}
