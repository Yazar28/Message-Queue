using System;
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

                var message = new Message("publish", _appId.ToString(), "testTopic", "Hola, soy MQClient!");
                string jsonMessage = JsonSerializer.Serialize(message);
                byte[] data = Encoding.UTF8.GetBytes(jsonMessage);

                _stream.Write(data, 0, data.Length);
                Console.WriteLine("[MQClient] Mensaje de prueba enviado.");

                byte[] buffer = new byte[1024];
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"[MQClient] Respuesta del servidor: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MQClient] Error al conectar con el servidor: {ex.Message}");
                _client?.Close();
                _client = null;
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
                Console.WriteLine("[MQClient] Conexión cerrada.");
                _disposed = true;
            }
        }

        ~MQClient()
        {
            Dispose();
        }
    }
}
