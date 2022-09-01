using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;
using System.Reflection;

namespace BetterLogs
{
#if DEBUG
    [EnableReloading]
#endif
    static class Main
    {
        public static Settings Settings;
        public static bool Enabled;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            Settings = Settings.Load<Settings>(modEntry);
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
            Settings.Draw();
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }
    }
}