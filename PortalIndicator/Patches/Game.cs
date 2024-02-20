using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace PortalIndicator.Patches
{
    [HarmonyPatch(typeof(Game), nameof(Game.Start))]
    static class Game_Start
    {
        static void Postfix()
        {
            Environment.GameStarted = true;
            PortalIndicator.GameStarted();
        }
    }

    [HarmonyPatch(typeof(Game), nameof(Game.ConnectPortals))]
    static class Game_ConnectPortals
    {
        static void Postfix()
        {
            PortalIndicator.RequestUpdate();
        }
    }
}
