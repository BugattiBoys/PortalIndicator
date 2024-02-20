using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace PortalIndicator.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.PlacePiece))]
    static class Player_PlacePiece
    {
        internal static void Postfix(Piece piece)
        {
            Log.Info("Piece placed!");
            Log.Info(piece.m_name);
            if (piece.m_name.Contains("$piece_portal"))
            {
                Log.Info("A portal has been placed!");
                PortalIndicator.RequestUpdate();
            }
        }
    }
}
