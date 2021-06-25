using MelonLoader;
using System.Reflection;

namespace AudicaModding
{
    public static class Config
    {
#pragma warning disable CS0649

        public const string Category = "TwitchModifiers";

        public static ModifierParams.General generalParams;
        private static bool enableTwitchModifiers;
        private static bool showOnScoreOverlay;
        private static bool allowScoreDisablingMods;
        private static bool enableCountdown;
        private static bool useChannelPoints;
        private static float cooldownBetweenModifiers;
        private static int maxActiveModifiers;
        private static bool showModStatus;
        private static bool disableForOst;

        public static ModifierParams.Speed speedParams;
        private static string speedTilte = "[Header]Speed Change";
        private static bool speedEnabled;
        private static float speedDuration;
        private static float speedCooldown;
        private static float minSpeed;
        private static float maxSpeed;

        public static ModifierParams.AimAssist aimParams;
        private static string aimTitle = "[Header]Aim Assist";
        private static bool aimAssistEnabled;
        private static float aimAssistDuration;
        private static float aimAssistCooldown;
        private static float minAimAssist;

        public static ModifierParams.Psychedelia psychadeliaParams;
        private static string psyTitle = "[Header]Psychedelia";
        private static bool psychedeliaEnabled;
        private static float psychedeliaDuration;
        private static float psychedeliaCooldown;
        private static float minPsychedeliaSpeed;
        private static float maxPsychedeliaSpeed;

        public static ModifierParams.Mines mineParams;
        private static string minesTitle = "[Header]Mines";
        private static bool minesEnabled;
        private static float minesDuration;
        private static float minesCooldown;

        public static ModifierParams.Wobble wobbleParams;
        private static string wobbleTitle = "[Header]Wobble";
        private static bool wobbleEnabled;
        private static bool wombleEnabled;
        private static bool woobleEnabled;
        private static bool wroblEnabled;
        private static float wobbleDuration;
        private static float wobbleCooldown;
        private static float wobbleMinSpeed;
        private static float wobbleMaxSpeed;

        public static ModifierParams.InvisGuns invisGunsParams;
        private static string invisGunsTitle = "[Header]Invisible Guns";
        private static bool invisibleGunsEnabled;
        private static float invisibleGunsDuration;
        private static float invisibleGunsCooldown;

        public static ModifierParams.Particles particlesParams;
        private static string particlesTitle = "[Header]Particles";
        private static bool particlesEnabled;
        private static float particlesDuration;
        private static float particlesCooldown;
        private static float minParticles;
        private static float maxParticles;

        public static ModifierParams.ZOffset zOffsetParams;
        private static string zOffsetTitle = "[Header]zOffset";
        private static bool zoffsetEnabled;
        private static float zoffsetDuration;
        private static float zoffsetCooldown;
        private static float minZoffset;
        private static float maxZoffset;

        public static ModifierParams.BetterMelees betterMeleesParams;
        private static string betterMeleesTitle = "[Header]Better Melees";
        private static bool betterMeleesEnabled;
        private static float betterMeleesDuration;
        private static float betterMeleesCooldown;

        public static ModifierParams.RandomOffset randomOffsetParams;
        private static string randomoffsetTitle = "[Header]Random Offset";
        private static bool randomOffsetEnabled;
        private static float randomOffsetDuration;
        private static float randomOffsetCooldown;
        private static float minRandomOffsetX;
        private static float maxRandomOffsetX;
        private static float minRandomOffsetY;
        private static float maxRandomOffsetY;

        public static ModifierParams.Scale scaleParams;
        private static string scaleTitle = "[Header]Scale";
        private static bool scaleEnabled;
        private static float scaleDuration;
        private static float scaleCooldown;
        private static float minScale;
        private static float maxScale;

        public static ModifierParams.RandomColors randomColorParams;
        private static string randomColorsTitle = "[Header]Random Colors";
        private static bool randomColorsEnabled;
        private static float randomColorsDuration;
        private static float randomColorsCooldown;

        public static ModifierParams.ColorSwap colorSwapParams;
        private static string colorSwapTitle = "[Header]Color Swap";
        private static bool colorSwapEnabled;
        private static float colorSwapDuration;
        private static float colorSwapCooldown;

        public static ModifierParams.StreamMode streamModeParams;
        private static string streamModeTitle = "[Header]Stream Mode";
        private static bool streamModeEnabled;
        private static float streamModeDuration;
        private static float streamModeCooldown;
        private static int maxStreamSpeed;

        public static ModifierParams.HiddenTelegraphs hiddenTelegraphsParams;
        private static string hiddenTelegraphsTitle = "[Header]Hidden Telegraphs";
        private static bool hiddenTelegraphsEnabled;
        private static float hiddenTelegraphsDuration;
        private static float hiddenTelegraphsCooldown;

        public static ModifierParams.UnifyColors unifyColorsParams;
        private static string unifyColorsTitle = "[Header]Unify Colors";
        private static bool unifyColorsEnabled;
        private static float unifyColorsDuration;
        private static float unifyColorsCooldown;

        /*public static ModifierParams.TimingAttack timingAttackParams;
        private static string timingAttackTitle = "[Header]Timing Attack";
        private static bool timingAttackEnabled;
        private static float timingAttackDuration;
        private static float timingAttackCooldown;

        public static ModifierParams.Nuke nukeParams;
        private static string nukeTitle = "[Header]Nuke";
        private static bool nukeEnabled;
        private static float nukeDuration;
        private static float nukeCooldown;*/

        public static ModifierParams.StutterChains stutterChainParams;
        private static string stutterChainTitle = "[Header]Stutter Chains";
        private static bool stutterChainsEnabled;
        private static float stutterChainsDuration;
        private static float stutterChainsCooldown;
        private static float stutterChainsMin;
        private static float stutterChainsMax;
        /*
        public static ModifierParams.BopMode bopModeParams;
        private static string bopModeTitle = "[Header]Bop Mode";
        private static bool bopModeEnabled;
        private static float bopModeDuration;
        private static float bopModeCooldown;
        */

        public static void RegisterConfig()
        {
            MelonPreferences.CreateEntry(Category, nameof(enableTwitchModifiers), true, "Enables Twitch Modifiers.");
            MelonPreferences.CreateEntry(Category, nameof(showOnScoreOverlay), true, "Shows active modifiers and cooldowns on Score Overlay (requries Score Overlay by octo).");
            MelonPreferences.CreateEntry(Category, nameof(enableCountdown), true, "Enables Countdown before activating a modifier.");
            MelonPreferences.CreateEntry(Category, nameof(useChannelPoints), false, "Requires viewers to redeem channel points to use modifiers.");
            MelonPreferences.CreateEntry(Category, nameof(allowScoreDisablingMods), true, "Turning this off disables some modifiers that prevent you from submitting scores to the leaderboards.");
            MelonPreferences.CreateEntry(Category, nameof(cooldownBetweenModifiers), 2f, "Cooldown before another modifier can be activated.[0, 20, 1, 2]");
            MelonPreferences.CreateEntry(Category, nameof(maxActiveModifiers), 5, "How many modifiers can be active at once.[1, 10, 1, 5]");
            MelonPreferences.CreateEntry(Category, nameof(showModStatus), true, "Shows time left for on active modifier ingame.");
            MelonPreferences.CreateEntry(Category, nameof(disableForOst), true, "Disables Twitch Modifiers during OST songs.");

            MelonPreferences.CreateEntry(Category, nameof(speedTilte), "", "[Header]Speed");
            MelonPreferences.CreateEntry(Category, nameof(speedEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(speedDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(speedCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(minSpeed), .5f, "Minimum amount of speed [0.1, 1, 0.1, 0.5]{P}");
            MelonPreferences.CreateEntry(Category, nameof(maxSpeed), 1.5f, "Maximum amount of speed [1, 3, 0.1, 1.5]{P}");

            MelonPreferences.CreateEntry(Category, nameof(aimTitle), "", "[Header]Aim Assist");
            MelonPreferences.CreateEntry(Category, nameof(aimAssistEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(aimAssistDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(aimAssistCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(minAimAssist), 0f, "Minimum amount of Aim Assist [0, 1, 0.1, 0]{P}");

            MelonPreferences.CreateEntry(Category, nameof(psyTitle), "", "[Header]Psychedelia");
            MelonPreferences.CreateEntry(Category, nameof(psychedeliaEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(psychedeliaDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(psychedeliaCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(minPsychedeliaSpeed), 1f, "Minimum amount of speed [0.1, 1, 0.1, 0.5]{P}");
            MelonPreferences.CreateEntry(Category, nameof(maxPsychedeliaSpeed), 10f, "Maximum amount of speed [1, 30, 1, 10]{P}");

            MelonPreferences.CreateEntry(Category, nameof(minesTitle), "", "[Header]Mines");
            MelonPreferences.CreateEntry(Category, nameof(minesEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(minesDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(minesCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPreferences.CreateEntry(Category, nameof(wobbleTitle), "", "[Header]Wobble");
            MelonPreferences.CreateEntry(Category, nameof(wobbleEnabled), true, "Enables this modifier and all of it's modes.");
            MelonPreferences.CreateEntry(Category, nameof(wombleEnabled), true, "Womble is wobble with speed arguments.");
            MelonPreferences.CreateEntry(Category, nameof(woobleEnabled), true, "A slightly more retarded wobble.");
            MelonPreferences.CreateEntry(Category, nameof(wroblEnabled), true, "Wobble gone full retard.");
            MelonPreferences.CreateEntry(Category, nameof(wobbleDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(wobbleCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(wobbleMinSpeed), .5f, "Minimum amount of speed [0.5, 1, 0.1, 0.5]{P}");
            MelonPreferences.CreateEntry(Category, nameof(wobbleMaxSpeed), 1.5f, "Maximum amount of speed [1, 2, 1, 1.5]{P}");

            MelonPreferences.CreateEntry(Category, nameof(invisGunsTitle), "", "[Header]Invisible Guns");
            MelonPreferences.CreateEntry(Category, nameof(invisibleGunsEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(invisibleGunsDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(invisibleGunsCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPreferences.CreateEntry(Category, nameof(particlesTitle), "", "[Header]Particles");
            MelonPreferences.CreateEntry(Category, nameof(particlesEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(particlesDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(particlesCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(minParticles), 0f, "Minimum amount of particles [0, 1, 0.1, 0]{P}");
            MelonPreferences.CreateEntry(Category, nameof(maxParticles), 10f, "Maximum amount of particles [1, 50, 1, 10]{P}");

            MelonPreferences.CreateEntry(Category, nameof(zOffsetTitle), "", "[Header]zOffset");
            MelonPreferences.CreateEntry(Category, nameof(zoffsetEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(zoffsetDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(zoffsetCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(minZoffset), -.5f, "Minimum amount of zOffset [-0.5, 0, 0.1, -0.5]");
            MelonPreferences.CreateEntry(Category, nameof(maxZoffset), 3f, "Maximum amount of zOffset [0, 5, 0.1, 3]");

            MelonPreferences.CreateEntry(Category, nameof(betterMeleesTitle), "", "[Header]Better Melees");
            MelonPreferences.CreateEntry(Category, nameof(betterMeleesEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(betterMeleesDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(betterMeleesCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPreferences.CreateEntry(Category, nameof(randomoffsetTitle), "", "[Header]Random Offset");
            MelonPreferences.CreateEntry(Category, nameof(randomOffsetEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(randomOffsetDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(randomOffsetCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(minRandomOffsetX), -1.5f, "Minimum offset on X-Axis.[-2.5, 0, 0.1, -1.5]{P}");
            MelonPreferences.CreateEntry(Category, nameof(maxRandomOffsetX), 1.5f, "Maximum offset on X-Axis.[0, 2.5, 0.1, 1.5]{P}");
            MelonPreferences.CreateEntry(Category, nameof(minRandomOffsetY), -1.5f, "Minimum offset on Y-Axis.[-2.5, 0, 0.1, -1.5]{P}");
            MelonPreferences.CreateEntry(Category, nameof(maxRandomOffsetY), 1.5f, "Maximum offset on Y-Axis.[0, 2.5, 0.1, 1.5]{P}");

            MelonPreferences.CreateEntry(Category, nameof(scaleTitle), "", "[Header]Scale");
            MelonPreferences.CreateEntry(Category, nameof(scaleEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(scaleDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(scaleCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(minScale), .5f, "Minimum scale.[0.1, 1, 0.1, 0.5]{P}");
            MelonPreferences.CreateEntry(Category, nameof(maxScale), 3f, "Maximum scale.[1, 3, 0.1, 3]{P}");

            MelonPreferences.CreateEntry(Category, nameof(randomColorsTitle), "", "[Header]Random Colors");
            MelonPreferences.CreateEntry(Category, nameof(randomColorsEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(randomColorsDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(randomColorsCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPreferences.CreateEntry(Category, nameof(colorSwapTitle), "", "[Header]Color Swap");
            MelonPreferences.CreateEntry(Category, nameof(colorSwapEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(colorSwapDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(colorSwapCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPreferences.CreateEntry(Category, nameof(streamModeTitle), "", "[Header]Stream Mode");
            MelonPreferences.CreateEntry(Category, nameof(streamModeEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(streamModeDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(streamModeCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(maxStreamSpeed), 16, "Maximum interval (note value).[4, 32, 2, 16]");

            MelonPreferences.CreateEntry(Category, nameof(hiddenTelegraphsTitle), "", "[Header]Hidden Telegraphs");
            MelonPreferences.CreateEntry(Category, nameof(hiddenTelegraphsEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(hiddenTelegraphsDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(hiddenTelegraphsCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPreferences.CreateEntry(Category, nameof(unifyColorsTitle), "", "[Header]Unify Colors");
            MelonPreferences.CreateEntry(Category, nameof(unifyColorsEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(unifyColorsDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(unifyColorsCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            /*MelonPreferences.CreateEntry(Category, nameof(timingAttackTitle), "", timingAttackTitle);
            MelonPreferences.CreateEntry(Category, nameof(timingAttackEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(timingAttackDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(timingAttackCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPreferences.CreateEntry(Category, nameof(nukeTitle), "", nukeTitle);
            MelonPreferences.CreateEntry(Category, nameof(nukeEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(nukeDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(nukeCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 600, 10, 120]");*/

            MelonPreferences.CreateEntry(Category, nameof(stutterChainTitle), "", "[Header]Stutter Chains");
            MelonPreferences.CreateEntry(Category, nameof(stutterChainsEnabled), true, "Enables this modifier.");
            MelonPreferences.CreateEntry(Category, nameof(stutterChainsDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPreferences.CreateEntry(Category, nameof(stutterChainsCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 600, 10, 120]");
            MelonPreferences.CreateEntry(Category, nameof(stutterChainsMin), .5f, "Minimum Stutter Rotation.[1, 50, 1, 1]{P}");
            MelonPreferences.CreateEntry(Category, nameof(stutterChainsMax), 3f, "Maximum Stutter Rotation.[6, 100, 1, 100]{P}");

            OnPreferencesSaved();
        }

        public static void OnPreferencesSaved()
        {
            foreach(var fieldInfo in typeof(Config).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (fieldInfo.FieldType == typeof(string)) fieldInfo.SetValue(null, MelonPreferences.GetEntryValue<string>(Category, fieldInfo.Name));
                else if (fieldInfo.FieldType == typeof(bool)) fieldInfo.SetValue(null, MelonPreferences.GetEntryValue<bool>(Category, fieldInfo.Name));
                else if (fieldInfo.FieldType == typeof(float)) fieldInfo.SetValue(null, MelonPreferences.GetEntryValue<float>(Category, fieldInfo.Name));
                else if (fieldInfo.FieldType == typeof(int)) fieldInfo.SetValue(null, MelonPreferences.GetEntryValue<int>(Category, fieldInfo.Name));
            }
            MelonLogger.Msg("Mod settings applied");
            AssignValues();
        }

        private static void AssignValues()
        {
            generalParams = new ModifierParams.General(enableCountdown, showOnScoreOverlay, allowScoreDisablingMods , useChannelPoints, cooldownBetweenModifiers, enableTwitchModifiers, maxActiveModifiers, showModStatus, disableForOst);
            speedParams = new ModifierParams.Speed(speedEnabled, speedDuration, speedCooldown, minSpeed, maxSpeed);
            aimParams = new ModifierParams.AimAssist(aimAssistEnabled, aimAssistDuration, aimAssistCooldown, minAimAssist);
            psychadeliaParams = new ModifierParams.Psychedelia(psychedeliaEnabled, psychedeliaDuration, psychedeliaCooldown, minPsychedeliaSpeed, maxPsychedeliaSpeed);
            mineParams = new ModifierParams.Mines(minesEnabled, minesDuration, minesCooldown);
            wobbleParams = new ModifierParams.Wobble(wobbleEnabled, wombleEnabled, woobleEnabled, wroblEnabled, wobbleDuration, wobbleCooldown, wobbleMinSpeed, wobbleMaxSpeed);
            invisGunsParams = new ModifierParams.InvisGuns(invisibleGunsEnabled, invisibleGunsDuration, invisibleGunsCooldown);
            particlesParams = new ModifierParams.Particles(particlesEnabled, particlesDuration, particlesCooldown, minParticles, maxParticles);
            zOffsetParams = new ModifierParams.ZOffset(zoffsetEnabled, zoffsetDuration, zoffsetCooldown, minZoffset, maxZoffset);
            betterMeleesParams = new ModifierParams.BetterMelees(betterMeleesEnabled, betterMeleesDuration, betterMeleesCooldown);
            randomOffsetParams = new ModifierParams.RandomOffset(randomOffsetEnabled, randomOffsetDuration, randomOffsetCooldown, minRandomOffsetX, maxRandomOffsetX, minRandomOffsetY, maxRandomOffsetY);
            scaleParams = new ModifierParams.Scale(scaleEnabled, scaleDuration, scaleCooldown, minScale, maxScale);
            randomColorParams = new ModifierParams.RandomColors(randomColorsEnabled, randomColorsDuration, randomColorsCooldown);
            colorSwapParams = new ModifierParams.ColorSwap(colorSwapEnabled, colorSwapDuration, colorSwapCooldown);
            streamModeParams = new ModifierParams.StreamMode(streamModeEnabled, streamModeDuration, streamModeCooldown, maxStreamSpeed);
            hiddenTelegraphsParams = new ModifierParams.HiddenTelegraphs(hiddenTelegraphsEnabled, hiddenTelegraphsDuration, hiddenTelegraphsCooldown);
            unifyColorsParams = new ModifierParams.UnifyColors(unifyColorsEnabled, unifyColorsDuration, unifyColorsCooldown);
            //timingAttackParams = new ModifierParams.TimingAttack(timingAttackEnabled, timingAttackDuration, timingAttackCooldown);
            //nukeParams = new ModifierParams.Nuke(nukeEnabled, nukeDuration, nukeCooldown);
            stutterChainParams = new ModifierParams.StutterChains(stutterChainsEnabled, stutterChainsDuration, stutterChainsCooldown, stutterChainsMin, stutterChainsMax);
            //bopModeParams = new ModifierParams.BopMode(bopModeEnabled, bopModeDuration, bopModeCooldown);
        }
    }
}
