using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEAserver
{    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            // TODO: send player into game
        }

        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine($"Received a packet via UDP. Contains a message: {_msg}");
        }

        public static void FurnituresCodeReceived(int _fromClient, Packet _packet)
        {
            string code = _packet.ReadString();

            Console.WriteLine($"Received a furnitures code: {code}");

            string potentialDir = dbHandler.FindDirectory(code);

            if (potentialDir == "")
            {
                ServerSend.WrongCodeReceived(_fromClient, code);
            }
            else
            {
                ServerSend.RightCodeReceived(_fromClient, potentialDir, code);
            }
        }

        public static void GetNews(int _fromClient, Packet _packet)
        {
            int numberOfNews = newsHandler.GetNumberOfDirectories();

            ServerSend.SendNews(_fromClient, newsHandler.GetDirectory(), numberOfNews);
        }
    }
}
