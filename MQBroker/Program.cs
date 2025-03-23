using System;
using MQBroker.Networking;

class Program
{
    static void Main()
    {
        Console.WriteLine("Iniciando el Servidor MQBroker...");
        BrokerServer broker = new BrokerServer();
        broker.Start(); 
    }
}