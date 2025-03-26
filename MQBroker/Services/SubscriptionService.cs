using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;   

namespace MQBroker.Services
{
    public class NodoSuscriptor
    {
        public string AppId { get; }
        public List<string> SubscribedTopics { get; set; } = new List<string>();
        public Queue<string> MessagesQueue { get; set; } = new Queue<string>();
        public NodoSuscriptor? Siguiente { get; set; }

        public NodoSuscriptor(string appId, NodoSuscriptor? siguiente = null)
        {
            AppId = appId;
            Siguiente = siguiente;
        }
    }

    public class DataStorage
    {
        public List<NodoSuscriptor> Suscriptores { get; set; } = new List<NodoSuscriptor>();
    }

    public class DataPersistence
    {
        private static readonly string filePath = @"C:\Users\Usuario\OneDrive\Desktop\Programación\Algoritmo y Estructuras de Datos\Message Queue\Data\data.json";
        private static string FilePath => filePath;

        public static void SaveData(DataStorage data)
        {
            try
            {
                string? directory = Path.GetDirectoryName(FilePath);

                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
                Console.WriteLine($"Datos guardados correctamente en {FilePath}");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error de E/S al guardar datos: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al guardar datos: {ex.Message}");
            }
        }

        public static DataStorage LoadData()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine($"No se encontró el archivo de datos ({FilePath}), creando uno nuevo.");
                    return new DataStorage();
                }

                string json = File.ReadAllText(FilePath);
                DataStorage? data = JsonSerializer.Deserialize<DataStorage>(json);

                if (data == null)
                {
                    Console.WriteLine($"Error al deserializar {FilePath}, creando uno nuevo.");
                    return new DataStorage();
                }

                Console.WriteLine($"Datos cargados correctamente desde {FilePath}");
                return data;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error de E/S al cargar datos: {ioEx.Message}");
                return new DataStorage();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error de JSON al cargar datos: {jsonEx.Message}");
                return new DataStorage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al cargar datos: {ex.Message}");
                return new DataStorage();
            }
        }

    }

    public class SubscriptionService
    {
        private NodoSuscriptor? cabeza;

        public SubscriptionService()
        {
            var data = DataPersistence.LoadData();
            cabeza = CreateLinkedListFromStorage(data.Suscriptores);
        }

        private NodoSuscriptor? CreateLinkedListFromStorage(List<NodoSuscriptor> subscribers)
        {
            if (subscribers == null || subscribers.Count == 0)
            {
                return null;
            }

            NodoSuscriptor? cabeza = null, actual = null;
            foreach (var subscriber in subscribers)
            {
                var nuevo = new NodoSuscriptor(subscriber.AppId)
                {
                    SubscribedTopics = new List<string>(subscriber.SubscribedTopics)
                };

                if (actual == null) cabeza = actual = nuevo;
                else actual = actual.Siguiente = nuevo;
            }
            return cabeza;
        }

        public bool Subscribe(string appId, string topic)
        {
            NodoSuscriptor? suscriptor = GetSubscriber(appId);
            if (suscriptor != null)
            {
                if (suscriptor.SubscribedTopics.Contains(topic))
                {
                    return false;
                }
                suscriptor.SubscribedTopics.Add(topic);
            }
            else
            {
                cabeza = new NodoSuscriptor(appId, cabeza);
                cabeza.SubscribedTopics.Add(topic);
            }
            DataPersistence.SaveData(new DataStorage { Suscriptores = GetAllSubscribers() });
            return true;
        }

        public void Unsubscribe(string appId, string topic)
        {
            NodoSuscriptor? actual = cabeza, anterior = null;
            while (actual != null)
            {
                if (actual.AppId == appId)
                {
                    if (!actual.SubscribedTopics.Contains(topic))
                    {
                        Console.WriteLine($"El usuario {appId} no está suscrito al tema {topic}.");
                        return;
                    }
                    actual.SubscribedTopics.Remove(topic);
                    if (actual.SubscribedTopics.Count == 0)
                    {
                        if (anterior == null) cabeza = actual.Siguiente;
                        else anterior.Siguiente = actual.Siguiente;
                    }
                    DataPersistence.SaveData(new DataStorage { Suscriptores = GetAllSubscribers() });
                    return;
                }
                anterior = actual;
                actual = actual.Siguiente;
            }
        }

        public List<string> GetSubscribersByTopic(string topic)
        {
            List<string> subscribers = new List<string>();
            NodoSuscriptor? actual = cabeza;
            while (actual != null)
            {
                if (actual.SubscribedTopics.Contains(topic))
                    subscribers.Add(actual.AppId);
                actual = actual.Siguiente;
            }
            return subscribers;
        }

        public bool IsSubscribed(string appId, string topic)
        {
            NodoSuscriptor? suscriptor = GetSubscriber(appId);
            return suscriptor != null && suscriptor.SubscribedTopics.Contains(topic);
        }

        public void Publish(string topic, string messageContent)
        {
            var subscribers = GetSubscribersByTopic(topic);

            foreach (var subscriberId in subscribers)
            {
                NodoSuscriptor? actual = cabeza;
                while (actual != null)
                {
                    if (actual.AppId == subscriberId)
                    {
                        actual.MessagesQueue.Enqueue(messageContent);
                        Console.WriteLine($"Mensaje publicado a {subscriberId} en el tema {topic}.");
                        break;
                    }
                    actual = actual.Siguiente;
                }
            }
        }

        public NodoSuscriptor? GetSubscriber(string appId)
        {
            NodoSuscriptor? actual = cabeza;
            while (actual != null)
            {
                if (actual.AppId == appId)
                    return actual;
                actual = actual.Siguiente;
            }
            return null;
        }

        public List<NodoSuscriptor> GetAllSubscribers()
        {
            var subscribers = new List<NodoSuscriptor>();
            NodoSuscriptor? actual = cabeza;
            while (actual != null)
            {
                subscribers.Add(actual);
                actual = actual.Siguiente;
            }
            return subscribers;
        }
    }
}
