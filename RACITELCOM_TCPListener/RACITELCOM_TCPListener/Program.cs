using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace RACITELCOM_TCPListener
{
    public class Server
    {
        private static List<ClientInfo> clients = new List<ClientInfo>();
        private static TcpListener listener;

        public static void StartServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            // IPAddress ipAddress = IPAddress.Parse(serverIPAddress);
            // listener = new TcpListener(ipAddress, port);
            listener.Start();
            Console.WriteLine($"Server started. Listening on port {port}...");

            ThreadPool.QueueUserWorkItem(AcceptClients);
        }

        public class ClientInfo
        {
            public TcpClient TcpClient { get; }
            public string Username { get; }

            public ClientInfo(TcpClient tcpClient, string username)
            {
                TcpClient = tcpClient;
                Username = username;
            }
        }

        private static string AuthenticateClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string username = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
            return username;
        }

        private static void AcceptClients(object state)
        {
            while (true)
            {
                TcpClient tcpclients = listener.AcceptTcpClient();

                string username = AuthenticateClient(tcpclients);

                if (username != null)
                {
                    clients.Add(new ClientInfo(tcpclients, username));

                    Console.WriteLine($"{username} is connected.");
                    BroadcastMessage($"{username} is connected.");

                    ThreadPool.QueueUserWorkItem(HandleClient, tcpclients);
                }
                else
                {
                    tcpclients.Close();
                }
            }
        }

        private static void HandleClient(object state)
        {
            TcpClient client = (TcpClient)state;
            NetworkStream stream = client.GetStream();

            // SendMessageToClient(client, "Welcome to the server!");

            ClientInfo clientInfo = clients.Find(c => c.TcpClient == client);
            string username = clientInfo.Username;

            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"{username}: {message}");

                    BroadcastMessage(message);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Client forcibly closed connection: {ex.Message}");
            }
            finally
            {
                clients.Remove(clientInfo);
                Console.WriteLine($"{username} disconnected.");
                BroadcastMessage($"{username} disconnected.");
                try
                {
                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error closing client connection: {ex.Message}");
                }
            }
        }

        private static void SendMessageToClient(TcpClient client, string message)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message to client: {ex.Message}");
            }
        }

        private static void BroadcastMessage(string message)
        {
            try
            {
                foreach (var clienInfo in clients)
                {
                    SendMessageToClient(clienInfo.TcpClient, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error broadcasting message to client: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            // string serverIPAddress = "192.168.1.20";
            int port = 8888;

            StartServer(port);

            Console.WriteLine($"Press Enter to stop the server on port {port}...");
            Console.ReadLine();
        }
    }
}