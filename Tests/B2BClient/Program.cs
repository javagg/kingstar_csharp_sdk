using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

namespace B2BClient
{
    class Program
    {
        private static int MSG_MAXLEN = 1024;

        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: B2BClient <IP Addresss> <PORT> <USER> <PASSWORD>");
                Environment.Exit(1);
            }

            var ip = args[0];
            var port = int.Parse(args[1]);
            var user = args[2];
            var passwd = args[3];
            TcpClient client = new TcpClient(ip, port);
            NetworkStream stream = client.GetStream();

            byte[] buffer = new Byte[1024];
            byte[] message = new Byte[1024];

            new Thread(new ThreadStart(() =>
                    {
                        Console.WriteLine("Start Receiving...");
                        int readCount = 0;
                        int position = 0;


                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("begin Receiving...");
                                readCount = stream.Read(buffer, 0, buffer.Length);
                                Console.WriteLine("readcount:{0}", readCount);
                                PrintData(buffer, readCount);
                                Buffer.BlockCopy(buffer, 0, message, position, readCount);
                                int eom = Array.IndexOf<byte>(message, (byte)0, position, readCount);
                                if (eom != -1)
                                {
                                    string msg = Encoding.GetEncoding("gb2312").GetString(message, 0, eom);
                                    Console.WriteLine("Receive string: {0}", msg);
                                    int remaining = position + readCount - eom;
                                    Buffer.BlockCopy(message, position, message, 0, remaining);
                                    position = remaining;
                                }
                                position += readCount;
                            }
                            catch (IOException e)
                            {
                                break;
                            }
                        }
               
                        Console.WriteLine("Stop Receiving");

                    })).Start();



//            HandleMessage(stream, string.Format("R|||6011|||{0}|{1}|10.253.44", user, passwd));

            Thread.Sleep(1000);
            SendMessage(stream, string.Format("R|||6011|||{0}|{1}|10.253.44", user, passwd));
            Thread.Sleep(1000);
            stream.Close();
            client.Close();
        }

        public static void PrintData(byte[] data, int count)
        {
            Console.Write("content: ");
            for (int i = 0; i < count; i++)
            {
                Console.Write("{0} ", data[i]);
            }
            Console.WriteLine();

        }

        public static void SendMessage(NetworkStream stream, string message)
        {
            Byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", message);
        }

//        public static void HandleMessage(NetworkStream stream, string message)
//        {
//            Byte[] data = Encoding.ASCII.GetBytes(message);
//            stream.Write(data, 0, data.Length);
//            Console.WriteLine("Sent: {0}", message);
//            data = new Byte[MSG_MAXLEN];
//            String received = String.Empty;
//            int bytes = stream.Read(data, 0, data.Length);
//            received = Encoding.GetEncoding("gbk").GetString(data, 0, bytes);
//            Console.WriteLine("Received: {0}", received);      
//        }
    }
}
