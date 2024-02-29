using System.Net.Sockets;
using System.Net;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse("192.168.1.72"), 2024);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
        ProtocolType.Tcp);
        client.Connect(iep);

        NetworkStream ns = new NetworkStream(client);
        StreamReader sr = new StreamReader(ns);
        StreamWriter sw = new StreamWriter(ns);
        string s = sr.ReadLine();
        Console.WriteLine("Server gui:{0}", s);
        string input;
        while (true)
        {
            input = Console.ReadLine();
            sw.WriteLine(input);
            sw.Flush();
            if (input.ToUpper().Equals("THOAT")) break;
            s = sr.ReadLine();
            Console.WriteLine("Server gui:{0}", s);
        }
        client.Disconnect(true);
        client.Close();
    }
}