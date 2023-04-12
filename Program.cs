
namespace Application {
    using System.Net.Sockets;
    using System.Net;
    using System.Diagnostics;

    class Program {
        static void Main(string[] args) {
            
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string ip = "192.168.3.21";
            socket.Connect(IPAddress.Parse(ip), 80);
            

            byte[] data = { 0xFE, 10 };

            var res = socket.Send(data, SocketFlags.None);

            Console.WriteLine(res);

            byte[] buffer = new byte[1024];
            int numberRepeat = data[1] + 1;
            var watch = System.Diagnostics.Stopwatch.StartNew();


            while (numberRepeat != 0) {
                numberRepeat--;
                var size = socket.Receive(buffer);
                if (size != 0)
                {
                    //string value = System.Text.Encoding.UTF8.GetString(buffer);

                    for (var i = 0; i < size; i++)
                    {
                        Console.Write($"{buffer[i]} ");
                    }
                    Console.WriteLine($"New Data {data[1] + 1 - numberRepeat} ");
                }
            }
            watch.Stop();

            Console.WriteLine($"Time ns: {watch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))}");

            socket.Close();
        }
    }
}