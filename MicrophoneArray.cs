using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace hostSocket {
    public class MicrophoneArray : BaseConnect {
        public enum TypeProcessing : byte {
            Average = 0,
            StreamAllSignal,
            AdaptiveAlgoritm
        }
        public MicrophoneArray(int numberRepeat, byte numberMicrophone, TypeProcessing type, byte filterOrder = 32) {
            this.numberMicrophone = numberMicrophone;
            this.numberRepeat = numberRepeat;
            this.type = type;
            this.filterOrder = filterOrder;
        }

        public override void Connect(NetworkStream stream, StreamWriter fileStream) {
            byte[] data = { magicValue, (byte)TypeConnect.MicrophoneArray, numberMicrophone, (byte)type, filterOrder };

            stream.Write(data, 0, data.Count());

            byte[] buffer = new byte[2048];
            if (type == TypeProcessing.AdaptiveAlgoritm) {
                numberRepeat *= sizeof(float);
            }

            while (numberRepeat >= 0) {
                var size = stream.Read(buffer);
                if (size != 0) {

                    if (type == TypeProcessing.AdaptiveAlgoritm) {
                        float dataValue = 0;
                        for (var i = 0; i < size / sizeof(float); i++) {
                            dataValue = (float)BitConverter.ToSingle(buffer, (i * sizeof(float)));

                            WriteStringToFile(fileStream, dataValue.ToString().Replace(",", ".")).Wait();
                        }
                    } else {
                        Int16 dataValue = 0;
                        for (var i = 0; i < size / sizeof(Int16); i++) {
                            dataValue = (Int16)BitConverter.ToInt16(buffer, (i * sizeof(Int16)));

                            WriteStringToFile(fileStream, dataValue.ToString()).Wait();
                        }
                    }
                    
                    numberRepeat -= size;
                }
                Console.WriteLine($"New Data with size: {size}");

            }
        }

        private byte numberMicrophone;
        private int numberRepeat;
        private TypeProcessing type;
        private byte filterOrder;

    }
}
