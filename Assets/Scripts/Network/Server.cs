using LiteNetLib;

namespace Server
{
    public class Server
    {
        private readonly EventBasedNetListener listener;
        private readonly NetManager netManager;
        private readonly int maxConnection;
        private readonly string key;

        public Server(int port, int maxConnection, string key)
        {
            listener = new();
            netManager = new(listener);
            listener.ConnectionRequestEvent += OnConnectionRequest;
            listener.PeerConnectedEvent += OnPeerConnected;
            netManager.Start(port);
            this.maxConnection = maxConnection;
            this.key = key;
        }

        public void Update() => netManager.PollEvents();
        public void Shutdown() => netManager.Stop();

        private void OnPeerConnected(NetPeer peer)
        {
             
        }

        private void OnConnectionRequest(ConnectionRequest request)
        {
            if (netManager.ConnectedPeersCount < maxConnection)
            {
                request.AcceptIfKey(key);
                return;
            } 
            
            request.Reject();
        }
    }
}