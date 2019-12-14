/*
 * MineStat.cs - A Minecraft server status checker
 *
 * Copyright (C) 2019 TraceLD
 * https://github.com/TraceLD
 *
 * Copyright (C) 2014 Lloyd Dilley
 * http://www.dilley.me/
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 */

using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace TraceLd.MineStatSharp
{
    public class MineStat
    {
        private const ushort DataSize = 512;  // this will hopefully suffice since the MotD should be <=59 characters
        private const ushort NumFields = 6;   // number of values expected from server
        private const int DefaultTimeout = 5; // default TCP timeout in seconds

        public string Address { get; set; }
        public ushort Port { get; set; }
        public int Timeout { get; set; }
        public string Motd { get; set; }
        public string Version { get; set; }
        public string CurrentPlayers { get; set; }
        public string MaximumPlayers { get; set; }
        public bool ServerUp { get; set; }
        public long Latency { get; set; }

        public MineStat(string address, ushort port, int timeout = DefaultTimeout)
        {
            var rawServerData = new byte[DataSize];

            Address = address;
            Port = port;
            Timeout = timeout * 1000;   // milliseconds

            try
            {
                var stopWatch = new Stopwatch();
                var tcpClient = new TcpClient { ReceiveTimeout = Timeout };
                stopWatch.Start();
                tcpClient.Connect(address, port);
                stopWatch.Stop();
                Latency = stopWatch.ElapsedMilliseconds;
                var stream = tcpClient.GetStream();
                var payload = new byte[] { 0xFE, 0x01 };
                stream.Write(payload, 0, payload.Length);
                stream.Read(rawServerData, 0, DataSize);
                tcpClient.Close();
            }
            catch (Exception)
            {
                ServerUp = false;
                return;
            }

            if (rawServerData.Length == 0)
            {
                ServerUp = false;
            }
            else
            {
                var serverData = Encoding.Unicode.GetString(rawServerData).Split("\u0000\u0000\u0000".ToCharArray());
                if (serverData.Length >= NumFields)
                {
                    ServerUp = true;
                    Version = serverData[2];
                    Motd = serverData[3];
                    CurrentPlayers = serverData[4];
                    MaximumPlayers = serverData[5];
                }
                else
                {
                    ServerUp = false;
                }
            }
        }
    }
}
