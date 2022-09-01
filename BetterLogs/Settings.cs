using Kingmaker.Blueprints.Root;
using UnityEngine;
using UnityModManagerNet;

namespace BetterLogs
{
    public class Settings : UnityModManager.ModSettings
    {
        private static readonly Dictionary<string, bool> m_visible_state = new Dictionary<string, bool>();

        private static RestColors RestColors = new();

        public bool SuccessEnable = true;
        public bool FailEnable = true;
        public bool CriticalSuccessEnable = true;
        public bool CriticalFailEnable = true;
        public bool EnemyEnable = true;
        //public bool PartyEnable = true;
        //public bool InvertEnemy = false;

        public bool AttackRolls = true;        
        public bool SavingThrows = true;
        public bool SpellResistance = true;
        public bool SkillCheck = true;
        public bool CombatManeuver = true;

        public Color SuccessColor = RestColors.TextColorSuccess;
        public Color FailureColor = RestColors.TextColorFail;
        public Color CriticalHitColor = RestColors.TextColorSuccess;
        public Color CriticalMissColor = RestColors.TextColorFail;

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }

        public static void Draw()
        {
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            bool draw_general = CollapsibleButton("General Settings");
            GUILayout.EndHorizontal();

            if (draw_general)
            {
                GUILayout.BeginHorizontal();
                Main.Settings.SuccessEnable = GUILayout.Toggle(Main.Settings.SuccessEnable, " Enable success");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                Main.Settings.FailEnable = GUILayout.Toggle(Main.Settings.FailEnable, " Enable failure");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                Main.Settings.CriticalSuccessEnable = GUILayout.Toggle(Main.Settings.CriticalSuccessEnable, " Enable critical success");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                Main.Settings.CriticalFailEnable = GUILayout.Toggle(Main.Settings.CriticalFailEnable, " Enable critical failure");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                Main.Settings.EnemyEnable = GUILayout.Toggle(Main.Settings.EnemyEnable, " Enable enemy");
                GUILayout.EndHorizontal();

                //GUILayout.BeginHorizontal();
                //Main.Settings.PartyEnable = GUILayout.Toggle(Main.Settings.PartyEnable, " Enable party");
                //GUILayout.EndHorizontal();

                //GUILayout.BeginHorizontal();
                //Main.Settings.InvertEnemy = GUILayout.Toggle(Main.Settings.InvertEnemy, " Invert enemy colors");
                //GUILayout.EndHorizontal();
            }

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            bool draw_rolls = CollapsibleButton("Rolls Settings");
            GUILayout.EndHorizontal();

            if (draw_rolls)
            {
                GUILayout.BeginHorizontal();
                Main.Settings.AttackRolls = GUILayout.Toggle(Main.Settings.AttackRolls, " Enable attack rolls");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                Main.Settings.SavingThrows = GUILayout.Toggle(Main.Settings.SavingThrows, " Enable saving throws");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                Main.Settings.SpellResistance = GUILayout.Toggle(Main.Settings.SpellResistance, " Enable spell resistance");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                Main.Settings.SkillCheck = GUILayout.Toggle(Main.Settings.SkillCheck, " Enable skill checks");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                Main.Settings.CombatManeuver = GUILayout.Toggle(Main.Settings.CombatManeuver, " Enable combat maneuver");
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            bool draw_colors = CollapsibleButton("Colors");
            GUILayout.EndHorizontal();

            if (draw_colors)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Success color");
                UnityModManager.UI.DrawColor(ref Main.Settings.SuccessColor);
                if (GUILayout.Button("Default"))
                {
                    Main.Settings.SuccessColor = RestColors.TextColorSuccess;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Failure color");
                UnityModManager.UI.DrawColor(ref Main.Settings.FailureColor);
                if (GUILayout.Button("Default"))
                {
                    Main.Settings.FailureColor = RestColors.TextColorFail;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Critical hit color");
                UnityModManager.UI.DrawColor(ref Main.Settings.CriticalHitColor);
                if (GUILayout.Button("Default"))
                {
                    Main.Settings.CriticalHitColor = RestColors.TextColorSuccess;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Critical miss color");
                UnityModManager.UI.DrawColor(ref Main.Settings.CriticalMissColor);
                if (GUILayout.Button("Default"))
                {
                    Main.Settings.CriticalMissColor = RestColors.TextColorFail;
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(10);
        }

        private static bool CollapsibleButton(string text, bool initial_state = true)
        {
            GUIStyle base_style = new GUIStyle(GUI.skin.GetStyle("Label"));
            base_style.fontStyle = FontStyle.Bold;
            base_style.normal.textColor = Color.white;

            if (!m_visible_state.ContainsKey(text))
            {
                m_visible_state[text] = initial_state;
            }

            bool prev_state = m_visible_state[text];
            string sign = prev_state ? "-" : "+";

            bool new_state = GUILayout.Button($"{sign} {text}", base_style) ? !prev_state : prev_state;
            m_visible_state[text] = new_state;

            return new_state;
        }
    }
}
