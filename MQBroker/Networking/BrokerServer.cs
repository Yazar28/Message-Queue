using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MQBroker.Networking
{
    public class BrokerServer
    {
        private TcpListener server;
        private const int Port = 5000;

        public BrokerServer()
        {
            server = new TcpListener(IPAddress.Any, Port);
        }

        public void Start()
        {
            server.Start();
            Console.WriteLine($"Server started on port {Port}...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected...");
                Task.Run(() => HandleClient(client));
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;
                StringBuilder receivedMessage = new StringBuilder();

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    receivedMessage.Append(data);

                    if (data.Contains("\n") || data.Contains("\r"))
                    {
                        break;
                    }
                }

                string finalMessage = receivedMessage.ToString().Trim(); 
                Console.WriteLine($"Mensaje recibido: {finalMessage}");

                string response = "Mensaje recibido correctamente.";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);

                client.Close(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al manejar cliente: {ex.Message}");
            }
        }
    }
}