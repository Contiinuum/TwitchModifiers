using MelonLoader;
using UnityEngine;
using Harmony;
using System;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using System.Linq;
using Il2CppSystem;
using System.Collections.Generic;

namespace AudicaModding
{
    public class CommandManager : MelonMod
    {

        public static class BuildInfo
        {
            public const string Name = "TwitchModifiers";  // Name of the Mod.  (MUST BE SET)
            public const string Author = "Continuum"; // Author of the Mod.  (Set as null if none)
            public const string Company = null; // Company that made the Mod.  (Set as null if none)
            public const string Version = "2.0.9"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none) 
        }
        
        public override void OnApplicationStart()
        {
            Config.RegisterConfig();
            Integrations.LookForIntegrations();
        }

        public override void OnPreferencesSaved()
        {
            Config.OnPreferencesSaved();
        }

        public static void RegisterModifier(ModifierType type, string user, string color)
        {
            RegisterModifier(type, 0, user, color);
        }

        public static void RegisterModifier(ModifierType type, float amount, string user, string color)
        {          
            CreateModifier(type, amount, user, color, false);
        }

        public static void CreateModifier(ModifierType type, float amount, string user, string color, bool fromNuke = true)
        {
            Modifier mod = null;
            switch (type)
            {
                case ModifierType.Speed:
                    if (Config.speedParams.enabled)
                    {
                        if (!Config.generalParams.allowScoreDisablingMods && amount < 1f) return;
                        mod = new SpeedChange(type, new ModifierParams.Default("Speed", user, color), Config.speedParams, amount);
                    }                      
                    break;
                case ModifierType.AA:
                    if (Config.aimParams.enabled) mod = new AimAssistChange(type, new ModifierParams.Default("AA", user, color), Config.aimParams, amount);
                    break;
                case ModifierType.Psychedelia:
                    if (Config.psychadeliaParams.enabled) mod = new Psychadelia(type, new ModifierParams.Default("Psychedelia", user, color), Config.psychadeliaParams, amount);
                    break;
                case ModifierType.Mines:
                    if (Config.mineParams.enabled) mod = new Mines(type, new ModifierParams.Default("Mines", user, color), Config.mineParams);
                    break;
                case ModifierType.Wobble:
                    if (Config.wobbleParams.enabled) mod = new Wobble(type, new ModifierParams.Default("Wobble", user, color), Config.wobbleParams, amount);
                    break;
                case ModifierType.InvisGuns:
                    if (Config.invisGunsParams.enabled) mod = new InvisibleGuns(type, new ModifierParams.Default("Invis Guns", user, color), Config.invisGunsParams);
                    break;
                case ModifierType.Particles:
                    if (Config.particlesParams.enabled) mod = new Particles(type, new ModifierParams.Default("Particles", user, color), Config.particlesParams, amount);
                    break;
                case ModifierType.ZOffset:
                    if (Config.zOffsetParams.enabled) mod = new ZOffset(type, new ModifierParams.Default("zOffset", user, color), Config.zOffsetParams, amount);
                    break;
                case ModifierType.BetterMelees:
                    if (Config.betterMeleesParams.enabled) mod = new BetterMelees(type, new ModifierParams.Default("Better Melees", user, color), Config.betterMeleesParams);
                    break;
                case ModifierType.RandomOffset:
                    if (Config.randomOffsetParams.enabled) mod = new RandomOffset(type, new ModifierParams.Default("Random Offset", user, color), Config.randomOffsetParams);
                    break;
                case ModifierType.Scale:
                    if (Config.scaleParams.enabled) mod = new Scale(type, new ModifierParams.Default("Scale", user, color), Config.scaleParams, amount);
                    break;
                case ModifierType.RandomColors:
                    if (Config.randomColorParams.enabled) mod = new RandomColors(type, new ModifierParams.Default("Random Colors", user, color), Config.randomColorParams);
                    break;
                case ModifierType.ColorSwap:
                    if (Config.colorSwapParams.enabled) mod = new ColorSwap(type, new ModifierParams.Default("Color Swap", user, color), Config.colorSwapParams);
                    break;
                case ModifierType.StreamMode:
                    if (Config.streamModeParams.enabled)
                    {
                        if (!Config.generalParams.allowScoreDisablingMods) return;
                        mod = new StreamMode(type, new ModifierParams.Default("Stream Mode", user, color), Config.streamModeParams);
                    } 
                    break;
                case ModifierType.HiddenTelegraphs:
                    if (Config.hiddenTelegraphsParams.enabled) mod = new HiddenTelegraphs(type, new ModifierParams.Default("Hidden Teles", user, color), Config.hiddenTelegraphsParams);
                    break;
                case ModifierType.UnifyColors:
                    if (Config.unifyColorsParams.enabled) mod = new UnifyColors(type, new ModifierParams.Default("Unify Colors", user, color), Config.unifyColorsParams);
                    break;
                /*case ModifierType.TimingAttack:
                    if (Config.timingAttackParams.enabled) mod = new TimingAttack(type, new ModifierParams.Default("Timing Attack", user, color), Config.timingAttackParams);
                    break;
                case ModifierType.Nuke:
                    if (Config.nukeParams.enabled) mod = new Nuke(type, new ModifierParams.Default("Nuke", user, color), Config.nukeParams);
                    break;*/
                case ModifierType.StutterChains:
                    if (Config.stutterChainParams.enabled) mod = new StutterChains(type, new ModifierParams.Default("Stutter Chains", user, color), Config.stutterChainParams, amount);
                    break;
                //case ModifierType.BopMode:
                    //if(Config.bopModeParams.enabled) mod = new BopMode(type, new ModifierParams.Default("Lightshow", user, color), Config.bopModeParams);
                    break;
                default:
                    return;
            }
            if (mod is null) return;
            ModifierManager.AddModifierToQueue(mod, fromNuke);
        }

        public override void OnUpdate()
        {

            /*if (Input.GetKeyDown(KeyCode.A)) DebugCommand("!speed 150");
             if (Input.GetKeyDown(KeyCode.S)) DebugCommand("!aa 0");
             if (Input.GetKeyDown(KeyCode.D)) DebugCommand("!psy 200");
             if (Input.GetKeyDown(KeyCode.F)) DebugCommand("!mines");
             if (Input.GetKeyDown(KeyCode.G)) DebugCommand("!invisguns");
             if (Input.GetKeyDown(KeyCode.H)) DebugCommand("!wobble 150");
             if (Input.GetKeyDown(KeyCode.J)) DebugCommand("!particles 150");
             if (Input.GetKeyDown(KeyCode.K)) DebugCommand("!zoffset 150");
             if (Input.GetKeyDown(KeyCode.L)) DebugCommand("!bettermelees");
             if (Input.GetKeyDown(KeyCode.Y)) DebugCommand("!randomoffset");
             if (Input.GetKeyDown(KeyCode.X)) DebugCommand("!scale 150");
             if (Input.GetKeyDown(KeyCode.C)) DebugCommand("!randomcolors");
             if (Input.GetKeyDown(KeyCode.V)) DebugCommand("!colorswap");
             if (Input.GetKeyDown(KeyCode.B)) DebugCommand("!streammode");
             if (Input.GetKeyDown(KeyCode.N)) DebugCommand("!hiddenteles");            
             if (Input.GetKeyDown(KeyCode.M)) DebugCommand("!dropnuke");
             if (Input.GetKeyDown(KeyCode.O)) DebugCommand("!bopmode");
             if (Input.GetKeyDown(KeyCode.P)) DebugCommand("!stutterchains");
             */

        }

        /*private void DebugCommand(string command)
        {
            TwitchHandler.ParseCommand(command, "conti", "color=#FFFFFF", "yaddayaddaa");
        }*/
    }
}
