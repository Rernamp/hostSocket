using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace hostSocket {
    public class OneMicrophone : BaseConnect {
        public OneMicrophone(int numberRepeat, byte numberMicrophone) {
            this.numberMicrophone = numberMicrophone;
            this.numberRepeat = numberRepeat;
        }
        public override void Connect(NetworkStream stream, StreamWriter fileStream) {
            byte[] data = { magicValue, (byte)TypeConnect.OneMicrophone, numberMicrophone};

            stream.Write(data, 0, data.Count());

            byte[] buffer = new byte[2048];

            while (numberRepeat >= 0) {
                var size = stream.Read(buffer);
                if (size != 0) {
                    
                    Int16 dataValue = 0;
                    for (var i = 0; i < size / sizeof(Int16); i++) {
                        dataValue = (Int16)BitConverter.ToInt16(buffer, (i * sizeof(Int16)));
                            
                        WriteStringToFile(fileStream, dataValue.ToString()).Wait();
                    }
                    numberRepeat -= size;
                }
                Console.WriteLine($"New Data with size: {size}");
            }
        }

        private byte numberMicrophone;
        private int numberRepeat;
    }
}
