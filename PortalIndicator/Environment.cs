using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalIndicator
{
    internal static class Environment
    {
        internal static bool IsServer
        {
            get
            {
                return ZNet.instance != null && ZNet.instance.IsServer();
            }
        }
        internal static long ServerPeerId
        {
            get
            {
                return ZRoutedRpc.instance.GetServerPeerID();
            }
        }
    }
}
