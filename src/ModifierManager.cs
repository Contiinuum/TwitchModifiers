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
        

        public static Vector3 debugTextPosition = new Vector3(0f, 3f, 8f);

        private static List<Modifier> queuedModifiers = new List<Modifier>();
        public static List<Modifier> activeModifiers = new List<Modifier>();


        public static void AddModifierToQueue(Modifier modifier)
        {
            if (!CanAddModifiers(modifier)) return;

            queuedModifiers.Add(modifier);
            activeModifiers.Add(modifier);
            ProcessQueue();             
        }

        private static void ProcessQueue()
        {
            if (queuedModifiers.Count == 0 || timerActive) return;
            Modifier mod = queuedModifiers[0];

            MelonCoroutines.Start(Countdown(mod));
        }


        private static bool CanAddModifiers(Modifier modifier)
        {
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

                if (nukeActive) return true;

                if (activeModifiers.Count >= Config.generalParams.maxActiveModifiers)
                {
                    MelonLogger.Log("Max active modifiers reached.");
                    return false;
                }

                
                return true;
            }

            MelonLogger.Log("Currently not in a song.");
            return false;
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
            queuedModifiers.Remove(modifier);
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
        }

        public static IEnumerator Reset()
        {              
            stopAllModifiers = true;
            yield return new WaitForSecondsRealtime(1.5f);
            ModStatusHandler.RemoveAllDisplays();
            for (int i = activeModifiers.Count - 1; i > -1; i--) activeModifiers[i].Deactivate();      
            timerActive = false;
            queuedModifiers.Clear();
            stopAllModifiers = false;
            invalidateScore = false;
            activeModifiers.Clear();
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
            queuedModifiers.Clear();
            stopAllModifiers = false;
            activeModifiers.Clear();
            
        }
    }
}
