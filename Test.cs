using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace hostSocket {
    public class Test :  BaseConnect {
        public Test(int numberRepeat) : base() {
            this.numberRepeat = numberRepeat;
        }

        public override void Connect(NetworkStream stream, StreamWriter fileStream) {
            byte[] data = { magicValue, (byte)TypeConnect.Test };

            stream.Write(data, 0, data.Count());

            byte[] buffer = new byte[2048];

            while (numberRepeat >= 0) {
                var size = stream.Read(buffer);
                if (size != 0) {
                    for (int i = 0; i < size; i++) {
                        Console.WriteLine(buffer[i].ToString());
                    }
                    numberRepeat -= size;
                    Console.WriteLine($"New Data with size: {size}");
                }

            }
        }
        private int numberRepeat;
    }

        
}
