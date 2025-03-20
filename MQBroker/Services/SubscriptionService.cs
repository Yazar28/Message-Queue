using System;

namespace MQBroker.Services
{
    public class NodoSuscriptor
    {
        public string AppId { get; }
        public NodoSuscriptor? Siguiente { get; set; }

        public NodoSuscriptor(string appId, NodoSuscriptor? siguiente = null)
        {
            AppId = appId;
            Siguiente = null;
        }
    }

    public class SubscriptionService
    {
        private NodoSuscriptor? cabeza;

        public void Subscribe(string appId)
        {
            if (IsSubscribed(appId))
            {
                Console.WriteLine($"Usuario {appId} ya está suscrito.");
                return;
            }

            var nuevo = new NodoSuscriptor(appId) { Siguiente = cabeza };
            cabeza = nuevo;

            Console.WriteLine($"Usuario {appId} se ha suscrito correctamente.");
        }

        public void Unsubscribe(string appId)
        {
            if (cabeza == null)
            {
                Console.WriteLine($"Usuario {appId} no está suscrito.");
                return;
            }

            if (cabeza.AppId == appId)
            {
                cabeza = cabeza.Siguiente;
                Console.WriteLine($"Usuario {appId} se ha desuscrito correctamente.");
                return;
            }

            NodoSuscriptor? actual = cabeza;
            while (actual?.Siguiente != null && actual.Siguiente.AppId != appId)
            {
                actual = actual.Siguiente;
            }

            if (actual?.Siguiente != null)
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
                Console.WriteLine($"Usuario {appId} se ha eliminado correctamente.");
            }
            else
            {
                Console.WriteLine($"Usuario {appId} no está suscrito.");
            }
        }

        public bool IsSubscribed(string appId)
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
    }
}
