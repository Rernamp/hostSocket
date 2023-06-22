using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;


namespace hostSocket {
    public abstract class BaseConnect {
        public BaseConnect() {

        }

        public void Connect(string pathToDamp) {
            tcpClient.ConnectAsync(IPAddress.Parse(ip), 80).Wait();
            // получаем поток для взаимодействия с сервером
            NetworkStream stream = tcpClient.GetStream();

            var recBuffer = new byte[1];

            var sizeRec = stream.Read(recBuffer);


            if (sizeRec == 1) {
                if (recBuffer[0] != magicValue) {
                    return;
                }
            }

            var streamFile = File.CreateText(pathToDamp);
            Connect(stream, streamFile);

            tcpClient.Close();
            streamFile.Close();
        }

        public abstract void Connect(NetworkStream stream, StreamWriter fileStream);
        public TcpClient tcpClient = new TcpClient();
        private const string ip = "192.168.3.21";
        private const int port = 80;
        protected const byte magicValue = 0xE6;
        protected static async Task WriteStringToFile(StreamWriter stream, string data) {
            await stream.WriteLineAsync(data);
        }
    }
}
