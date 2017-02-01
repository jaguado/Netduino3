using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JAM.Netduino3.Web.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class GatewayHelper:IDisposable
    {
        internal int localPort { set; get; }
        internal TcpHelper helper { set; get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public GatewayHelper(int port)
        {
            localPort = port;
            Start();
        }

        private void Start()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Listen), localPort);
        }


        private void Listen(object port)
        {
            helper = new TcpHelper();
            helper.StartServer((int)port);
            helper.Listen();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (helper != null)
                helper.Dispose();
        }

        internal class TcpHelper:IDisposable
        {
            private TcpListener listener { get; set; }
            private bool accept { get; set; } = false;
            public bool abort { set; get; } = false;

            public void StartServer(int port)
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                accept = true;

                Console.WriteLine($"Server started. Listening to TCP clients at 127.0.0.1:{port}");
            }

            public void Listen()
            {
                if (listener != null && accept)
                {
                    // Continue listening.  
                    while (true && !abort)
                    {
                        Console.WriteLine("Waiting for client...");
                        var clientTask = listener.AcceptTcpClientAsync(); // Get the client  

                        if (clientTask.Result != null)
                        {
                            Console.WriteLine("Client connected. Waiting for data.");
                            var client = clientTask.Result;
                            string message = "";

                            while (message != null && !message.StartsWith("quit"))
                            {
                                byte[] data = Encoding.ASCII.GetBytes("Send next data: [enter 'quit' to terminate] ");
                                client.GetStream().Write(data, 0, data.Length);

                                byte[] buffer = new byte[1024];
                                client.GetStream().Read(buffer, 0, buffer.Length);

                                message = Encoding.ASCII.GetString(buffer);
                                Console.WriteLine(message);
                            }
                            Console.WriteLine("Closing connection.");
                            client.GetStream().Dispose();
                        }
                    }
                }
            }

            public void Dispose()
            {
                abort = true;
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}
