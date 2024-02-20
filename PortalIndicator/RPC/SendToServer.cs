using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalIndicator.RPC
{
    internal static class SendToServer
    {
        /// <summary>
        /// Ask the server to distribute the list of KnownPortals
        /// </summary>
        /// <param name="reason">The reason for the Resync Request</param>
        public static void SyncRequest(string reason)
        {
            Log.Info($"Asking server for a sync request, because: {reason}");
            Log.Info($"Environment Peer ID: {Environment.ServerPeerId}");
            ZRoutedRpc.instance.InvokeRoutedRPC(Environment.ServerPeerId, RPCManager.RPC_SYNCREQUEST, reason);
        }
    }
}