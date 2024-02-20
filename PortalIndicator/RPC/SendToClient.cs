using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalIndicator.RPC
{
    internal static class SendToClient
    {
        public static void Resync(ZPackage pkg, string reason)
        {
            Log.Info($"Sending all portals to everybody, because: {reason}");
            ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.Everybody, RPCManager.RPC_RESYNC, pkg, reason);
        }
    }
}
