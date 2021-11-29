using System;
using System.Net;
using System.Net.Sockets;

using Calculator;

namespace Server
{
    class Program
    {
        static void Main()
        {
            RunServer();

            Console.WriteLine("\nPress a key to continue...");
            Console.Read();
        }

        static void RunServer()
        {
            // RPN manager of the calculator
            RPNManager man = new RPNManager(4);

            // Input stream data buffer and bytes received
            byte[] inBuffer = new byte[1024];
            int inBytes = 0;

            // Sets up the remote end point to match the localhost on port 11000
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEP = new IPEndPoint(ipAddress, 11000);

            // Creates the TCP/IP socket
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            
            try {
                // Binds the socket to the end point and starts listening
                listener.Bind(localEP);
                listener.Listen(10);

                while (true) {
                    // Prepares the server for a new connection
                    Console.WriteLine("Waiting for a connection...");
                    Socket handler = listener.Accept();
                    inBuffer = new byte[1024];
                    inBytes = 0;

                    // Receives the data from the end point
                    inBytes = handler.Receive(inBuffer);
                    string code = System.Text.Encoding.ASCII.GetString(inBuffer, 0, inBytes);

                    // Prepares the data to be sent
                    string result;
                    try {
                        man.Interpret(code);
                        result = man.Calculator.ToString();
                    }
                    catch (Exception){
                        result = "Inserted instructions could not be interpreted.";
                    }
                    byte[] outBytes = System.Text.Encoding.ASCII.GetBytes(result);

                    // Sends the data to the end point
                    handler.Send(outBytes);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }
}