using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;

namespace AudicaModding
{
    public class ModifierManager
    {
        private static bool timerActive = false;
        public static bool stopAllModifiers = false;
        public static bool enableCountdown = true;

        public static bool invalidateScore = false;

        public static bool randomColorsActive = false;
        public static bool colorSwapActive = false;

        public static bool nukeActive = false;
        public static bool queueCheckInProgress = false;

        public static Vector3 debugTextPosition = new Vector3(0f, 3f, 8f);

        public static List<Modifier> queuedModifiers = new List<Modifier>();
        private static List<Modifier> queuedNukeModifiers = new List<Modifier>();
        public static List<Modifier> activeModifiers = new List<Modifier>();

        public static Dictionary<float, Vector2> originalOffsets = new Dictionary<float, Vector2>();
        public static float originalExposure = .5f;
        public static float originalRotation;
        public static float userExposure = .5f;
        public static float userRotation;
        public static bool originalArenaValuesSet = false;

        public static void AddModifierToQueue(Modifier modifier, bool fromNuke)
        {
            MelonLogger.Log(modifier.defaultParams.name + " added to queue");
            if(nukeActive && fromNuke)
            {
                queuedNukeModifiers.Add(modifier);
            }
            else
            {
                queuedModifiers.Add(modifier);
            }
            
            if((nukeActive && fromNuke) || !nukeActive) ProcessQueue();
        }

        public static IEnumerator ISetUserArenaValues()
        {
            if (!Config.generalParams.enableTwitchModifiers) yield break;
            if (KataConfig.I.practiceMode) yield break;
            if (originalArenaValuesSet) yield break;
            while (EnvironmentLoader.I.IsSwitching())
            {
                yield return new WaitForSecondsRealtime(.2f);
            }
            originalExposure = RenderSettings.skybox.GetFloat("_Exposure");
            originalRotation = RenderSettings.skybox.GetFloat("_Rotation");
            originalArenaValuesSet = true;
        }

        public static IEnumerator ISetDefaultArenaValues()
        {
            if (!Config.generalParams.enableTwitchModifiers) yield break;
            if (KataConfig.I.practiceMode) yield break;
            if (originalArenaValuesSet) yield break;
            while (EnvironmentLoader.I.IsSwitching())
            {
                yield return new WaitForSecondsRealtime(.2f);
            }
            userExposure = RenderSettings.skybox.GetFloat("_Exposure");
            userRotation = RenderSettings.skybox.GetFloat("_Rotation");
            originalArenaValuesSet = true;
        }

        public static void SetOriginalOffset(SongCues.Cue[] cues)
        {
            originalOffsets.Clear();
            for(int i = 0; i < cues.Length; i++)
            {
                if(!originalOffsets.ContainsKey(cues[i].tick + cues[i].pitch)) originalOffsets.Add(cues[i].tick + cues[i].pitch, cues[i].gridOffset);
            }
        }

        public static IEnumerator ProcessQueueDelayed()
        {
            
            yield return new WaitForSecondsRealtime(5f);
            ProcessQueue();
            
        }

        public static void ProcessQueue()
        {
            
            MelonCoroutines.Start(IProcessQueue(nukeActive ? queuedNukeModifiers : queuedModifiers));
        }

        private static IEnumerator IProcessQueue(List<Modifier> queue)
        {
            if (queue.Count == 0) yield break;
            if (queueCheckInProgress) yield break;
            queueCheckInProgress = true;
            Modifier add = null;
            foreach (Modifier mod in queue)
            {
                if (nukeActive && mod.type == ModifierType.Nuke) continue;
                if (!CanAddModifier(mod))
                {
                    continue;
                }
                activeModifiers.Add(mod);
                add = mod;
                break;
            }
            if (add != null)
            {
                queue.Remove(add);
                MelonCoroutines.Start(Countdown(add));
            }
            yield return new WaitForSecondsRealtime(.2f);
            queueCheckInProgress = false;
        }


        private static bool CanAddModifier(Modifier mod)
        {
            if (KataConfig.I.practiceMode) return false;

            if (MenuState.sState == MenuState.State.Launched)
            {
                if (AudioDriver.I is null)
                {
                    MelonLogger.Log("AudioDriver is null.");
                    return false;
                }
                  
                if (AudioDriver.I.IsPlaying())
                {
                    if(SongCues.I.GetLastCueStartTick() < AudioDriver.I.mCachedTick + 7680)
                    {
                        MelonLogger.Log("Song is about to end. Mod can't be activated.");
                        return false;                                           
                    }
                }
                else
                {
                    MelonLogger.Log("Song hasn't started yet.");
                    return false;
                }

                if (nukeActive && mod.type != ModifierType.Nuke) return true;

                if (activeModifiers.Count >= Config.generalParams.maxActiveModifiers)
                {
                    MelonLogger.Log("Max active modifiers reached.");
                    return false;
                }

                if (timerActive) return false;

                if (activeModifiers.Count > 0)
                {
                    foreach (Modifier activeMod in activeModifiers)
                    {
                        if (activeMod.type == mod.type)
                        {
                            return false;
                        }
                        if (mod.type == ModifierType.TimingAttack && !Integrations.timingAttackFound)
                        {
                            return false;
                        }
                        if (activeMod.defaultParams.active)
                        {
                            switch (mod.type)
                            {
                                case ModifierType.HiddenTelegraphs:
                                case ModifierType.TimingAttack:
                                    if (!CanActivate(ModifierType.HiddenTelegraphs, ModifierType.TimingAttack)) return false;
                                    break;
                                case ModifierType.Speed:
                                    if (!CanActivate(ModifierType.Speed, ModifierType.Wobble)) return false;
                                    if (!CanActivateSpeed(mod)) return false;
                                    break;
                                case ModifierType.Scale:
                                case ModifierType.ZOffset:
                                    if (!CanActivate(ModifierType.Scale, ModifierType.ZOffset)) return false;
                                    break;
                                case ModifierType.Wobble:
                                    if (!CanActivate(ModifierType.Wobble, ModifierType.Speed)) return false;
                                    break;
                                case ModifierType.UnifyColors:
                                    if (!CanActivateUnifyColors()) return false;
                                    break;
                                case ModifierType.RandomColors:
                                    if (!CanActivate(ModifierType.RandomColors, ModifierType.UnifyColors)) return false;
                                    break;
                                case ModifierType.ColorSwap:
                                    if (!CanActivate(ModifierType.ColorSwap, ModifierType.UnifyColors)) return false;
                                    break;
                                case ModifierType.StreamMode:
                                    if (!CanActivateStreamMode()) return false;
                                    break;
                                default:
                                    break;
                            }
                            /*
                            if ((mod.type == ModifierType.Speed && activeMod.type == ModifierType.Wobble) || (mod.type == ModifierType.Wobble && activeMod.type == ModifierType.Speed))
                            {
                                return false;
                            }
                            else if (mod.type == ModifierType.UnifyColors && (activeMod.type == ModifierType.RandomColors || activeMod.type == ModifierType.ColorSwap) || (mod.type == ModifierType.RandomColors || mod.type == ModifierType.ColorSwap) && activeMod.type == ModifierType.UnifyColors)
                            {
                                return false;
                            }
                            else if ((mod.type == ModifierType.ZOffset && activeMod.type == ModifierType.Scale) || (mod.type == ModifierType.Scale && activeMod.type == ModifierType.ZOffset))
                            {
                                return false;
                            }
                            else if((mod.type == ModifierType.Speed && mod.amount > 1f && activeMod.type == ModifierType.StreamMode) || (mod.type == ModifierType.StreamMode && activeMod.type == ModifierType.Speed && activeMod.amount > 1f))
                            {
                                return false;
                            }
                            else if((mod.type == ModifierType.TimingAttack && activeMod.type == ModifierType.HiddenTelegraphs) || (mod.type == ModifierType.HiddenTelegraphs && activeMod.type == ModifierType.TimingAttack))
                            {
                                CheckAgainstActiveModifier(ModifierType.TimingAttack, ModifierType.HiddenTelegraphs);
                                return false;
                            }
                            */
                        }

                    }
                }
                return true;
            }
            MelonLogger.Log("Currently not in a song.");           
            return false;
        }
        private static bool CanActivateUnifyColors()
        {
            foreach (Modifier activeMod in activeModifiers)
            {
                if (activeMod.type == ModifierType.RandomColors || activeMod.type == ModifierType.ColorSwap || activeMod.type == ModifierType.UnifyColors) return false;
            }
            return true;
        }

        private static bool CanActivateSpeed(Modifier speed)
        {
            foreach (Modifier activeMod in activeModifiers)
            {
                if ((activeMod.type == ModifierType.StreamMode || activeMod.type == speed.type) && speed.amount > 1f) return false;
            }
            return true;
        }
        private static bool CanActivateStreamMode()
        {
            foreach (Modifier activeMod in activeModifiers)
            {
                if (activeMod.type == ModifierType.Speed && activeMod.amount > 1f) return false;
            }
            return true;
        }

        private static bool CanActivate(ModifierType modToActivate, ModifierType modToCheckAgainst)
        {

            foreach(Modifier activeMod in activeModifiers)
            {
                if (activeMod.type == modToActivate || activeMod.type == modToCheckAgainst) return false;
            }
            return true;
        }

        private static IEnumerator Countdown(Modifier modifier, float countdownTimer = 4)
        {
            yield return new WaitForSecondsRealtime(.2f);
            if (Config.generalParams.countdownEnabled && !nukeActive)
            {
                timerActive = true;
                
                string colortag = modifier.type == ModifierType.AA ? "<color=\"orange\">" : modifier.type == ModifierType.Psychedelia ? "<color=\"blue\">" : modifier.type == ModifierType.Mines ? "<color=\"red\">" : modifier.type == ModifierType.Speed ? "<color=\"green\">" : "";
                while (countdownTimer > 0)
                {
                    if (!InGameUI.I.pauseScreen.IsPaused())
                    {
                        if (countdownTimer == 4) DebugText(colortag + modifier.defaultParams.name + "\nrequested by: " + modifier.defaultParams.user, 0.2f);
                        if (countdownTimer == 3) DebugText("3", 0.6f);
                        if (countdownTimer == 2) DebugText("2", 0.6f);
                        if (countdownTimer == 1) DebugText("1", 0.6f);
                        if (stopAllModifiers) yield break;


                        yield return new WaitForSecondsRealtime(1f);
                        countdownTimer--;
                    }
                    yield return null;
                }
            }
            
            timerActive = false;
            if (!stopAllModifiers) modifier.Activate();
            yield return new WaitForSeconds(nukeActive ? .1f : Config.generalParams.cooldownBetweenModifiers);
            ProcessQueue();
            yield return null;
        }

        public static void DebugText(string text, float speed)
        {
            KataConfig.I.CreateDebugText(text, debugTextPosition, 5f, null, false, speed);
        }

        public static void RemoveActiveModifier(Modifier mod)
        {
            activeModifiers.Remove(mod);
            ProcessQueue();
        }

        public static IEnumerator Reset()
        {              
            stopAllModifiers = true;
            yield return new WaitForSecondsRealtime(1.5f);
            ModStatusHandler.RemoveAllDisplays();
            for (int i = activeModifiers.Count - 1; i > -1; i--) activeModifiers[i].Deactivate();      
            timerActive = false;
            stopAllModifiers = false;
            invalidateScore = false;
            originalArenaValuesSet = false;
            activeModifiers.Clear();
            RenderSettings.skybox.SetFloat("_Rotation", userRotation);
            Hooks.updateChainColor = false;
        }      

        public static IEnumerator NukeReset(bool _nukeActive)
        {
            nukeActive = _nukeActive;
            stopAllModifiers = true;
            ModStatusHandler.RemoveAllDisplays();
            yield return new WaitForSecondsRealtime(1.5f);           
            for (int i = activeModifiers.Count - 1; i > -1; i--)
            {
                if (activeModifiers[i].type == ModifierType.Nuke) continue;
                activeModifiers[i].Deactivate();
            }           
            timerActive = false;
            stopAllModifiers = false;
            activeModifiers.Clear();
            
        }
    }
}
