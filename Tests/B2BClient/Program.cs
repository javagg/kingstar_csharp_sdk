using System;
using System.Net.Sockets;
using System.Text;

namespace B2BClient
{
	class Program
	{
		private static int MSG_MAXLEN = 1024;

		public static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Usage: B2BClient <IP Addresss> <PORT>\n");
				Environment.Exit(1);
			}

			var ip = args [0];
			var port = int.Parse (args [1]);
			TcpClient client = new TcpClient(ip, port);

			NetworkStream stream = client.GetStream();
			HandleMessage (stream, "R|||6011|||200889|1|10.253.44.94|1|1|");

			stream.Close ();
			client.Close ();	
		}

		public static void HandleMessage(NetworkStream stream, string message)
		{
			Byte[] data = Encoding.ASCII.GetBytes(message);  
			stream.Write(data, 0, data.Length);
			Console.WriteLine("Sent: {0}", message);     // Buffer to store the response bytes.
			data = new Byte[MSG_MAXLEN];
			String received = String.Empty;
			int bytes = stream.Read(data, 0, data.Length);
			received = Encoding.ASCII.GetString(data, 0, bytes);
			Console.WriteLine("Received: {0}", received);      
		}
	}
}
