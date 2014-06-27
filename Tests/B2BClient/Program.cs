using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace B2BClient
{
	class Program
	{
		private static int MSG_MAXLEN = 1024;

        static void Main(string[] args)
		{
			if (args.Length != 4)
			{
				Console.WriteLine("Usage: B2BClient <IP Addresss> <PORT> <USER> <PASSWORD>\n");
				Environment.Exit(1);
			}

			var ip = args[0];
			var port = int.Parse(args[1]);
			var user = args[2];
			var passwd = args[3];
			TcpClient client = new TcpClient(ip, port);
			NetworkStream stream = client.GetStream();
            //    HandleMessageAsync(stream, string.Format("R|||6011|||{0}|{1}|10.253.44.94|1|1|", user, passwd));
            HandleMessage(stream, string.Format("R|||6011|||{0}|{1}|10.253.44", user, passwd));

            //Thread.Sleep(1000);
			stream.Close();
			client.Close();	
		}

        public async static void HandleMessageAsync(NetworkStream stream, string message)
		{
			Byte[] data = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);
			Console.WriteLine("Sent: {0}", message);
			data = new Byte[MSG_MAXLEN];
			String received = String.Empty;
            int bytes = await stream.ReadAsync(data, 0, data.Length);
            received = Encoding.GetEncoding("gbk").GetString(data, 0, bytes);
			Console.WriteLine("Received: {0}", received);      
		}

        public static void HandleMessage(NetworkStream stream, string message)
        {
            Byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", message);
            data = new Byte[MSG_MAXLEN];
            String received = String.Empty;
            int bytes = stream.Read(data, 0, data.Length);
            received = Encoding.GetEncoding("gbk").GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", received);      
        }
	}
}
