using System;
using MQClient.Library;

class TestClient
{
    static void Main()
    {
        Console.WriteLine("🚀 Ejecutando Test de MQClient...");
        var client = new MQClient.Library.MQClient("127.0.0.1", 5000, Guid.NewGuid());
        client.Close();
        Console.WriteLine("✅ Prueba de MQClient finalizada.");
    }
}
