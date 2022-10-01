using HarmonyLib;
using Kingmaker.Blueprints.Root.Strings.GameLog;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UI.Models.Log;
using Kingmaker.UI.Models.Log.CombatLog_ThreadSystem.LogThreads.Combat;
using Kingmaker.UI.Models.Log.CombatLog_ThreadSystem;

namespace EnhancedCombatLogs
{
    [HarmonyPatch(typeof(AttackLogMessage), nameof(AttackLogMessage.GetData))]
    public class AttackLogPatch
    {
        static bool Prefix(ref AttackLogMessage __instance, RuleAttackRoll rule)
        {
            __instance.Color = GameLogStrings.Instance.DefaultColor;

            if (Settings.IsEnabled(Settings.AttackRoll) && (Settings.IsEnabled(Settings.EnableEnemy) || !rule.Initiator.IsPlayersEnemy))
            {
                //if (rule.IsCriticalRoll && rule.IsCriticalConfirmed && Main.Settings.CriticalSuccessEnable)
                //{
                //    __instance.Color = Main.Settings.CriticalHitColor;
                //}
                if (rule.IsHit && Settings.IsEnabled(Settings.EnableSuccess))
                {
                    __instance.Color = Settings.SuccessColor;
                }
                //else if (rule.AutoMiss && Main.Settings.CriticalFailEnable)
                //{
                //    __instance.Color = Main.Settings.CriticalMissColor;
                //}
                else if (!rule.IsHit && Settings.IsEnabled(Settings.EnableFailure))
                {
                    __instance.Color = Settings.FailureColor;
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

            if (Settings.IsEnabled(Settings.SavingThrow) && (Settings.IsEnabled(Settings.EnableEnemy) || !rule.Initiator.IsPlayersEnemy))
            {
                if (rule.IsPassed && Settings.IsEnabled(Settings.EnableSuccess))
                {
                    __instance.Color = Settings.SuccessColor;
                }
                else if (!rule.IsPassed && Settings.IsEnabled(Settings.EnableFailure))
                {
                    __instance.Color = Settings.FailureColor;
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

                if (Settings.IsEnabled(Settings.SpellResistance) && (Settings.IsEnabled(Settings.EnableEnemy) || !rule.Initiator.IsPlayersEnemy))
                {
                    if (rule.IsSpellResisted && Settings.IsEnabled(Settings.EnableFailure))
                    {
                        LogThreadBase.Strings.SpellImmunity.Color = Settings.FailureColor;
                        LogThreadBase.Strings.SpellResistanceNotPassed.Color = Settings.FailureColor;
                    }
                    else if (!rule.IsSpellResisted && Settings.IsEnabled(Settings.EnableSuccess))
                    {
                        LogThreadBase.Strings.SpellResistancePassed.Color = Settings.SuccessColor;
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

                if (Settings.IsEnabled(Settings.SkillCheck))
                {
                    if (!check.Success && Settings.IsEnabled(Settings.EnableFailure))
                    {
                        LogThreadBase.Strings.SkillCheckFail.Color = Settings.FailureColor;
                    }
                    else if (check.Success && Settings.IsEnabled(Settings.EnableSuccess))
                    {
                        LogThreadBase.Strings.SkillCheckSuccess.Color = Settings.SuccessColor;
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

                if (Settings.IsEnabled(Settings.SkillCheck))
                {
                    if (!check.Success && Settings.IsEnabled(Settings.EnableFailure))
                    {
                        LogThreadBase.Strings.PartySkillCheckFail.Color = Settings.FailureColor;
                    }
                    else if (check.Success && Settings.IsEnabled(Settings.EnableSuccess))
                    {
                        LogThreadBase.Strings.PartySkillCheckSuccess.Color = Settings.SuccessColor;
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

            if (Settings.IsEnabled(Settings.CombatManeuver) && (Settings.IsEnabled(Settings.EnableEnemy) || !rule.Initiator.IsPlayersEnemy))
            {
                if (rule.Success && Settings.IsEnabled(Settings.EnableSuccess))
                {
                    __instance.Color = Settings.SuccessColor;
                }
                else if (!rule.Success && Settings.IsEnabled(Settings.EnableFailure))
                {
                    __instance.Color = Settings.FailureColor;
                }
            }

            return true;
        }
    }
}
