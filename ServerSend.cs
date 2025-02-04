﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEAserver
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();

            for (int i = 1; i <= Server.MaxUsers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();

            for (int i = 1; i <= Server.MaxUsers; i++)
            {
                if (_exceptClient != i)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();

            for (int i = 1; i <= Server.MaxUsers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();

            for (int i = 1; i <= Server.MaxUsers; i++)
            {
                if (_exceptClient != i)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int) ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void UDPTest(int _toClient)
        {
            using (Packet _packet = new Packet((int) ServerPackets.udpTest))
            {
                _packet.Write("A test packet for UDP");

                SendUDPData(_toClient, _packet);
            }
        }

        public static void WrongCodeReceived(int _toClient, string _code)
        {
            using (Packet _packet = new Packet((int)ServerPackets.wrongFurnitCode))
            {
                _packet.Write(_code);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void RightCodeReceived(int _toClient, string _url, string _code)
        {
            using (Packet _packet = new Packet((int) ServerPackets.rightFurnitCode))
            {
                _packet.Write(_code);
                _packet.Write(_url + @"/anim.prefab");           // animation's url
                _packet.Write(_url + @"/logo.jpg");           // logo's url
                _packet.Write(_url + @"/descr.txt");           // description's url

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SendNews(int _toClient, int numberOfNews, List<string[]> newsData)
        {
            using (Packet _packet = new Packet((int) ServerPackets.sendNews))
            {
                _packet.Write(numberOfNews);

                foreach (string[] s in newsData)
                {
                    int lineChecker = 0;
                    foreach (string s2 in s)
                    {
                        if (lineChecker == 3)
                        {
                            Console.WriteLine("Some of the news files contain more than 3 lines of information, packet submission may be broken!!");
                            break;
                        }
                        _packet.Write(s2);
                        lineChecker++;
                    }

                    if (lineChecker < 3)
                    {
                        Console.WriteLine("Some of the news files contain less than 3 lines of information, packet submission may be broken!!");
                        
                        for (; lineChecker < 3; lineChecker++)
                        {
                            _packet.Write("");
                        }
                    }
                }
                SendTCPData(_toClient, _packet);
                Console.WriteLine("News data has been sent to the client");
            }
        }
    }
}
