
namespace Application {
    using System.Net.Sockets;
    using System.Net;
    using System.Diagnostics;
    using System;
    using System.Runtime.CompilerServices;

    class Program {
        static void Main(string[] args) {
            
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string ip = "192.168.3.21";
            socket.Connect(IPAddress.Parse(ip), 80);
            
            const byte magicValue = 0xE6;

            byte[] data = { magicValue, 0 };

            var recBuffer = new byte[1];

            var sizeRec = socket.Receive(recBuffer);

            if (sizeRec == 1) {
                if (recBuffer[0] != magicValue) {
                    return;
                }
            }

            var res = socket.Send(data, SocketFlags.None);

            Console.WriteLine(res);

            byte[] buffer = new byte[2048];
            int numberRepeat = 255;
            var watch = System.Diagnostics.Stopwatch.StartNew();


            var stream = File.CreateText("damp.txt");


            while (numberRepeat != 0) {
                
                var size = socket.Receive(buffer);
                if (size != 0)
                {
                    numberRepeat--;
                    string value = System.Text.Encoding.UTF8.GetString(buffer);

                    Int16 dataValue = 0;

                    //for (var i = 0; i < size / 2; i++) {
                    //    dataValue = (Int16)BitConverter.ToUInt16(buffer, (i * sizeof(UInt16)));
                    //    Console.Write(dataValue.ToString());
                    //    Console.Write("  ");
                    //    stream.WriteLine(dataValue.ToString());
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

            res = socket.Send(data, SocketFlags.None);

            Console.WriteLine($"Time ns: {watch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))}");

            socket.Close();

            Thread.Sleep(1000);
        }
    }
}