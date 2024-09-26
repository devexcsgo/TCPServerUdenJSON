using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

Console.WriteLine("TcpServerTest");

TcpListener listener = new TcpListener(IPAddress.Any, 7);
listener.Start();
while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    IPEndPoint iPEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;

    Console.WriteLine("client connected");

    Task.Run(() => HandleClient(socket));
}

void HandleClient(TcpClient socket)
{
    NetworkStream ns = socket.GetStream();
    StreamReader reader = new StreamReader(ns);
    StreamWriter writer = new StreamWriter(ns);

    while (socket.Connected)
    {
        string message = reader.ReadLine()?.ToLower();
        Console.WriteLine(message);
        if (message == "help")
        {
            writer.WriteLine("Commands available are: 1. Add 2. Subtract 3. Random 4. Stop");
            writer.Flush();
        }
        if (message == "stop")
        {
            writer.WriteLine("Server is closing");
            writer.Flush();
            socket.Close();
        }
        if (message == "add")
        {
            writer.WriteLine("Enter two numbers to add");
            writer.Flush();
            int num1 = Convert.ToInt32(reader.ReadLine());
            int num2 = Convert.ToInt32(reader.ReadLine());
            int sum = num1 + num2;
            writer.WriteLine("Sum is " + sum);
            writer.Flush();
        }
        else if (message == "subtract")
        {
            writer.WriteLine("Enter two numbers to subtract");
            writer.Flush();
            int num1 = Convert.ToInt32(reader.ReadLine());
            int num2 = Convert.ToInt32(reader.ReadLine());
            int sub = num1 - num2;
            writer.WriteLine("Subtraction is " + sub);
            writer.Flush();
        }
        else if (message == "random")
        {
            writer.WriteLine("Enter two numbers to get a random number");
            writer.Flush();
            int num1 = Convert.ToInt32(reader.ReadLine());
            int num2 = Convert.ToInt32(reader.ReadLine());
            Random random = new Random();
            int rand = random.Next(num1, num2);
            writer.WriteLine("Random number is " + rand);
            writer.Flush();
        }
    }
}