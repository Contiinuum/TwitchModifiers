using Harmony;
using System;
using TwitchChatter;
using MelonLoader;
using System.Linq;
using UnityEngine;

namespace AudicaModding
{
    internal static class Hooks
    {
        [HarmonyPatch(typeof(TwitchChatStream), "write_chat_msg", new Type[] { typeof(string) })]
        private static class PatchWriteChatMsg
        {
            private static void Prefix(string msg)
            {
                if (msg.Length > 1)
                {
                    if (msg.Substring(0, 1) == "@")
                    {
                        if (msg.Contains("tmi.twitch.tv PRIVMSG "))
                        {
                            TwitchHandler.ParsedTwitchMessage parsedMsg = TwitchHandler.ParseTwitchMessage(msg);
                            TwitchHandler.ParseCommand(parsedMsg.message, parsedMsg.displayName);
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(MenuState), "SetState", new Type[] { typeof(MenuState.State) })]
        private static class PatchSetMenuState
        {
            private static void Postfix(MenuState __instance, ref MenuState.State state)
            {
                if (state == MenuState.State.Launched || state == MenuState.State.SongPage) MelonCoroutines.Start(ModifierManager.Reset());
            }
        }

        [HarmonyPatch(typeof(InGameUI), "Restart")]
        private static class PatchRestart
        {
            private static void Prefix(InGameUI __instance)
            {
                MelonCoroutines.Start(ModifierManager.Reset());
            }

        }

        [HarmonyPatch(typeof(ScoreKeeper), "GetScoreValidity")]
        private static class PatchGetScoreValidity
        {
            private static bool Prefix(ScoreKeeper __instance, ref ScoreKeeper.ScoreValidity __result)
            {
                if (ModifierManager.activeModifiers.Count == 0 || ModifierManager.invalidateScore) return false;
                foreach (Modifier mod in ModifierManager.activeModifiers)
                {
                    if (mod.type == ModifierType.Speed)
                    {
                        if (mod.amount >= 1f)
                        {
                            __result = ScoreKeeper.ScoreValidity.Valid;
                            __instance.mHasInvalidatedScore = false;
                            return true;
                        }
                    }
                    else if(mod.type == ModifierType.Wobble)
                    {
                        __result = ScoreKeeper.ScoreValidity.Valid;
                        __instance.mHasInvalidatedScore = false;
                        return true;
                    }
                }


                return false;
            }
        }
        /*
        [HarmonyPatch(typeof(Target), "InitFromSpawner", new Type[] { typeof(TargetSpawner.SpawnInfo), typeof(SongCues.Cue) })]
        private static class OnCreated
        {
            private static void Postfix(Target __instance, TargetSpawner.SpawnInfo info, SongCues.Cue cue)
            {
                if (!ModifierManager.colorSwapActive && !ModifierManager.randomColorsActive) return;
                if (cue.behavior != Target.TargetBehavior.Chain) return;          
                
            }
        }
        */
                /*[HarmonyPatch(typeof(AudioDriver), "SetSpeed", new Type[] { typeof(float) })]
                private static class PatchSetSpeed
                {
                    private static void PostFix(AudioDriver __instance, float speed)
                    {
                        ScoreKeeper.I.GetScoreValidity();
                    }
                }
                */
                /*
                [HarmonyPatch(typeof(ScoreKeeper), "InvalidateScore")]
                private static class PatchGetScoreValidity
                {
                    private static void Postfix(ScoreKeeper __instance)
                    {

                        foreach (Modifier mod in ModifierManager.activeModifiers)
                        {
                            if (mod.type == ModifierType.Speed)
                            {
                                MelonLogger.Log("invalidated!");
                                __instance.mHasInvalidatedScore = true;
                                break;
                            }
                        }
                        //if (__result == ScoreKeeper.ScoreValidity.Valid || __result == ScoreKeeper.ScoreValidity.NoFail) return false;
                    }
                }
                */

            }
}