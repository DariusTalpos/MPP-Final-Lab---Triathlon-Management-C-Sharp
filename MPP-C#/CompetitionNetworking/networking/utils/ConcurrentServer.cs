using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionNetworking.networking.utils
{
    public abstract class ConcurrentServer : AbstractServer
    {
        public ConcurrentServer(string host, int port) : base(host, port)
        { }

        public override void processRequest(TcpClient client)
        {
            Thread t = createWorker(client);
            t.Start();
        }

        protected abstract Thread createWorker(TcpClient client);
    }
}
