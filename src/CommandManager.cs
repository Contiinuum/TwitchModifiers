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
            public const string Version = "0.1.0"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
        }

        public override void OnApplicationStart()
        {
            HarmonyInstance instance = HarmonyInstance.Create("TwitchModifiers");
            Config.RegisterConfig();
        }

        public override void OnModSettingsApplied()
        {
            Config.OnModSettingsApplied();
        }

        public static void RegisterModifier(ModifierType type, string user)
        {
            RegisterModifier(type, 0, user);
        }

        public static void RegisterModifier(ModifierType type, float amount, string user)
        {
            MelonLogger.Log("Checking active modifiers count...");
            if(ModifierManager.activeModifiers.Count > 0)
            {
                foreach(Modifier activeMod in ModifierManager.activeModifiers)
                {
                    if(activeMod.type == type)
                    {
                        return;
                    }
                    if((type == ModifierType.Speed && activeMod.type == ModifierType.Wobble) || (type == ModifierType.Wobble && activeMod.type == ModifierType.Speed))
                    {
                        return;
                    }
                }
            }
            MelonLogger.Log("Check passed. Creating mod...");
            Modifier mod = null;
            switch (type)
            {
                case ModifierType.Speed:
                    if(Config.speedParams.enabled) mod = new SpeedChange(type, new ModifierParams.Default("Speed Change", user), Config.speedParams, amount);
                    break;
                case ModifierType.AA:
                    if (Config.aimParams.enabled) mod = new AimAssistChange(type, new ModifierParams.Default("Aim Assist Lower", user), Config.aimParams, amount);
                    break;
                case ModifierType.Psychadelia:
                    if (Config.psychadeliaParams.enabled) mod = new Psychadelia(type, new ModifierParams.Default("Psychedelia", user), Config.psychadeliaParams, amount);
                    break;
                case ModifierType.Mines:
                    if (Config.mineParams.enabled) mod = new Mines(type, new ModifierParams.Default("Mines", user), Config.mineParams);
                    break;
                case ModifierType.Wobble:
                    if (Config.wobbleParams.enabled) mod = new Wobble(type, new ModifierParams.Default("Wobble", user), Config.wobbleParams);
                    break;
                case ModifierType.InvisGuns:
                    if (Config.invisGunsParams.enabled) mod = new InvisibleGuns(type, new ModifierParams.Default("Invisible Guns", user), Config.invisGunsParams);
                    break;
                case ModifierType.Particles:
                    if (Config.particlesParams.enabled) mod = new Particles(type, new ModifierParams.Default("Particles", user), Config.particlesParams, amount);
                    break;
                case ModifierType.ZOffset:
                    if (Config.zOffsetParams.enabled) mod = new ZOffset(type, new ModifierParams.Default("zOffset", user), Config.zOffsetParams, amount);
                    break;
                case ModifierType.BetterMelees:
                    if (Config.betterMeleesParams.enabled) mod = new BetterMelees(type, new ModifierParams.Default("Better Melees", user), Config.betterMeleesParams);
                    break;
                case ModifierType.RandomOffset:
                    if (Config.randomOffsetParams.enabled) mod = new RandomOffset(type, new ModifierParams.Default("Random Offset", user), Config.randomOffsetParams);
                    break;
                case ModifierType.Scale:
                    if (Config.scaleParams.enabled) mod = new Scale(type, new ModifierParams.Default("Scale", user), Config.scaleParams, amount);
                    break;
                case ModifierType.RandomColors:
                    if (Config.randomColorParams.enabled) mod = new RandomColors(type, new ModifierParams.Default("Random Colors", user), Config.randomColorParams);
                    break;
                case ModifierType.ColorSwap:
                    if (Config.colorSwapParams.enabled) mod = new ColorSwap(type, new ModifierParams.Default("Color Swap", user), Config.colorSwapParams);
                    break;
                default:
                    return;
            }
            if (mod is null) return;
            MelonLogger.Log("Mod added to active modifiers.");
            ModifierManager.AddModifierToQueue(mod);
        }

        public override void OnUpdate()
        {

            /*
            if (Input.GetKeyDown(KeyCode.A)) DebugCommand("!speed 150");
            if (Input.GetKeyDown(KeyCode.S)) DebugCommand("!aa 0");
            if (Input.GetKeyDown(KeyCode.D)) DebugCommand("!psy 200");
            if (Input.GetKeyDown(KeyCode.F)) DebugCommand("!mines");
            if (Input.GetKeyDown(KeyCode.G)) DebugCommand("!invisguns");
            if (Input.GetKeyDown(KeyCode.H)) DebugCommand("!wobble 150");
            if (Input.GetKeyDown(KeyCode.J)) DebugCommand("!particles 150");
            if (Input.GetKeyDown(KeyCode.K)) DebugCommand("!zoffset 150");
            if (Input.GetKeyDown(KeyCode.Y)) DebugCommand("!bettermelees");
            if (Input.GetKeyDown(KeyCode.X)) DebugCommand("!randomoffset");
            if (Input.GetKeyDown(KeyCode.C)) DebugCommand("!scale 150");
            if (Input.GetKeyDown(KeyCode.V)) DebugCommand("!colorswap");
            */
        }

        private void DebugCommand(string command)
        {
            TwitchHandler.ParseCommand(command, "conti");
        }
    }
}
