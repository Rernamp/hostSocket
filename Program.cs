
namespace Application {
    using System.Net.Sockets;
    using System.Net;
    using System.Diagnostics;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    class Program {
        static void Main(string[] args) {
            using TcpClient tcpClient = new TcpClient();
            var server = "www.google.com";
            string ip = "192.168.3.21";

            tcpClient.ConnectAsync(IPAddress.Parse(ip), 80).Wait();
            // получаем поток для взаимодействия с сервером
            NetworkStream stream = tcpClient.GetStream();

            const byte magicValue = 0xE6;

            byte[] data = { magicValue, 0 };

            var recBuffer = new byte[1];

            var sizeRec = stream.Read(recBuffer);

            if (sizeRec == 1) {
                if (recBuffer[0] != magicValue) {
                    return;
                }
            }

            stream.Write(data, 0, data.Count());

            byte[] buffer = new byte[2048];
            int numberRepeat = 255;
            var watch = System.Diagnostics.Stopwatch.StartNew();


            var streamFile = File.CreateText("damp.txt");


            while (numberRepeat != 0) {
                
                var size = stream.Read(buffer);
                if (size != 0)
                {
                    numberRepeat--;
                    string value = System.Text.Encoding.UTF8.GetString(buffer);

                    Int16 dataValue = 0;

                    //for (var i = 0; i < size / 2; i++) {
                    //    dataValue = (Int16)BitConverter.ToUInt16(buffer, (i * sizeof(UInt16)));
                    //    Console.Write(dataValue.ToString());
                    //    Console.Write("  ");
                    //    streamFile.WriteLine(dataValue.ToString());
                    //}

                    for (int i = 0; i < size; i++) {
                        Console.WriteLine($"data: {buffer[i]}");
                    }
                    Console.WriteLine($"New Data {numberRepeat}; Size: {size}");
                } else {
                    Console.WriteLine("empty packet");
                }
            }
            watch.Stop();

            Console.WriteLine($"Time ns: {watch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))}");

            tcpClient.Close();

            Thread.Sleep(1000);
        }
    }
}