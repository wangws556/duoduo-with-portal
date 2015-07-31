using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace YoYoStudio.SocketService.Music
{
    public class TcpAsynchronousServer
    {
        // Thread signal.
        public ManualResetEvent allDone = new ManualResetEvent(false);

        private  int port;
        private  string filePath;

        public TcpAsynchronousServer()
        {
            
            port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TcpPort"]);
            filePath = System.Configuration.ConfigurationManager.AppSettings["MusicServerPath"];
        }

        // the Tcp socket
        private static TcpListener listener;

        public void StartListening()
        {
            listener = new TcpListener(IPAddress.Any,port);
            listener.Start(50);
            try
            {
                for (; ; )
                {
                    //set the event to nonsignaled state.
                    allDone.Reset();

                    Console.WriteLine("Waiting for a connection...");

                    listener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }

        
        public void AcceptCallback(IAsyncResult ar)
        {
            //Signal the main thread to continue.
            allDone.Set();

            //Get the socket that handles the client request.
            TcpListener listener = (TcpListener)ar.AsyncState;
            TcpClient client = listener.EndAcceptTcpClient(ar);
            NetworkStream newworkStream = null;

            if (client != null)
            {
                Console.WriteLine("Received connection!");
                newworkStream = client.GetStream();

                Byte[] header = new Byte[1];
                int length;
                int receivedDataLength =0;
                if ((length = newworkStream.Read(header, 0, header.Length)) != 0)
                {
                    receivedDataLength += length;
                    TcpPacketType packetType = (TcpPacketType)Enum.ToObject(typeof(TcpPacketType), header[0]);
                    if (packetType == TcpPacketType.Upload)
                    {
                        Byte[] nameLengthBytes = new Byte[4];
                        if ((length = newworkStream.Read(nameLengthBytes, 0, nameLengthBytes.Length)) != 0)
                        {
                            receivedDataLength += length;
                            int nameLength = BitConverter.ToInt32(nameLengthBytes, 0);
                            Byte[] nameBytes = new Byte[nameLength];
                            if ((length = newworkStream.Read(nameBytes, 0, nameBytes.Length)) != 0)
                            {
                                receivedDataLength += length;
                                string name = System.Text.Encoding.UTF8.GetString(nameBytes);
                                using (Stream stream = new FileStream(System.IO.Path.Combine(filePath, name), FileMode.OpenOrCreate, FileAccess.Write))
                                {
                                    Byte[] bytes = new Byte[1024];
                                    while ((length = newworkStream.Read(bytes, 0, bytes.Length)) != 0)
                                    {
                                        stream.Write(bytes, 0, length);
                                    }
                                }
                            }
                        }
                        //byte[] receivedData = new byte[4];
                        //receivedData = BitConverter.GetBytes(receivedDataLength);
                        //newworkStream.Write(receivedData, 0, 4);
                    }
                    else if (packetType == TcpPacketType.Get)
                    {
                        List<string> fileNames = new List<string>();
                        DirectoryInfo directInfo = new DirectoryInfo(filePath);
                        foreach (FileInfo fileInfo in directInfo.GetFiles())
                        {
                            if (fileInfo.Extension == ".mp3" || fileInfo.Extension == ".flv")
                            {
                                fileNames.Add(fileInfo.Name);
                            }
                        }
                        StringBuilder sb = new StringBuilder();
                        if (fileNames.Count > 0)
                        {
                            for (int i = 0; i < fileNames.Count - 1; i++)
                            {
                                sb.Append(fileNames[i] + "#");
                            }
                            sb.Append(fileNames[fileNames.Count - 1]);
                        }
                        Byte[] dataToSend = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
                        newworkStream.Write(dataToSend, 0, dataToSend.Length);
                        newworkStream.Flush();
                    }

                    else if (packetType == TcpPacketType.Delete)
                    {
                        List<string> result = new List<string>();
                        Byte[] buffer = new Byte[1024];
                        int byteRead;
                        StringBuilder sb = new StringBuilder();
                        while ((byteRead = newworkStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            sb.Append(System.Text.Encoding.UTF8.GetString(buffer));
                        }
                        string songNames = sb.ToString().TrimEnd('\0');
                        result = songNames.Split('#').ToList();

                        DirectoryInfo director = new DirectoryInfo(filePath);
                        if (director.Exists)
                        {
                            List<FileInfo> fileInfo = director.GetFiles().ToList();
                            foreach (string name in result)
                            {
                                FileInfo info = fileInfo.Find(r => r.Name == name);
                                if (info != null && !IsFileLocked(info))
                                    info.Delete();
                            }
                        }
                    }
                }
                newworkStream.Close();
                client.Close();
            }
            

            allDone.Set();
        }

        private bool IsFileLocked(FileInfo file)
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
    }
}
