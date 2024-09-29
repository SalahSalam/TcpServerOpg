using System;
using System.IO;
using System.Net.Sockets;

Console.WriteLine("TCP Client:");

bool keepSending = true;

TcpClient socket = new TcpClient("127.0.0.1", 7);

NetworkStream ns = socket.GetStream();
StreamReader reader = new StreamReader(ns);
StreamWriter writer = new StreamWriter(ns);

while (keepSending)
{
    string message = Console.ReadLine();

    writer.WriteLine(message);
    writer.Flush();

    string response = reader.ReadLine();

    Console.WriteLine(response);

    if (message.ToLower() == "close")
    {
        keepSending = false;
        break;
    }
}

socket.Close();
