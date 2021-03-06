﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoreOverlay;
using MelonLoader;

namespace AudicaModding
{
    public static class Integrations
    {
        public static bool scoreOverlayFound = false;
        //public static bool timingAttackFound = false;
        public static bool arenaLoaderFound = false;
        public static bool particleKillerFound = false;

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
                        MelonLogger.Msg("Score Overlay found");
                               
                    }
                    else
                    {
                        MelonLogger.Warning("Score Overlay version not compatible. Update Score Overlay to use it with Twitch Modifiers.");
                        scoreOverlayFound = false;
                    }
                }                       
                /*else if (mod.Info.SystemType.Name == nameof(TimingAttackClass))
                {
                    var scoreVersion = new Version(mod.Info.Version);
                    var lastUnsupportedVersion = new Version("0.0.0");
                    var result = scoreVersion.CompareTo(lastUnsupportedVersion);
                    if (result > 0)
                    {
                        timingAttackFound = true;
                        MelonLogger.Msg("Timing Attack found");
                    }
                    else
                    {
                        MelonLogger.Warning("Timing Attack version not compatible. Update Timing Attack to use it with Twitch Modifiers.");
                        timingAttackFound = false;
                    }
                }*/
                else if (mod.Assembly.GetName().Name == "ArenaLoader")
                {
                    var scoreVersion = new Version(mod.Info.Version);
                    var lastUnsupportedVersion = new Version("0.2.1");
                    var result = scoreVersion.CompareTo(lastUnsupportedVersion);
                    if (result > 0)
                    {
                        arenaLoaderFound = true;
                        MelonLogger.Msg("Arena Loader found");
                    }
                    else
                    {
                        MelonLogger.Warning("Arena Loader version not compatible. Update Arena Loader to use it with Twitch Modifiers.");
                        arenaLoaderFound = false;
                    }
                }
                else if (mod.Assembly.GetName().Name == "ParticleKiller")
                {
                    var scoreVersion = new Version(mod.Info.Version);
                    var lastUnsupportedVersion = new Version("0.0.0");
                    var result = scoreVersion.CompareTo(lastUnsupportedVersion);
                    if (result > 0)
                    {
                        particleKillerFound = true;
                        MelonLogger.Msg("Particle Killer found");
                    }
                    else
                    {
                        MelonLogger.Warning("Particle Killer version not compatible. Update Particle Killer to use it with Twitch Modifiers.");
                        particleKillerFound = false;
                    }
                }
            }                  
        }
    }
}
