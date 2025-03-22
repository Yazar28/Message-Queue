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
        private static readonly bool debugMode = true; 

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
                if (debugMode) Console.WriteLine("Cliente conectado...");
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
                    if (debugMode) Console.WriteLine("Cliente desconectado sin enviar datos.");
                    return;
                }

                string requestJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                if (debugMode) Console.WriteLine($"Mensaje recibido: {requestJson}");

                Message? message;
                try
                {
                    message = JsonSerializer.Deserialize<Message>(requestJson);
                }
                catch (JsonException ex)
                {
                    if (debugMode) Console.WriteLine($"Error de deserialización: {ex.Message}\nJSON defectuoso: {requestJson}");
                    message = null;
                }

                string response = message != null ? ProcessMessage(message) : "Error: JSON no válido.";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);

                if (debugMode) Console.WriteLine($"Respuesta enviada: {response}");

                client.Close();
            }
            catch (Exception ex)
            {
                if (debugMode) Console.WriteLine($"Error al manejar cliente: {ex.Message}");
            }
        }

        private string ProcessMessage(Message message)
        {
            return message.Type.ToLower() switch
            {
                "subscribe" => HandleSubscribe(message),
                "unsubscribe" => HandleUnsubscribe(message),
                "publish" => HandlePublish(message),
                "receive" => HandleReceive(message),
                _ => "Tipo de mensaje no soportado."
            };
        }

        private string HandleSubscribe(Message message)
        {
            bool wasAlreadySubscribed = subscriptionService.IsSubscribed(message.AppId, message.Topic);

            subscriptionService.Subscribe(message.AppId, message.Topic);

            return wasAlreadySubscribed
                ? $"El usuario {message.AppId} ya estaba suscrito al tema {message.Topic}."
                : $"Usuario {message.AppId} suscrito al tema {message.Topic}.";
        }

        private string HandleUnsubscribe(Message message)
        {
            bool wasSubscribed = subscriptionService.IsSubscribed(message.AppId, message.Topic);
            subscriptionService.Unsubscribe(message.AppId, message.Topic);

            return wasSubscribed
                ? $"Usuario {message.AppId} eliminado del tema {message.Topic}."
                : $"El usuario {message.AppId} no estaba suscrito al tema {message.Topic}.";
        }

        private string HandlePublish(Message message)
        {
            var subscribers = subscriptionService.GetSubscribersByTopic(message.Topic);
            if (subscribers.Count == 0)
            {
                return $"El tema {message.Topic} no tiene suscriptores o no existe.";
            }

            foreach (var subscriber in subscribers)
            {
                NodoSuscriptor? actual = subscriptionService.GetSubscriber(subscriber);
                if (actual != null && message.Content != null)
                {
                    actual.MessagesQueue.Enqueue(message.Content);
                    if (debugMode) Console.WriteLine($"Mensaje publicado a {subscriber} en el tema {message.Topic}.");
                }
            }

            DataPersistence.SaveData(new DataStorage { Suscriptores = subscriptionService.GetAllSubscribers() });
            return $"Mensaje publicado en el tema {message.Topic}.";
        }

        private string HandleReceive(Message message)
        {
            NodoSuscriptor? actual = subscriptionService.GetSubscriber(message.AppId);

            if (actual == null || !actual.SubscribedTopics.Contains(message.Topic))
            {
                return $"El usuario {message.AppId} no está suscrito al tema {message.Topic}.";
            }

            if (actual.MessagesQueue.Count > 0)
            {
                var receivedMessage = actual.MessagesQueue.Dequeue();
                if (debugMode) Console.WriteLine($"Mensaje recibido por {message.AppId} del tema {message.Topic}: {receivedMessage}");

                DataPersistence.SaveData(new DataStorage { Suscriptores = subscriptionService.GetAllSubscribers() });
                return $"Mensaje recibido por {message.AppId} del tema {message.Topic}: {receivedMessage}";
            }
            else
            {
                return $"No hay mensajes pendientes para {message.AppId} en el tema {message.Topic}.";
            }
        }
    }
}
