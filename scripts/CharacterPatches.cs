using HarmonyLib;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Managers;
using System.Collections.Generic;
using System.Reflection;
using sts1to2card.src.RedIronclad.character;
using sts1to2card.src.RedIroncladAwakened.character;
using sts1to2card.src.GreenSilent.character;
using sts1to2card.src.GreenSilentAwakened.character;
using MegaCrit.Sts2.Core.Entities.Ancients;
using Godot;
using sts1to2card.src.BlueDefect.character;

namespace sts1to2card.scripts
{
    [HarmonyPatch]
    public static class CharacterPatches
    {
        private static readonly HashSet<System.Type> MyCharacterTypes = new()
        {
            typeof(RedIronclad),
            typeof(RedIroncladAwakened),
            typeof(GreenSilent),
            typeof(GreenSilentAwakened),
            typeof(BlueDefect)
        };

        private static bool IsMyCharacter(Player player)
        {
            return player?.Character != null && MyCharacterTypes.Contains(player.Character.GetType());
        }

        // Boss时间线跳过
        [HarmonyPatch(typeof(ProgressSaveManager), "ObtainCharUnlockEpoch")]
        public static class Patch_ObtainCharUnlockEpoch
        {
            private static bool Prefix(Player localPlayer, int act)
            {
                if (IsMyCharacter(localPlayer))
                {
                    return false;
                }
                return true;
            }
        }

        // Architect 修复版
        [HarmonyPatch(typeof(TheArchitect), "WinRun")]
        public static class Patch_TheArchitect_WinRun
        {
            private static void Prefix(TheArchitect __instance)
            {
                var runState = RunManager.Instance.DebugOnlyGetState();
                if (runState == null)
                    return;

                var localPlayer = LocalContext.GetMe(runState);
                if (localPlayer != null && !IsMyCharacter(localPlayer))
                    return;

                TryFixDialogue(__instance);
            }

            private static void TryFixDialogue(TheArchitect instance)
            {
                var field = typeof(TheArchitect).GetField("_dialogue",
                    BindingFlags.Instance | BindingFlags.NonPublic);

                if (field == null)
                    return;

                var current = field.GetValue(instance);

                // 只在 null 时修复（安全）
                if (current != null)
                    return;

                var dialogue = new AncientDialogue(
                    new string[] { "event:/sfx/ancients/architect/architect_vo_generic" }
                )
                {
                    EndAttackers = (ArchitectAttackers)0
                };

                field.SetValue(instance, dialogue);
            }
        }

        // 15精英击败 跳过
        [HarmonyPatch(typeof(ProgressSaveManager), "CheckFifteenElitesDefeatedEpoch")]
        public static class Patch_CheckFifteenElitesDefeatedEpoch
        {
            private static bool Prefix(Player localPlayer)
            {
                if (IsMyCharacter(localPlayer))
                {
                    return false;
                }
                return true;
            }
        }

        // 15 Boss击败 跳过
        [HarmonyPatch(typeof(ProgressSaveManager), "CheckFifteenBossesDefeatedEpoch")]
        public static class Patch_CheckFifteenBossesDefeatedEpoch
        {
            private static bool Prefix(Player localPlayer)
            {
                if (IsMyCharacter(localPlayer))
                {
                    return false;
                }
                return true;
            }
        }
    }
}