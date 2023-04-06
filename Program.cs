
namespace Application {
    using System.Net.Sockets;
    using System.Net;

    class Program {
        static void Main(string[] args) {
            
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string ip = "192.168.3.207";
            socket.Connect(IPAddress.Parse(ip), 80);

            byte[] data = { 0xFE };

            var res = socket.Send(data, SocketFlags.None);

            Console.WriteLine(res);

            byte[] buffer = new byte[1024];
            int numberRepeat = 10000;
            while (numberRepeat != 0) {
                numberRepeat--;
                var size = socket.Receive(buffer);
                if (size != 0) {
                   string value = System.Text.Encoding.UTF8.GetString(buffer);
 
                    for (var i = 0; i < size; i++) 
                    {
                        Console.Write($"{buffer[i]} ");                        
                    }
                    Console.WriteLine("");



                }
                Thread.Sleep(1);
            }

            socket.Close();
        }
    }
}