using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Net;

namespace B2BPush
{
    [StructLayout(LayoutKind.Sequential)]
    public class MessageHead
    {
        public ushort Type;
        public ushort Length;
        //       public string Content;
        //        private ushort type;
        //        private ushort length;

        //        public ushort Type
        //        {
        //            get
        //            {
        //                return (ushort)IPAddress.NetworkToHostOrder((short)this.type);
        //            }
        //
        //            set
        //            {
        //                this.type = (ushort)IPAddress.HostToNetworkOrder((short)value);
        //            }
        //        }
        //
        //        public ushort Length
        //        {
        //            get
        //            {
        //                return (ushort)IPAddress.NetworkToHostOrder((short)this.length);
        //            }
        //
        //            set
        //            {
        //                this.type = (ushort)IPAddress.HostToNetworkOrder((short)value);
        //            }
        //        }


    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConnectRequest
    {
        const ushort Type = 1;
        const ushort Length = 0;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class PingRequest
    {
        const ushort Type = 20;
        const ushort Length = 0;
    }

    class Program
    {
        static TcpClient client;
        static NetworkStream stream;

        public static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: B2BPush <IP Addresss> <PORT> <USER> <PASSWORD>\n");
                Environment.Exit(1);
            }

            var ip = args[0];
            var port = int.Parse(args[1]);
            var user = args[2];
            var passwd = args[3];
            client = new TcpClient(ip, port);
            stream = client.GetStream();

//            byte[] buffer = new Byte[1024];
//            byte[] message = new Byte[4096];
//            Thread t = new Thread(new ThreadStart(() =>
//                    {
//                        Console.WriteLine("Start Receiving...");    
//                        int readCount = 0;
//                        int position = 0;
//
//                        do
//                        {
//                            readCount = stream.Read(buffer, 0, buffer.Length);
//                            Console.WriteLine("Receiving some data... {0}", readCount);    
//                            buffer.CopyTo(message, position);
//                            position += readCount;
//                            MessageHead head = FromBytes(message);
//                            if (position - 4 >= head.Length)
//                            {
//                                string msg = Encoding.GetEncoding("gbk").GetString(message, 4, position);
//                                Console.WriteLine(msg);
//                                position = 0;
//                            }
//                        }
//                        while(stream.DataAvailable);
//                    }));
//            t.Start();
//
//            MessageHead connectMsg = new MessageHead();
//            connectMsg.Type = (ushort)IPAddress.HostToNetworkOrder((short)1);
//            connectMsg.Length = (ushort)IPAddress.HostToNetworkOrder((short)0);
//
//            byte[] data = ToByteArray(connectMsg);
//            stream.Write(data, 0, data.Length);
//            Console.WriteLine("{0} {1} {2} {3}", data[0], data[1], data[2], data[3]);
//            t.Join();         
//            Console.WriteLine("Done!");

			Byte[] data = {0,1,0,0};
			stream.Write(data, 0, data.Length);
			Console.WriteLine("Sent: {0} {1} {2} {3}", data[0],data[1],data[2],data[3] );  
			Byte[] data1 = new Byte[128];
			Thread.Sleep (10);
			//	String received = String.Empty;
			int bytes = stream.Read(data1, 0, data1.Length);
			Console.WriteLine("length: {0}", bytes);  
			//received = Encoding.GetEncoding("gbk").GetString(data, 0, bytes);
			Console.WriteLine("Received: {0} {1} {2} {3}", data1[0],data1[1],data1[2],data1[3] );  
        }

        public static MessageHead FromBytes(byte[] buf)
        {
            MessageHead head = new MessageHead();
            int size = Marshal.SizeOf(head);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(buf, 0, ptr, 4);
            head = (MessageHead)Marshal.PtrToStructure(ptr, head.GetType());
            Marshal.FreeHGlobal(ptr);
            return head;
        }

        public static byte[] ToByteArray(MessageHead req)
        {
            int size = Marshal.SizeOf(req);
            byte[] buf = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(req, ptr, true);
            Marshal.Copy(ptr, buf, 0, buf.Length);
            Marshal.FreeHGlobal(ptr);
            return buf;
        }
    }
}
