﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalIndicator.RPC;
using PortalIndicator;

namespace PortalIndicator.RPC
{
    internal static class Client
    {
        public static void RequestSync(string reason)
        {
            Log.Debug($"Asking server for a sync request, because: {reason}");
            ZRoutedRpc.instance.InvokeRoutedRPC(Environment.ServerPeerId, RPCManager.RPC_SYNCREQUEST, reason);
        }

        public static void ReceiveResync(long sender, ZPackage pkg)
        {
            //if (Environment.IsServer)
            //{
            //    Log.Info("Ignoring resync package, im a server");
            //    return;
            //}

            PortalIndicator.UpdateFromPackage(pkg);
        }
    }
}
