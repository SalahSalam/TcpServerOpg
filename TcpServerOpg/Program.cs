using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;
using System.Text.Json;

Console.WriteLine("TCP Server");

TcpListener listener = new TcpListener(IPAddress.Any, 7); // Port 7 for echo protocol or change to desired port.
listener.Start();
Console.WriteLine("Server started, waiting for connections...");

while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
    Console.WriteLine("Client connected: " + clientEndPoint.Address);

    Task.Run(() => HandleClient(socket));
}

listener.Stop();

void HandleClient(TcpClient socket)
{
    NetworkStream ns = socket.GetStream();
    StreamReader reader = new StreamReader(ns);
    StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };


    while (socket.Connected)
    {

        string? message = reader.ReadLine();
        if (message == null)
        {
            break;
        }

        Console.WriteLine("Received: " + message);

        switch (message.ToLower())
        {
            case "random":
                writer.WriteLine("input two numbers");
                string numbers = reader.ReadLine();
                string[] split = numbers.Split(" ");
                if (split.Length == 2 && int.TryParse(split[0], out int a) && int.TryParse(split[1], out int b))
                {
                    Random random = new Random();
                    writer.WriteLine(random.Next(a, b));
                }
                else
                {
                    writer.WriteLine("Invalid input. Please input two numbers.");
                }

                break;
            case "add":
                string numbers1 = reader.ReadLine();
                string[] add = numbers1.Split();
                if (add.Length == 2 && int.TryParse(add[0], out int x) && int.TryParse(add[1], out int y))
                {

                    writer.WriteLine(x + y);
                }
                else
                {
                    writer.WriteLine("Invalid input. Please input two numbers.");
                }
                break;
            case "subtract":
                string numbers2 = reader.ReadLine();
                string[] sub = numbers2.Split(" ");
                if (sub.Length == 2 && int.TryParse(sub[0], out int c) && int.TryParse(sub[1], out int d))
                {

                    writer.WriteLine(c - d);
                }
                else
                {
                    writer.WriteLine("Invalid input. Please input two numbers.");
                }
                break;
            default:
                writer.WriteLine("Unknown command");
                break;
        }

    }
}
