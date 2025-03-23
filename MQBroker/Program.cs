using System;
using System.Threading;
using MQBroker.Networking;

class Program
{
    static void Main()
    {
        Console.WriteLine("Iniciando el Servidor MQBroker...");

        var serverThread = new Thread(() =>
        {
            new BrokerServer().Start();
        })
        {
            IsBackground = true
        };
        serverThread.Start();

        Thread.Sleep(2000);

        Console.WriteLine("Iniciando MQClient...");

        using var client = new MQClient.Library.MQClient("127.0.0.1", 5000, Guid.NewGuid());

        Console.WriteLine("MQClient se ejecutó correctamente.");

        Console.WriteLine("Presiona Enter para salir...");
        Console.ReadLine();
    }
}
