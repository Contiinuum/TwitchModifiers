using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace AudicaModding
{
    public class CooldownManager
    {
        private static Dictionary<ModifierType, float> lastModifierTime = new Dictionary<ModifierType, float>();
        private static List<Modifier> modifiersOnCooldown = new List<Modifier>();

        /*public static void SetLastModifierTime(Modifier modifier)
        {
            lastModifierTime[modifier.type] = AudioDriver.I.GetSongPositionSeconds(AudioDriver.TickContext.Audio);
        }*/

        public static void ResetCooldowns()
        {
            lastModifierTime.Clear();
        }

        /*
        public static void PutModifierOnCooldown(Modifier modifier)
        {
            modifiersOnCooldown.Add(modifier);
        }
        */
        /*
        public static bool IsOnCooldown(ModifierType type)
        {
            if (modifiersOnCooldown.Count == 0) return false;

            foreach(Modifier mod in modifiersOnCooldown)
            {
                if(type == mod.type)
                {
                    if (mod.remainingCooldown > 0) return true;
                }
            }
            return false;
        }
        */
        public static void RemoveFromList(Modifier mod)
        {
            modifiersOnCooldown.Remove(mod);
        }
    }
}
