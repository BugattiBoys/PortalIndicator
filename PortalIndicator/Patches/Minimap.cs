using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using static Minimap;

namespace PortalIndicator.Patches
{
    [HarmonyPatch(typeof(Minimap), nameof(Minimap.SetMapMode))]
    static class Minimap_SetMapMode
    {
        static void Prefix()
        {
            Log.Info("Map status changed, triggering resync");
            PortalIndicator.RequestUpdate();
        }
    }
}
