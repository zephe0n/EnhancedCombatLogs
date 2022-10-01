using HarmonyLib;
using UnityModManagerNet;
using System.Reflection;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace EnhancedCombatLogs
{
#if DEBUG
    [EnableReloading]
#endif
    public static class Main
    {
        public static bool Enabled;
        public static ModLogger Logger;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;

            Logger.Log("Loading");

            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
#if DEBUG
            modEntry.OnUnload = Unload;
#endif
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }

#if DEBUG
        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            new Harmony(modEntry.Info.Id).UnpatchAll();

            return true;
        }
#endif

        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            //Settings.Draw();
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            //Settings.Save(modEntry);
        }
    }
}