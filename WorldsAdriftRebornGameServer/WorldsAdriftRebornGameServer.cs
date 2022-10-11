using System.Runtime.InteropServices;
using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Game;
using WorldsAdriftRebornGameServer.Networking.Singleton;

namespace WorldsAdriftRebornGameServer
{
    internal class WorldsAdriftRebornGameServer
    {
        [PInvoke(typeof(EnetLayer.ENet_Poll_Callback))]
        private unsafe static void OnNewClientConnected(IntPtr peer )
        {
            ENetPeerHandle ePeer = new ENetPeerHandle(peer, new ENetHostHandle());
            if (!ePeer.IsInvalid)
            {
                Console.WriteLine("[info] got a connection.");
                PeerManager.Instance.playerState.Add(ePeer, Game.GameState.State.NEWLY_CONNECTED);
            }
        }

        private static readonly EnetLayer.ENet_Poll_Callback callback = new EnetLayer.ENet_Poll_Callback(OnNewClientConnected);

        static unsafe void Main( string[] args )
        {
            if (EnetLayer.ENet_Initialize() < 0)
            {
                Console.WriteLine("[error] failed to initialize ENet.");
                return;
            }

            Console.WriteLine("[info] successfully initialized ENet.");
            ENetHostHandle server = EnetLayer.ENet_Create_Host(7777, 1, 1, 0, 0);

            if (server.IsInvalid)
            {
                Console.WriteLine("[error] failed to create host and listen on network interface.");

                EnetLayer.ENet_Deinitialize(new IntPtr(0));
                return;
            }

            Console.WriteLine("[info] successfully initialized networking, now waiting for connections and data.");
            PeerManager.Instance.SetENetHostHandle(server);

            string content = "Hello back from our own Worlds Adrift server!";
            IntPtr answer = Translator.stringToIntPtr(content);

            while (true)
            {
                EnetLayer.ENetPacket_Wrapper* packet = EnetLayer.ENet_Poll(server, 50, Marshal.GetFunctionPointerForDelegate(callback), new IntPtr(0));
                if(packet != null)
                {
                    // commented out to not spam the log file and fill your disk ;)
                    //Console.WriteLine("[info] got a valid packet, data is: " + Translator.FromUtf8Cstr(packet->data, packet->dataLength));

                    foreach (KeyValuePair<ENetPeerHandle, GameState.State> keyValuePair in PeerManager.Instance.playerState)
                    {
                        EnetLayer.ENet_Send(keyValuePair.Key, 0, answer.ToPointer(), content.Length, 1);
                    }
                }
            }

            // sad its never reached
            Marshal.FreeHGlobal(answer);
        }
    }
}
