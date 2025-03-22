using System;
using System.Text.Json;
using System.IO;

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
        private static readonly string filePath = @"C:\Users\Usuario\OneDrive\Desktop\Programación\Algoritmo y Estructuras de Datos\Proyecto#1\MQBroker\Data\data.json";

        public static void SaveData(DataStorage data)
        {
            try
            {
                string? directory = Path.GetDirectoryName(filePath);

                if (!string.IsNullOrEmpty(directory))
                {
                    Console.WriteLine($"Ruta donde se intenta guardar el archivo: {directory}");

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                        Console.WriteLine("Carpeta creada correctamente.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: La ruta del archivo no es válida.");
                    return;
                }

                string json = JsonSerializer.Serialize(data);
                File.WriteAllText(filePath, json);
                Console.WriteLine("Datos guardados correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar datos: {ex.Message}");
            }
        }

        public static DataStorage LoadData()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No se encontró el archivo, creando nuevo.");
                    return new DataStorage();
                }

                string json = File.ReadAllText(filePath);
                Console.WriteLine("Datos cargados correctamente.");
                return JsonSerializer.Deserialize<DataStorage>(json) ?? new DataStorage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar datos: {ex.Message}");
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

            NodoSuscriptor? cabeza = null;
            NodoSuscriptor? actual = null;

            foreach (var subscriber in subscribers)
            {
                var newMode = new NodoSuscriptor(subscriber.AppId);

                if (cabeza == null)
                {
                    cabeza = newMode;
                    actual = cabeza;
                }
                else
                {
                    if (actual != null)
                    {
                        actual.Siguiente = newMode;
                    }
                    actual = newMode;
                }
            }
            return cabeza;
        }

        public bool IsTopicSubscribed(string appId, string topic)
        {
            NodoSuscriptor? actual = cabeza;
            while (actual != null)
            {
                if (actual.AppId == appId && actual.SubscribedTopics.Contains(topic))
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        public List<string> GetSubscribersByTopic(string topic)
        {
            List<string> subscribers = new List<string>();
            NodoSuscriptor? actual = cabeza;

            while (actual != null)
            {
                if (actual.SubscribedTopics.Contains(topic))
                {
                    subscribers.Add(actual.AppId);
                }
                actual = actual.Siguiente;
            }
            return subscribers;
        }

        public void Subscribe(string appId, string topic)
        {
            if (IsSubscribed(appId, topic))
            {
                Console.WriteLine($"Usuario {appId} ya está suscrito al tema {topic}.");
                return;
            }

            var nuevo = new NodoSuscriptor(appId) { Siguiente = cabeza };
            cabeza = nuevo;
            nuevo.SubscribedTopics.Add(topic);
            DataPersistence.SaveData(new DataStorage { Suscriptores = GetAllSubscribers() });
            Console.WriteLine($"Usuario {appId} se ha suscrito al tema {topic} correctamente.");
        }

        public void Unsubscribe(string appId, string topic)
        {
           if (cabeza == null)
           {
                Console.WriteLine($"Usuario {appId} no está suscrito.");
                return;
           }

           NodoSuscriptor? actual = cabeza;
            while (actual != null)
            {
                if (actual.AppId == appId)
                {
                    if (actual.SubscribedTopics.Contains(topic))
                    {
                        actual.SubscribedTopics.Remove(topic);
                        Console.WriteLine($"Usuario {appId} se ha desuscrito del tema {topic}.");

                        if (actual.SubscribedTopics.Count == 0)
                        {
                            if (cabeza == actual)
                            {
                                cabeza = actual.Siguiente;
                            }
                            else
                            {
                                NodoSuscriptor? anterior = cabeza;
                                while (anterior?.Siguiente != actual)
                                {
                                    anterior = anterior?.Siguiente;
                                }

                                if (anterior != null)
                                {
                                    anterior.Siguiente = actual.Siguiente;
                                }
                            }
                            Console.WriteLine($"Usuario {appId} ha sido completamente eliminado, ya no tiene temas.");
                        }
                        DataPersistence.SaveData(new DataStorage { Suscriptores = GetAllSubscribers() });
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Usuario {appId} no está suscrito al tema {topic}.");
                        return;
                    }
                }
                actual = actual.Siguiente;
            }
            DataPersistence.SaveData(new DataStorage { Suscriptores = GetAllSubscribers() });
            Console.WriteLine($"Usuario {appId} no está suscrito.");    
        }

        public bool IsSubscribed(string appId, string topic)
        {
            NodoSuscriptor? actual = cabeza;
            while (actual != null)
            {
                if (actual.AppId == appId && actual.SubscribedTopics.Contains(topic))
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
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
                {
                    return actual;
                }
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
