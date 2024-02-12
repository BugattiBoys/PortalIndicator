using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalIndicator.RPC
{
    internal static class RPCManager
    {
        // Server RPCs
        internal const string RPC_SYNCPORTAL = PortalIndicator.PluginName + "_SyncPortal";
        internal const string RPC_RESYNC = PortalIndicator.PluginName + "_ResyncPortals";

        // Client RPCs
        internal const string RPC_SYNCREQUEST = PortalIndicator.PluginName + "_SyncPortalRequest";
        internal const string RPC_ADDORUPDATEREQUEST = PortalIndicator.PluginName + "_AddOrUpdatePortalRequest";
        internal const string RPC_REMOVEREQUEST = PortalIndicator.PluginName + "_RemovePortalRequest";

        public static void Register()
        {
            // Server RPCs
            //ZRoutedRpc.instance.Register(RPC_SYNCPORTAL, new Action<long, ZPackage>(Client.ClientEvents.RPC_SyncPortal));
            ZRoutedRpc.instance.Register(RPC_RESYNC, new Action<long, ZPackage>(Client.ReceiveResync));

            // Client RPCs
            ZRoutedRpc.instance.Register(RPC_SYNCREQUEST, new Action<long, string>(Server.SyncRequest));
            //ZRoutedRpc.instance.Register(RPC_ADDORUPDATEREQUEST, new Action<long, ZPackage>(Server.ServerEvents.RPC_AddOrUpdateRequest));
            //ZRoutedRpc.instance.Register(RPC_REMOVEREQUEST, new Action<long, ZDOID>(Server.ServerEvents.RPC_RemoveRequest));
        }
    }
}
