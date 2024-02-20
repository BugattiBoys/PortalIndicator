using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace PortalIndicator.Patches
{
    [HarmonyPatch(typeof(WearNTear), nameof(WearNTear.OnPlaced))]
    static class WearNTear_OnPlaced
    {
        /// <summary>
        /// After placing a piece, check if it's a portal, and if so, call OnPortalPlaced
        /// Known issue: This patch magically stops working if a patch on Player.PlacePiece exists (here or in another mod)
        /// See: SpikeHimself/XPortal#36 and BepInEx/HarmonyX#71
        /// </summary>
        internal static void Prefix(WearNTear __instance)
        {
            var piece = __instance.GetComponent<Piece>();
            var nview = __instance.GetComponent<ZNetView>();
            if (piece.m_name.Contains("$piece_portal") && nview)
            {
                Log.Info("A portal has been placed!");
                PortalIndicator.RequestUpdate();
            }
        }
    }

    [HarmonyPatch(typeof(WearNTear), nameof(WearNTear.Destroy))]
    static class WearNTear_Destroy
    {
        /// <summary>
        /// Before destroying a piece, check if it's a portal, and if so, call OnPortalDestroyed
        /// </summary>
        static void Postfix(WearNTear __instance)
        {
            var piece = __instance.m_piece;
            if (!piece)
            {
                return;
            }

            var nview = piece.m_nview;
            if (!nview)
            {
                return;
            }

            if (piece.m_name.Contains("$piece_portal") && piece.CanBeRemoved())
            {
                Log.Info("A portal has been destroyed");
                PortalIndicator.RequestUpdate();
            }
        }
    }
}
