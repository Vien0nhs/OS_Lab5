﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Client
{
    static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse("192.168.1.72"), 2024);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client.Connect(iep);

        NetworkStream ns = new NetworkStream(client);
        StreamReader sr = new StreamReader(ns);
        StreamWriter sw = new StreamWriter(ns);
        string s = sr.ReadLine();
        Console.WriteLine("Server gui:{0}", s);

        Thread readThread = new Thread(() =>
        {
            while (true)
            {
                s = sr.ReadLine();
                Console.WriteLine("Server gui:{0}", s);
                if (s.ToUpper().Equals("THOAT")) break;
            }
        });
        readThread.Start();

        string input;
        while (true)
        {
            input = Console.ReadLine();
            sw.WriteLine(input);
            sw.Flush();
            if (input.ToUpper().Equals("THOAT")) break;
        }

        readThread.Join();
        client.Disconnect(true);
        client.Close();
    }
}
