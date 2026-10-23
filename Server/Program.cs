using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main(string[] args)
    {
        var tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 27001);

        tcpListener.Start(10);

        Console.WriteLine($"{tcpListener.Server.LocalEndPoint} listening.....");

        var client = tcpListener.AcceptTcpClient();

        Console.WriteLine($"{client.Client.RemoteEndPoint} connected server....");
    }
}