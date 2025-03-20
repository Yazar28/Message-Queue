using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using MQBroker.Models;
using MQBroker.Services;

namespace MQBroker.Networking
{
    public class BrokerServer
    {
        private TcpListener server;
        private const int Port = 5000;
        private readonly SubscriptionService subscriptionService;
        private readonly bool debugMode = true;

        public BrokerServer()
        {
            server = new TcpListener(IPAddress.Any, Port);
            subscriptionService = new SubscriptionService();
        }

        public void Start()
        {
            server.Start();
            Console.WriteLine($"Servidor iniciado en el puerto {Port}...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Cliente conectado...");
                Task.Run(() => HandleClient(client));
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                // Verificar si el cliente se desconectó sin enviar datos
                if (bytesRead == 0)
                {
                    Console.WriteLine("Cliente desconectado sin enviar datos.");
                    return;
                }

                string requestJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                if (debugMode)
                {
                    Console.WriteLine($"Mensaje recibido RAW: [{requestJson}]");
                }
                else
                {
                    Console.WriteLine($"Mensaje recibido: {requestJson}");
                }

                Message? message;
                try
                {
                    message = JsonSerializer.Deserialize<Message>(requestJson);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error de deserialización: {ex.Message}");
                    Console.WriteLine($"JSON defectuoso: {requestJson}"); // Mostrar JSON incorrecto
                    message = null;
                }

                string response;

                if (message != null)
                {
                    switch (message.Type.ToLower())
                    {
                        case "subscribe":
                            subscriptionService.Subscribe(message.AppId);
                            response = $"Usuario {message.AppId} suscrito al tema {message.Topic}.";
                            break;
                        case "unsubscribe":
                            subscriptionService.Unsubscribe(message.AppId);
                            response = $"Usuario {message.AppId} eliminado del tema {message.Topic}.";
                            break;
                        default:
                            response = "Tipo de mensaje no soportado.";
                            break;
                    }
                }
                else
                {
                    response = "Error: JSON no válido.";
                }

                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);

                // Imprimir la respuesta enviada al cliente
                Console.WriteLine($"Respuesta enviada: {response}");

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al manejar cliente: {ex.Message}");
            }
        }
    }
}
