using System.Net.Sockets;
using System.Text;
using NetCoreServer;

namespace WorldsAdriftServer.Handlers
{
    internal class gRPC_Proxy_Session : TcpSession
    {
        public gRPC_Proxy_Session( TcpServer server ) : base(server) { }
        protected override void OnConnected()
        {
            Console.WriteLine("GOT TCP CONNECTION");
        }
        /*
        protected override void OnHandshaked()
        {
            Console.WriteLine("SSL HANDSHAKE COMPLETED");
        }
        */
        protected override void OnDisconnected()
        {
            Console.WriteLine("TCP DISCONNECTED");
        }
        protected override void OnReceived( byte[] buffer, long offset, long size )
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);

            Console.WriteLine("-----------");
            Console.WriteLine(message);
            Console.WriteLine("-----------");
            FileStream stream = File.Open("C:\\Users\\max\\gRPC.bin", FileMode.OpenOrCreate);
            for(long i = offset; i < size; i++)
            {
                stream.WriteByte(buffer[i]);
            }
            stream.Flush();
            stream.Close();
            Console.WriteLine("-----------");
        }
        protected override void OnError( SocketError error )
        {
            Console.WriteLine("TCP ERROR");
            Console.WriteLine(error.ToString());
        }
    }
}
