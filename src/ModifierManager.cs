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
        public bool enableCountdown = true;

        public static Dictionary<ModifierType, float> lastModifierTime = new Dictionary<ModifierType, float>();

        public static Vector3 debugTextPosition = new Vector3(0f, 3f, 8f);

        private static List<Modifier> queuedModifiers = new List<Modifier>();
        public static List<Modifier> activeModifiers = new List<Modifier>();


        public static void AddModifierToQueue(Modifier modifier)
        {
            MelonLogger.Log("Check if modifier can be added..");
            if (!CanAddModifiers(modifier)) return;
            MelonLogger.Log("Check passed, Modifier queued.");

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
                if (AudioDriver.I is null) return false;
                if (AudioDriver.I.IsPlaying())
                {
                    float tick = AudioDriver.I.mCachedTick;
                    if(SongCues.I.GetLastCueStartTick() > tick + 7680)
                    {
                        float time;
                        if (lastModifierTime.TryGetValue(modifier.type, out time))
                        {
                            if ((time + modifier.defaultParams.cooldown) <= AudioDriver.I.GetSongPositionSeconds(AudioDriver.TickContext.Audio)) return true;
                            else return false;

                        }
                        else
                        {
                            lastModifierTime.Add(modifier.type, 0f);
                            return true;
                        }
                        
                    }                    
                }
            }
            MelonLogger.Log("Failed check.");
            return false;
        }

        private static IEnumerator Countdown(Modifier modifier, float countdownTimer = 6)
        {
            if (Config.generalParams.countdownEnabled)
            {
                timerActive = true;
                yield return new WaitForSeconds(Config.generalParams.cooldownBetweenModifiers);
                string colortag = modifier.type == ModifierType.AA ? "<color=\"orange\">" : modifier.type == ModifierType.Psychadelia ? "<color=\"blue\">" : modifier.type == ModifierType.Mines ? "<color=\"red\">" : modifier.type == ModifierType.Speed ? "<color=\"green\">" : "";
                while (countdownTimer > 0)
                {

                    if (countdownTimer == 6) DebugText(colortag + modifier.defaultParams.name + "\nrequested by: " + modifier.defaultParams.user, 0.09f);
                    if (countdownTimer == 3) DebugText("3", 0.6f);
                    if (countdownTimer == 2) DebugText("2", 0.6f);
                    if (countdownTimer == 1) DebugText("1", 0.6f);
                    if (stopAllModifiers) yield break;


                    yield return new WaitForSeconds(1f);
                    countdownTimer--;


                }
            }
           
            timerActive = false;
            queuedModifiers.Remove(modifier);
            if (!stopAllModifiers) modifier.Activate();          
            ProcessQueue();
            yield return null;
        }

        public static void DebugText(string text, float speed)
        {
            KataConfig.I.CreateDebugText(text, debugTextPosition, 5f, null, false, speed);
        }
       
        public static void UnregisterModifier(Modifier mod)
        {
            lastModifierTime[mod.type] = AudioDriver.I.GetSongPositionSeconds(AudioDriver.TickContext.Audio);
            activeModifiers.Remove(mod);           
        }

        public static IEnumerator Reset() //was IEnumerator
        {
            stopAllModifiers = true;
            yield return new WaitForSeconds(1.5f);
           
            for (int i = activeModifiers.Count - 1;i > -1; i--) activeModifiers[i].Deactivate();
            lastModifierTime.Clear();
            timerActive = false;
            queuedModifiers.Clear();
            stopAllModifiers = false;
        }      
    }
}
