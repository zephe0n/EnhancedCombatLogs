using HarmonyLib;
using Kingmaker.Blueprints.Root.Strings.GameLog;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UI.Models.Log;
using Kingmaker.UI.Models.Log.CombatLog_ThreadSystem.LogThreads.Combat;
using Kingmaker.UI.Models.Log.CombatLog_ThreadSystem;

namespace BetterLogs
{
    [HarmonyPatch(typeof(AttackLogMessage), nameof(AttackLogMessage.GetData))]
    public class AttackLogPatch
    {
        static bool Prefix(ref AttackLogMessage __instance, RuleAttackRoll rule)
        {
            __instance.Color = GameLogStrings.Instance.DefaultColor;

            if (Main.Settings.AttackRolls && (Main.Settings.EnemyEnable || !rule.Initiator.IsPlayersEnemy))
            {
                if (rule.IsCriticalRoll && rule.IsCriticalConfirmed && Main.Settings.CriticalSuccessEnable)
                {
                    __instance.Color = Main.Settings.CriticalHitColor;
                }
                else if (rule.IsHit && Main.Settings.SuccessEnable)
                {
                    __instance.Color = Main.Settings.SuccessColor;
                }
                else if (rule.AutoMiss && Main.Settings.CriticalFailEnable)
                {
                    __instance.Color = Main.Settings.CriticalMissColor;
                }
                else if (!rule.IsHit && Main.Settings.FailEnable)
                {
                    __instance.Color = Main.Settings.FailureColor;
                }
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(SavingThrowMessage), nameof(SavingThrowMessage.GetData))]
    public class SavingThrowLogPatch
    {
        static bool Prefix(ref SavingThrowMessage __instance, RuleSavingThrow rule)
        {
            __instance.Color = GameLogStrings.Instance.DefaultColor;

            if (Main.Settings.SavingThrows && (Main.Settings.EnemyEnable || !rule.Initiator.IsPlayersEnemy))
            {
                if (Main.Settings.SuccessEnable && rule.IsPassed)
                {
                    __instance.Color = Main.Settings.SuccessColor;
                }
                else if (Main.Settings.FailEnable && !rule.IsPassed)
                {
                    __instance.Color = Main.Settings.FailureColor;
                }
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(RulebookSpellResistanceCheckLogThread), nameof(RulebookSpellResistanceCheckLogThread.HandleEvent), new Type[] { typeof(RuleSpellResistanceCheck) })]
    public class SpellResistancePatch : LogThreadBase
    {
        [HarmonyPrefix]
        static bool Prefix(RuleSpellResistanceCheck rule)
        {
            using (GameLogContext.Scope)
            {
                LogThreadBase.Strings.SpellImmunity.Color = GameLogStrings.Instance.DefaultColor;
                LogThreadBase.Strings.SpellResistanceNotPassed.Color = GameLogStrings.Instance.DefaultColor;
                LogThreadBase.Strings.SpellResistancePassed.Color = GameLogStrings.Instance.DefaultColor;

                if (Main.Settings.SpellResistance && (Main.Settings.EnemyEnable || !rule.Initiator.IsPlayersEnemy))
                {
                    if (rule.IsSpellResisted && Main.Settings.FailEnable)
                    {
                        LogThreadBase.Strings.SpellImmunity.Color = Main.Settings.FailureColor;
                        LogThreadBase.Strings.SpellResistanceNotPassed.Color = Main.Settings.FailureColor;
                    }
                    else if (!rule.IsSpellResisted && Main.Settings.SuccessEnable)
                    {
                        LogThreadBase.Strings.SpellResistancePassed.Color = Main.Settings.SuccessColor;
                    }
                }
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(RollSkillCheckLogThread), nameof(RollSkillCheckLogThread.HandleEvent), new Type[] { typeof(RuleSkillCheck) })]
    public class SkillCheckPatch : LogThreadBase
    {
        [HarmonyPrefix]
        static bool Prefix(RuleSkillCheck check)
        {
            using (GameLogContext.Scope)
            {
                LogThreadBase.Strings.SkillCheckSuccess.Color = GameLogStrings.Instance.DefaultColor;
                LogThreadBase.Strings.SkillCheckFail.Color = GameLogStrings.Instance.DefaultColor;

                if (Main.Settings.SkillCheck)
                {
                    if (!check.Success && Main.Settings.FailEnable)
                    {
                        LogThreadBase.Strings.SkillCheckFail.Color = Main.Settings.FailureColor;
                    }
                    else if (check.Success && Main.Settings.SuccessEnable)
                    {
                        LogThreadBase.Strings.SkillCheckSuccess.Color = Main.Settings.SuccessColor;
                    }
                }
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(RollSkillCheckLogThread), nameof(RollSkillCheckLogThread.HandleEvent), new Type[] { typeof(RulePartyStatCheck) })]
    public class PartySkillCheckPatch : LogThreadBase
    {
        [HarmonyPrefix]
        static bool Prefix(RulePartyStatCheck check)
        {
            using (GameLogContext.Scope)
            {
                LogThreadBase.Strings.PartySkillCheckSuccess.Color = GameLogStrings.Instance.DefaultColor;
                LogThreadBase.Strings.PartySkillCheckFail.Color = GameLogStrings.Instance.DefaultColor;

                if (Main.Settings.SkillCheck)
                {
                    if (!check.Success && Main.Settings.FailEnable)
                    {
                        LogThreadBase.Strings.PartySkillCheckFail.Color = Main.Settings.FailureColor;
                    }
                    else if (check.Success && Main.Settings.SuccessEnable)
                    {
                        LogThreadBase.Strings.PartySkillCheckSuccess.Color = Main.Settings.SuccessColor;
                    }
                }
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(CombatManeuverLogMessage), nameof(CombatManeuverLogMessage.GetData))]
    public class CombatManeuverPatch
    {
        static bool Prefix(ref CombatManeuverLogMessage __instance, RuleCombatManeuver rule)
        {
            __instance.Color = GameLogStrings.Instance.DefaultColor;

            if (Main.Settings.CombatManeuver && (Main.Settings.EnemyEnable || !rule.Initiator.IsPlayersEnemy))
            {
                if (rule.Success && Main.Settings.SuccessEnable)
                {
                    __instance.Color = Main.Settings.SuccessColor;
                }
                else if (!rule.Success && Main.Settings.FailEnable)
                {
                    __instance.Color = Main.Settings.FailureColor;
                }
            }

            return true;
        }
    }
}
