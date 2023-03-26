
namespace Application {
    using System.Net.Sockets;
    using System.Net;

    class Program {
        static void Main(string[] args) {
            
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string ip = "192.168.3.207";
            socket.Connect(IPAddress.Parse(ip), 80);

            byte[] buffer = new byte[1024];
            int numberRepeat = 10000;
            while (numberRepeat != 0) {
                numberRepeat--;
                if (socket.Receive(buffer) != 0) {
                    string value = System.Text.Encoding.UTF8.GetString(buffer);

                    Console.WriteLine(value);

                    
                }
                Thread.Sleep(1);
            }

            socket.Close();
        }
    }
}