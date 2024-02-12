using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using PortalIndicator.Patches;

namespace PortalIndicator.Patches
{
    internal static class Patcher
    {
        private static readonly Harmony patcher = new Harmony(PortalIndicator.PluginGUID + ".harmony");
        public static void Patch()
        {
            patcher.PatchAll(typeof(Game_Start));
        }

        public static void Unpatch() => patcher?.UnpatchSelf();
    }
}
