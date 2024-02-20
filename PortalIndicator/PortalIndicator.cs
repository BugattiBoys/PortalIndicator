using System.Collections.Generic;
using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;

namespace PortalIndicator
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class PortalIndicator : BaseUnityPlugin
    {
        public const string PluginGUID = "BugattiBoys.Valheim.PortalIndicator";
        public const string PluginName = "PortalIndicator";
        public const string PluginVersion = "0.0.3";
        
        private void Awake()
        {
            Log.IsDebug = true;

            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            Jotunn.Logger.LogInfo("PortalIndicator has landed");

            MinimapManager.OnVanillaMapAvailable += MinimapManager_OnVanillaMapDataLoaded;

            Log.Error("Show a fucking message");
            Log.Info("Applying patches");
            Patches.Patcher.Patch();
        }

        private static void MinimapManager_OnVanillaMapDataLoaded()
        {
            Log.Info("MiniMapManager.OnVanillaMapDataLoaded");
            RequestUpdate();
        }

        public static void RequestUpdate()
        {
            Log.Info("Requesting Portal Update");
            // Ask the server to send us the portals
            var myId = ZDOMan.GetSessionID();
            var myName = Game.instance.GetPlayerProfile().GetName();
            RPC.SendToServer.SyncRequest($"{myName} ({myId}) has joined the game");
        }

        public static void GameStarted()
        {
            Log.Info("Registering RPC handlers");
            RPC.RPCManager.Register();
        }

        public static void UpdateFromPackage(ZPackage pkg)
        {
            var count = pkg.ReadInt();


            Log.Info($"Received {count} portals from server");

            // Wipe all portal pins 
            var existingPins = Minimap.instance.m_pins;
            foreach (var pin in existingPins)
            {
                Log.Info(pin.m_type);
                if (pin.m_type == Minimap.PinType.Icon4)
                {
                    Log.Info("Removing da pins");
                    Minimap.instance.DestroyPinMarker(pin);
                }
            }

            // If we have portals synced up, let's rewrite them all now
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var portalPkg = pkg.ReadPackage();
                    var tag = portalPkg.ReadString();
                    var location = portalPkg.ReadVector3();
                    Log.Info($"Client writing {tag} to {location}");

                    Minimap.instance.AddPin(location, Minimap.PinType.Icon4, tag, false, false);
                }
            }
        }

        public static void ProcessSyncRequest()
        {
            Log.Info("Responding to sync request");
            RPC.SendToClient.Resync(Package(), "I said so");
        }

        public static ZPackage Package()
        {
            var portals = ZDOMan.instance.GetPortals();

            var pkg = new ZPackage();
            pkg.Write(portals.Count);

            Log.Info($"Packaging {portals.Count} portals for client(s)");

            foreach (var portal in portals)
            {
                pkg.Write(PackagePortal(portal));
            }

            return pkg;
        }

        public static ZPackage PackagePortal(ZDO portal)
        {
            var pkg = new ZPackage();
            pkg.Write(portal.GetString("tag"));
            pkg.Write(portal.GetPosition());
            return pkg;
        }
    }
}