using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RACITELCOM_CS_APP
{
    public class TCPClient
    {
        private static TCPClient instance;
        private TcpClient client;
        private NetworkStream stream;
        private string serverIp = "127.0.0.1";
        // private string serverIp = "192.168.253.218";
        private int port = 8888;
        public string username;

        public event Action<string> OnMessageReceived;

        private TCPClient() { }

        public static TCPClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TCPClient();
                }
                return instance;
            }
        }

        public bool IsConnected { get { return client != null && client.Connected; } }

        public bool Connect(string username)
        {
            try
            {
                this.username = username;

                client = new TcpClient(serverIp, port);
                stream = client.GetStream();

                SendMessage(username);

                ThreadPool.QueueUserWorkItem(ReceiveMessages);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                if (IsConnected)
                {
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    stream.Write(messageBytes, 0, messageBytes.Length);
                }
                else
                {
                }
            }
            catch (Exception)
            {
            }
        }

        public void ReceiveMessages(object state)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while (IsConnected && (bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    OnMessageReceived?.Invoke(receivedMessage);
                }
            }
            catch (Exception)
            {
            }
        }

        public void Disconnect()
        {
            try
            {
                if (IsConnected)
                {
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}