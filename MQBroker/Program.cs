using System;
using MQBroker.Networking;

class Program
{
    static void Main()
    {
        BrokerServer server = new BrokerServer();
        server.Start();
    }
}