using System;

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

    public class SubscriptionService
    {
        private NodoSuscriptor? cabeza;

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
            Console.WriteLine($"Usuario {appId} no está suscrito.");    
        }

        public bool IsSubscribed(string appId, string topic)
        {
            NodoSuscriptor? actual = cabeza;
            while (actual != null)
            {
                if (actual.AppId == appId)
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
    }
}
