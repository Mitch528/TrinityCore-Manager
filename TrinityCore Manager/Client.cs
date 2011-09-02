//    This file is part of TrinityCore Manager.

//    TrinityCore Manager is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    TrinityCore Manager is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with TrinityCore Manager.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace TrinityCore_Manager
{
    class Client
    {
        private Socket client;
        private byte[] data = new byte[2048];

        private string HOST = String.Empty;
        private int PORT = 3443;
        private string USERNAME = String.Empty;
        private string PASSWORD = String.Empty;


        public delegate void ClientConnectedEventHandler(object sender, EventArgs e);
        public delegate void ConnectionFailedEventHandler(object sender, EventArgs e);

        public delegate void ReceiveFailedEventHandler(object sender, EventArgs e);

        public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

        public delegate void SentMessageFailedEventHandler(object sender, EventArgs e);

        public event ClientConnectedEventHandler clientConnected;
        public event ConnectionFailedEventHandler connFailed;

        public event ReceiveFailedEventHandler receiveFailed;

        public event MessageReceivedEventHandler msgReceived;

        public event SentMessageFailedEventHandler sentMSGFailed;

        public class MessageReceivedEventArgs : EventArgs
        {
            public string message { get; set; }

            public MessageReceivedEventArgs(string msg)
            {
                message = msg;
            }
        }

        public Client(string host, int port, string username, string password)
        {
            HOST = host;
            PORT = port;
            USERNAME = username;
            PASSWORD = password;
        }

        public void StartConnection()
        {
            try
            {
                if (client != null && client.Connected)
                    client.Disconnect(true);


                IPAddress ipa = IPAddress.None;

                IPAddress.TryParse(HOST, out ipa);

                if (HOST == "localhost" || HOST == String.Empty)
                {
                    ipa = IPAddress.Parse("127.0.0.1");
                }
                else
                {
                    ipa = Dns.Resolve(HOST).AddressList[0];
                }

                IPEndPoint ipe = new IPEndPoint(ipa, PORT);

                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Create New Socket
                client.NoDelay = true;

                client.BeginConnect(ipe, new AsyncCallback(BeginConnect), client); //Begin Connection
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BeginConnect(IAsyncResult iar)
        {
            try
            {
                client = (Socket)iar.AsyncState;

                client.EndConnect(iar);

                data = new byte[2048];

                if (clientConnected != null)
                    clientConnected(this, new EventArgs());


                client.BeginReceive(data, 0, 2048, SocketFlags.None, new AsyncCallback(BeginReceive), client); //Start Receiving
            }
            catch (SocketException ex)
            {
                if (connFailed != null)
                    connFailed(this, new EventArgs());

                Console.WriteLine(ex.Message);
            }

        }

        private void BeginReceive(IAsyncResult iar)
        {
            try
            {
                if (isConnected())
                {
                    client = (Socket)iar.AsyncState;

                    int bytesRead = client.EndReceive(iar);

                    if (bytesRead != 0)
                    {
                        string message = Encoding.ASCII.GetString(data, 0, bytesRead);

                        msgReceived(this, new MessageReceivedEventArgs(message));
                    }

                    client.BeginReceive(data, 0, 2048, SocketFlags.None, new AsyncCallback(BeginReceive), client);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message + " " + ex.ErrorCode);

                if (receiveFailed != null)
                    receiveFailed(this, new EventArgs());
            }
        }

        private void SendData(IAsyncResult iar)
        {
            try
            {
                Socket socket = (Socket)iar.AsyncState;

                int sent = socket.EndSend(iar);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);

                if (sentMSGFailed != null)
                    sentMSGFailed(this, new EventArgs());
            }
        }

        public void SendMessage(string message)
        {
            try
            {

                if (!isConnected())
                    return;

                byte[] msg = Encoding.ASCII.GetBytes(message + "\n");

                client.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(SendData), client);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool isConnected()
        {
            if (client != null && client.Connected)
                return true;

            return false;
        }

        public void Disconnect()
        {
            if (isConnected())
            {
                try
                {
                    client.Shutdown(SocketShutdown.Both);

                    client.Disconnect(false);

                    //client.Shutdown(SocketShutdown.Both);
                    //client.BeginDisconnect(true, new AsyncCallback(BeginDisconnection), client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void BeginDisconnection(IAsyncResult iar)
        {
            try
            {
                client = (Socket)iar.AsyncState;

                client.EndDisconnect(iar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
