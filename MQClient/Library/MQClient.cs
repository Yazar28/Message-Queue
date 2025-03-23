﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using MQClient.Models;

namespace MQClient.Library
{
    public class MQClient : IDisposable
    {
        private readonly string _ip;
        private readonly int _port;
        private readonly Guid _appId;
        private TcpClient? _client;
        private NetworkStream? _stream;
        private bool _disposed = false;

        public MQClient(string ip, int port, Guid AppID)
        {
            _ip = ip;
            _port = port;
            _appId = AppID;

            try
            {
                Console.WriteLine($"Intentando conectar a MQBroker en {_ip}:{_port}...");
                _client = new TcpClient();
                _client.Connect(_ip, _port);
                _stream = _client.GetStream();
                Console.WriteLine($"Conectado a {_ip}:{_port} con AppID {_appId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con el servidor: {ex.Message}");
                _client?.Close();
                _client = null;
            }
        }

        public bool Subscribe(Topic topic)
        {
            if (_stream == null)
            {
                Console.WriteLine("No hay conexión establecida con el servidor.");
                return false;
            }

            if (topic == null)
            {
                Console.WriteLine("El topic no puede ser nulo.");
                return false;
            }

            return SendMessage(new Message("subscribe", _appId.ToString(), topic.ToString()), "suscrito");
        }

        public bool Unsubscribe(Topic topic)
        {
            if (_stream == null)
            {
                Console.WriteLine("No hay conexión establecida con el servidor.");
                return false;
            }

            if (topic == null)
            {
                Console.WriteLine("El topic no puede ser nulo.");
                return false;
            }

            bool result = SendMessage(new Message("unsubscribe", _appId.ToString(), topic.ToString()), "eliminado");

            Console.WriteLine(result
                ? $"Desuscripción exitosa de '{topic}'."
                : $"No se pudo desuscribir de '{topic}' porque el usuario no estaba suscrito.");

            return result;
        }

        private bool SendMessage(Message message, string expectedResponse)
        {
            try
            {
                if (_stream == null)
                {
                    Console.WriteLine("Error: No hay conexión con el servidor.");
                    return false;
                }

                string jsonMessage = JsonSerializer.Serialize(message);
                byte[] data = Encoding.UTF8.GetBytes(jsonMessage);
                _stream.Write(data, 0, data.Length);
                _stream.Flush();

                byte[] buffer = new byte[1024];
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    Console.WriteLine("No se recibió respuesta del servidor.");
                    return false;
                }

                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                return response.Contains(expectedResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar mensaje: {ex.Message}");
                return false;
            }
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _stream?.Close();
                _client?.Close();
                Console.WriteLine("Conexión cerrada.");
                _disposed = true;
            }
        }
    }
}
