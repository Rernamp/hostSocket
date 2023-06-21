
namespace Application {
    using System.Net.Sockets;
    using System.Net;
    using System.Diagnostics;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using hostSocket;

    enum TypeConnect : byte {
        Test = 0,
        OneMicrophone,
        MicrophoneArray
    }
    class Program {
        static void Main(string[] args) {
            BaseConnect connect = null;                        
            
            if (args.Length < 3) {
                throw new ArgumentException("Invalid number arguments");
            }

            string pathToDamp = args[0];
            TypeConnect typeConnection = (TypeConnect)(Byte.Parse(args[1]));
            int numberRepeat = int.Parse(args[2]);

            switch (typeConnection) {
                case TypeConnect.Test:
                    connect = new Test(numberRepeat);
                    break;
                case TypeConnect.OneMicrophone: {
                        byte numberMicrophone = Byte.Parse(args[3]);
                        connect = new OneMicrophone(numberRepeat, numberMicrophone);
                        break;
                    }
                    
                case TypeConnect.MicrophoneArray: {
                        byte numberMicrophone = Byte.Parse(args[3]);
                        MicrophoneArray.TypeProcessing typeProcessing = (MicrophoneArray.TypeProcessing)Byte.Parse(args[4]);
                        connect = new MicrophoneArray(numberRepeat, numberMicrophone, typeProcessing);
                        break;
                    }
                default:
                    throw new ArgumentException("Invalide type connection");
            }

            connect.Connect(pathToDamp);


            Thread.Sleep(1000);
        }

        private static async Task WriteStringToFile(StreamWriter stream, string data) {            
            await stream.WriteLineAsync(data);
        }
    }
}