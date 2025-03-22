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
                    Console.WriteLine($"JSON defectuoso: {requestJson}"); 
                    message = null;
                }

                string response;

                if (message != null)
                {
                    switch (message.Type.ToLower())
                    {
                        case "subscribe":
                            subscriptionService.Subscribe(message.AppId, message.Topic);
                            response = $"Usuario {message.AppId} suscrito al tema {message.Topic}.";
                            break;
                        case "unsubscribe":
                            subscriptionService.Unsubscribe(message.AppId, message.Topic);
                            response = $"Usuario {message.AppId} eliminado del tema {message.Topic}.";
                            break;
                        case "publish":
                            response = HandlePublish(message);
                            break;
                        case "receive":
                            response = HandleReceive(message);
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

                Console.WriteLine($"Respuesta enviada: {response}");

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al manejar cliente: {ex.Message}");
            }
        }

        private string HandlePublish(Message message)
        {
            bool topicExists = subscriptionService.IsSubscribed(message.AppId,message.Topic);
            if (!topicExists)
            {
                return $"El tema {message.Topic} no tiene suscriptores o no existe.";
            }

            var subscribers = subscriptionService.GetSubscribersByTopic(message.Topic);

            foreach (var subscriber in subscribers)
            {
                NodoSuscriptor? actual = subscriptionService.GetSubscriber(subscriber);

                if (actual != null)
                {
                    if (message.Content != null)
                    {
                        actual.MessagesQueue.Enqueue(message.Content);
                    }
                    else
                    {
                        Console.WriteLine($"Mensaje publicado a {subscriber} en el tema {message.Topic}.");
                    }
                }
            }

            return $"Mensaje publicado en el tema {message.Topic}.";
        }

        private string HandleReceive(Message message)
        {
            var subscribers = subscriptionService.GetSubscribersByTopic(message.Topic);

            if (!subscribers.Contains(message.AppId))
            {
                return $"El usuario {message.AppId} no está suscrito al tema {message.Topic}.";
            }

            NodoSuscriptor? actual = subscriptionService.GetSubscriber(message.AppId);

            if (actual != null && actual.MessagesQueue.Count > 0)
            {
                var receivedMessage = actual.MessagesQueue.Dequeue();
                return $"Mensaje recibido por {message.AppId} del tema {message.Topic}: {receivedMessage}";
            }
            else 
            {
                return $"No hay mensajes pendientes para {message.AppId} en el tema {message.Topic}.";
            }
        }
    }
}
