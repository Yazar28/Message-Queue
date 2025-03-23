using System;
using System.Threading;
using MQBroker.Networking;
using MQClient.Library;
using MQClient.Models;

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

        Guid appId = Guid.NewGuid();
        using var client = new MQClient.Library.MQClient("127.0.0.1", 5000, appId);

        var topic = new Topic("testTopic");

        if (client.Subscribe(topic))
            Console.WriteLine($"Suscripción exitosa a '{topic}'");
        else
            Console.WriteLine($"No se pudo suscribir a '{topic}'");

        Thread.Sleep(2000);

        if (client.Publish(topic, "¡Mensaje de prueba desde MQClient!"))
            Console.WriteLine($"Mensaje publicado en '{topic}'");
        else
            Console.WriteLine($"No se pudo publicar en '{topic}'");

        Thread.Sleep(2000);

        string? receivedMessage = client.Receive(topic);
        if (receivedMessage != null)
            Console.WriteLine($"Mensaje recibido en '{topic}': {receivedMessage}");
        else
            Console.WriteLine($"No se recibieron mensajes en '{topic}'");

        Thread.Sleep(2000);

        if (client.Unsubscribe(topic))
            Console.WriteLine($"Desuscripción exitosa de '{topic}'");
        else
            Console.WriteLine($"No se pudo desuscribir de '{topic}'");

        Console.WriteLine("MQClient se ejecutó correctamente.");
        Console.WriteLine("Presiona Enter para salir...");
        Console.ReadLine();
    }
}
