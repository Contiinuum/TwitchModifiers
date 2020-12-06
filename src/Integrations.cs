using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoreOverlay;
using TimingAttack;
using MelonLoader;

namespace AudicaModding
{
    public static class Integrations
    {
        public static bool scoreOverlayFound = false;
        public static bool timingAttackFound = false;

        public static void LookForIntegrations()
        {
            foreach(MelonMod mod in MelonHandler.Mods)
            {
                if(mod.Info.SystemType.Name == nameof(ScoreOverlayMod))
                {
                    var scoreVersion = new Version(mod.Info.Version);
                    var lastUnsupportedVersion = new Version("2.0.2");
                    var result = scoreVersion.CompareTo(lastUnsupportedVersion);
                    if (result > 0)
                    {
                        scoreOverlayFound = true;
                        MelonLogger.Log("Score Overlay found");
                               
                    }
                    else
                    {
                        MelonLogger.Log("Score Overlay version not compatible. Update Score Overlay to use it with Twitch Modifiers.");
                        scoreOverlayFound = false;
                    }
                }                       
                if (mod.Info.SystemType.Name == nameof(TimingAttackClass))
                {
                    var scoreVersion = new Version(mod.Info.Version);
                    var lastUnsupportedVersion = new Version("0.0.0");
                    var result = scoreVersion.CompareTo(lastUnsupportedVersion);
                    if (result > 0)
                    {
                        timingAttackFound = true;
                        MelonLogger.Log("Timing Attack found");
                    }
                    else
                    {
                        MelonLogger.Log("Timing Attack version not compatible. Update Timing Attack to use it with Twitch Modifiers.");
                        timingAttackFound = false;
                    }
                }
            }                  
        }
    }
}
