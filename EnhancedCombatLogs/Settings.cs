using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;
using Kingmaker.Controllers.Dialog;
using Kingmaker.Localization;
using Kingmaker.RuleSystem.Rules;
using ModMenu.Settings;
using System.Globalization;
using UnityEngine;
using UnityModManagerNet;
using Color = UnityEngine.Color;
using FontStyle = UnityEngine.FontStyle;

namespace EnhancedCombatLogs
{
    public class Settings
    {
        private static bool Initialized = false;
        private static readonly Dictionary<string, bool> m_visible_state = new Dictionary<string, bool>();

        private static readonly string RootKey = "enhancedcombatlogs";
        private static readonly string GeneralKey = "general-menu";
        private static readonly string RollsKey = "rolls-menu";
        private static readonly string ColorsKey = "colors-menu";
        public static readonly string EnableSuccess = "enablesuccess";
        public static readonly string EnableFailure= "enablefailure";
        public static readonly string EnableCriticalSuccess = "enablecriticalsuccess";
        public static readonly string EnableCriticalFailure = "enablecriticalfailure";
        public static readonly string EnableParty = "enableparty";
        public static readonly string EnableEnemy= "enableenemy";
        public static readonly string InvertEnemy = "invertenemy";
        public static readonly string AttackRoll = "attackroll";
        public static readonly string SavingThrow = "savingthrow";
        public static readonly string SpellResistance = "spellresistance";
        public static readonly string SkillCheck = "skillcheck";
        public static readonly string CombatManeuver = "combatmaneuver";

        private static SettingsBuilder settings = SettingsBuilder.New(RootKey, GetString(GetKey("title"), "Enhanced Combat Logs"));

        private static RestColors RestColors = new();

        //public bool SuccessEnable = true;
        //public bool FailEnable = true;
        //public bool CriticalSuccessEnable = true;
        //public bool CriticalFailEnable = true;
        //public bool EnemyEnable = true;
        ////public bool PartyEnable = true;
        ////public bool InvertEnemy = false;

        //public bool AttackRolls = true;
        //public bool SavingThrows = true;
        //public bool SpellResistance = true;
        //public bool SkillCheck = true;
        //public bool CombatManeuver = true;

        public static Color32 SuccessColor = RestColors.TextColorSuccess;
        public static Color32 FailureColor = RestColors.TextColorFail;
        public static Color32 CriticalHitColor = RestColors.TextColorSuccess;
        public static Color32 CriticalMissColor = RestColors.TextColorFail;

        public static void Init()
        {
            if (Initialized)
            {
                Main.Logger.Log("Settings already initialized");
                return;
            }

            Main.Logger.Log("Initializing Settings");


            DrawGeneralSettings();
            DrawRollsSettings();
            DrawColorsSettings();






            //    //GUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
            //    //GUILayout.Label("Success color");
            //    //DrawColor32(ref Main.Settings.SuccessColor);
            //    //if (GUILayout.Button("Default"))
            //    //{
            //    //    Main.Settings.SuccessColor = RestColors.TextColorSuccess;
            //    //}
            //    //GUILayout.EndHorizontal();

            //    //GUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
            //    //GUILayout.Label("Failure color");
            //    //DrawColor32(ref Main.Settings.FailureColor);
            //    //if (GUILayout.Button("Default"))
            //    //{
            //    //    Main.Settings.FailureColor = RestColors.TextColorFail;
            //    //}
            //    //GUILayout.EndHorizontal();

            //    //GUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
            //    //GUILayout.Label("Critical hit color");
            //    //DrawColor32(ref Main.Settings.CriticalHitColor);
            //    //if (GUILayout.Button("Default"))
            //    //{
            //    //    Main.Settings.CriticalHitColor = RestColors.TextColorSuccess;
            //    //}
            //    //GUILayout.EndHorizontal();

            //    //GUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
            //    //GUILayout.Label("Critical miss color");
            //    //DrawColor32(ref Main.Settings.CriticalMissColor);
            //    //if (GUILayout.Button("Default"))
            //    //{
            //    //    Main.Settings.CriticalMissColor = RestColors.TextColorFail;
            //    //}
            //    //GUILayout.EndHorizontal();
            //}

            //GUILayout.Space(10);

            ModMenu.ModMenu.AddSettings(settings);

            Initialized = true;

            Main.Logger.Log("Settings Initialized");
        }

        private static void DrawGeneralSettings()
        {
            settings.AddToggle(
                Toggle.New(
                    GetKey(EnableSuccess),
                    false,
                    GetString($"{EnableSuccess}-desc", "Enable success"))
                .WithLongDescription(GetString($"{EnableSuccess}-desc-long", "Enable custom color on roll success.")));

            settings.AddToggle(
                Toggle.New(
                    GetKey(EnableFailure),
                    true,
                    GetString($"{EnableFailure}-desc", "Enable failure"))
                .WithLongDescription(GetString($"{EnableFailure}-desc-long", "Enable custom color on roll failure.")));

            //settings.AddToggle(
            //    Toggle.New(
            //        GetKey(EnableCriticalSuccess),
            //        true,
            //        GetString($"{EnableCriticalSuccess}-desc", "Enable critical success"))
            //    .WithLongDescription(GetString($"{EnableCriticalSuccess}-desc-long", "Enable custom color on roll critical success. Otherwise it will be treated as a regular success.")));

            //settings.AddToggle(
            //    Toggle.New(
            //        GetKey(EnableCriticalFailure),
            //        true,
            //        GetString($"{EnableCriticalFailure}-desc", "Enable critical failure"))
            //    .WithLongDescription(GetString($"{EnableCriticalFailure}-desc-long", "Enable custom color on roll critical failure. Otherwise it will be treated as a regular failure.")));

            //settings.AddToggle(
            //    Toggle.New(
            //        GetKey(EnableParty),
            //        true,
            //        GetString($"{EnableParty}-desc", "Enable party"))
            //    .WithLongDescription(GetString($"{EnableParty}-desc-long", "Enable custom color for party members rolls.")));

            settings.AddToggle(
                Toggle.New(
                    GetKey(EnableEnemy),
                    true,
                    GetString($"{EnableEnemy}-desc", "Enable enemy"))
                .WithLongDescription(GetString($"{EnableEnemy}-desc-long", "Enable custom color on enemy rolls.")));

            //settings.AddToggle(
            //    Toggle.New(
            //        GetKey(InvertEnemy),
            //        true,
            //        GetString($"{InvertEnemy}-desc", "Invert enemy colors"))
            //    .WithLongDescription(GetString($"{InvertEnemy}-desc-long", "Invert success/failure color for enemy rolls.")));
        }

        private static void DrawRollsSettings()
        {
            var rolls= settings.AddSubHeader(GetString(RollsKey, "Rolls Settings"), true);

            rolls.AddToggle(
                Toggle.New(
                    GetKey(AttackRoll),
                    true,
                    GetString($"{AttackRoll}-desc", "Attack rolls"))
                .WithLongDescription(GetString($"{AttackRoll}-desc-long", "Enable custom color on attack rolls.")));

            rolls.AddToggle(
                Toggle.New(
                    GetKey(SavingThrow),
                    true,
                    GetString($"{SavingThrow}-desc", "Saving throws"))
                .WithLongDescription(GetString($"{SavingThrow}-desc-long", "Enable custom color on saving throws.")));

            rolls.AddToggle(
                Toggle.New(
                    GetKey(SpellResistance),
                    true,
                    GetString($"{SpellResistance}-desc", "Spell resitance"))
                .WithLongDescription(GetString($"{SpellResistance}-desc-long", "Enable custom color on spell resistance checks.")));

            rolls.AddToggle(
                Toggle.New(
                    GetKey(SkillCheck),
            true,
                    GetString($"{SkillCheck}-desc", "Skill checks"))
                .WithLongDescription(GetString($"{SkillCheck}-desc-long", "Enable custom color on skill checks rolls.")));

            rolls.AddToggle(
                Toggle.New(
                    GetKey(CombatManeuver),
                    true,
                    GetString($"{CombatManeuver}-desc", "Combat maneuver"))
                .WithLongDescription(GetString($"{CombatManeuver}-desc-long", "Enable custom color on combat maneuvers.")));
        }

        private static void DrawColorsSettings()
        {
            //var colors = settings.AddSubHeader(GetString(ColorsKey, "Colors Configuration"), false);

            //GUILayout.Space(10);

            //GUILayout.BeginHorizontal();
            //bool draw_colors = CollapsibleButton("Colors");
            //GUILayout.EndHorizontal();

            //if (draw_colors)
            //{
            //    GUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
            //    GUILayout.BeginVertical();
            //    GUILayout.Label("Success color");
            //    GUILayout.Label("Failure color");
            //    GUILayout.Label("Critical hit color");
            //    GUILayout.Label("Critical miss color");
            //    GUILayout.EndVertical();

            //    GUILayout.BeginVertical();
            //    GUILayout.BeginHorizontal();
            //    DrawColor32(ref Main.Settings.SuccessColor);
            //    if (GUILayout.Button("Default"))
            //    {
            //        Main.Settings.SuccessColor = RestColors.TextColorSuccess;
            //    }
            //    GUILayout.EndHorizontal();

            //    GUILayout.BeginHorizontal();
            //    DrawColor32(ref Main.Settings.FailureColor);
            //    if (GUILayout.Button("Default"))
            //    {
            //        Main.Settings.FailureColor = RestColors.TextColorFail;
            //    }
            //    GUILayout.EndHorizontal();

            //    GUILayout.BeginHorizontal();
            //    DrawColor32(ref Main.Settings.CriticalHitColor);
            //    if (GUILayout.Button("Default"))
            //    {
            //        Main.Settings.CriticalHitColor = RestColors.TextColorSuccess;
            //    }
            //    GUILayout.EndHorizontal();

            //    GUILayout.BeginHorizontal();
            //    DrawColor32(ref Main.Settings.CriticalMissColor);
            //    if (GUILayout.Button("Default"))
            //    {
            //        Main.Settings.CriticalMissColor = RestColors.TextColorFail;
            //    }
            //    GUILayout.EndHorizontal();
            //    GUILayout.EndVertical();
            //    GUILayout.EndHorizontal();
        }

        //private static bool DrawColor32(ref Color32 color)
        //{
        //    bool changed = false;

        //    GUILayout.BeginHorizontal();
        //    changed |= DrawByteField(ref color.r, " R", GUILayout.Width(50));
        //    changed |= DrawByteField(ref color.g, " G", GUILayout.Width(50));
        //    changed |= DrawByteField(ref color.b, " B", GUILayout.Width(50));
        //    changed |= DrawByteField(ref color.a, " A", GUILayout.Width(50));
        //    GUILayout.EndHorizontal();

        //    return changed;
        //}

        //private static bool DrawByteField(ref byte b, string label, params GUILayoutOption[] options)
        //{
        //    byte num = b;

        //    GUILayout.BeginHorizontal(options);
        //    GUILayout.Label(label);
        //    string text = GUILayout.TextField(b.ToString(), GUILayout.Width(30));
        //    GUILayout.EndHorizontal();

        //    if (string.IsNullOrEmpty(text))
        //    {
        //        b = 0;
        //    }
        //    else if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out int result))
        //    {
        //        if (result > 0 && result <= 255)
        //        {
        //            b = (byte)result;
        //        }
        //    }
        //    else
        //    {
        //        b = 0;
        //    }

        //    return num != b;
        //}

        //private static bool CollapsibleButton(string text, bool initial_state = true)
        //{
        //    GUIStyle base_style = new GUIStyle(GUI.skin.GetStyle("Label"));
        //    base_style.fontStyle = FontStyle.Bold;
        //    base_style.normal.textColor = Color.white;

        //    if (!m_visible_state.ContainsKey(text))
        //    {
        //        m_visible_state[text] = initial_state;
        //    }

        //    bool prev_state = m_visible_state[text];
        //    string sign = prev_state ? "-" : "+";

        //    bool new_state = GUILayout.Button($"{sign} {text}", base_style) ? !prev_state : prev_state;
        //    m_visible_state[text] = new_state;

        //    return new_state;
        //}

        internal static bool IsEnabled(string key)
        {
            return ModMenu.ModMenu.GetSettingValue<bool>(GetKey(key));
        }

        private static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}";
        }

        private static LocalizedString GetString(string partialKey, string text)
        {
            return Helpers.CreateString(GetKey(partialKey), text);
        }
    }

    [HarmonyPatch(typeof(BlueprintsCache))]
    static class BlueprintsCache_Postfix
    {
        [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
        static void Postfix()
        {
            Settings.Init();
        }
    }
}
