using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main()
        {
            string? code = string.Empty;
            string response = string.Empty;

            while (code != null) {
                Console.WriteLine("Insert code to execute on RPN:");
                code = Console.ReadLine();

                if (code != null) {
                    response = ExecuteData(code);
                    Console.WriteLine(response);
                }
            }
        }

        static string ExecuteData(string code)
        {
            // Input stream data buffer and bytes received
            byte[] inBuffer = new byte[1024];
            int inBytes = 0;

            try {
                // Sets up the remote end point to match the localhost on port 11000
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Creates the TCP/IP socket
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                
                try {
                    // Enstablishes the connection with the chosen end point
                    sender.Connect(remoteEP);
                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint);

                    // Sets up the message and sends it to the remote device
                    byte[] outBuffer = System.Text.Encoding.ASCII.GetBytes(code);
                    int outBytes = sender.Send(outBuffer);

                    // Receives the response from the remote device
                    inBytes = sender.Receive(inBuffer);
                    
                    // Releases the socket
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch (ArgumentNullException e) {
                    Console.WriteLine("ArgumentNullException: " + e.ToString());
                }
                catch (SocketException e) {
                    Console.WriteLine("SocketException: " + e.ToString());
                }
                catch (Exception e) {
                    Console.WriteLine("Unexpected exception: " + e.ToString());
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

            // Return the received data
            return System.Text.Encoding.ASCII.GetString(inBuffer, 0, inBytes);
        }
    }
}