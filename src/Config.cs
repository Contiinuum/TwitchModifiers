using MelonLoader;
using System.Reflection;

namespace AudicaModding
{
    public static class Config
    {
        public const string Category = "TwitchModifiers";

        public static ModifierParams.General generalParams;
        private static bool enableTwitchModifiers;
        private static bool enableCountdown;
        private static float cooldownBetweenModifiers;
        private static int maxActiveModifiers;
        private static bool showModStatus;

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
        private static float wobbleDuration;
        private static float wobbleCooldown;

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


        public static void RegisterConfig()
        {
            MelonPrefs.RegisterBool(Category, nameof(enableTwitchModifiers), true, "Enables Twitch Modifiers.");
            MelonPrefs.RegisterBool(Category, nameof(enableCountdown), true, "Enables Countdown before activating a modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(cooldownBetweenModifiers), 2f, "Cooldown before another modifier can be activated.[0, 20, 1, 2]");
            MelonPrefs.RegisterInt(Category, nameof(maxActiveModifiers), 5, "How many modifiers can be active at once.[1, 10, 1, 5]");
            MelonPrefs.RegisterBool(Category, nameof(showModStatus), true, "Shows time left for on active modifier ingame.");

            MelonPrefs.RegisterString(Category, nameof(speedTilte), "", speedTilte);
            MelonPrefs.RegisterBool(Category, nameof(speedEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(speedDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(speedCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(minSpeed), .5f, "Minimum amount of speed [0.1, 1, 0.1, 0.5]{P}");
            MelonPrefs.RegisterFloat(Category, nameof(maxSpeed), 1.5f, "Maximum amount of speed [1, 3, 0.1, 1.5]{P}");

            MelonPrefs.RegisterString(Category, nameof(aimTitle), "", aimTitle);
            MelonPrefs.RegisterBool(Category, nameof(aimAssistEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(aimAssistDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(aimAssistCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(minAimAssist), 0f, "Minimum amount of Aim Assist [0, 1, 0.1, 0]{P}");

            MelonPrefs.RegisterString(Category, nameof(psyTitle), "", psyTitle);
            MelonPrefs.RegisterBool(Category, nameof(psychedeliaEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(psychedeliaDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(psychedeliaCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(minPsychedeliaSpeed), 1f, "Minimum amount of speed [0.1, 1, 0.1, 0.5]{P}");
            MelonPrefs.RegisterFloat(Category, nameof(maxPsychedeliaSpeed), 10f, "Maximum amount of speed [1, 30, 1, 10]{P}");

            MelonPrefs.RegisterString(Category, nameof(minesTitle), "", minesTitle);
            MelonPrefs.RegisterBool(Category, nameof(minesEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(minesDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(minesCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPrefs.RegisterString(Category, nameof(wobbleTitle), "", wobbleTitle);
            MelonPrefs.RegisterBool(Category, nameof(wobbleEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(wobbleDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(wobbleCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPrefs.RegisterString(Category, nameof(invisGunsTitle), "", invisGunsTitle);
            MelonPrefs.RegisterBool(Category, nameof(invisibleGunsEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(invisibleGunsDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(invisibleGunsCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPrefs.RegisterString(Category, nameof(particlesTitle), "", particlesTitle);
            MelonPrefs.RegisterBool(Category, nameof(particlesEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(particlesDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(particlesCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(minParticles), 0f, "Minimum amount of particles [0, 1, 0.1, 0]{P}");
            MelonPrefs.RegisterFloat(Category, nameof(maxParticles), 10f, "Maximum amount of particles [1, 50, 1, 10]{P}");

            MelonPrefs.RegisterString(Category, nameof(zOffsetTitle), "", zOffsetTitle);
            MelonPrefs.RegisterBool(Category, nameof(zoffsetEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(zoffsetDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(zoffsetCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(minZoffset), -.5f, "Minimum amount of zOffset [-0.5, 0, 0.1, -0.5]");
            MelonPrefs.RegisterFloat(Category, nameof(maxZoffset), 3f, "Maximum amount of zOffset [0, 5, 0.1, 3]");

            MelonPrefs.RegisterString(Category, nameof(betterMeleesTitle), "", betterMeleesTitle);
            MelonPrefs.RegisterBool(Category, nameof(betterMeleesEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(betterMeleesDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(betterMeleesCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");

            MelonPrefs.RegisterString(Category, nameof(randomoffsetTitle), "", randomoffsetTitle);
            MelonPrefs.RegisterBool(Category, nameof(randomOffsetEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(randomOffsetDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(randomOffsetCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(minRandomOffsetX), -1.5f, "Minimum offset on X-Axis.[-2.5, 0, 0.1, -1.5]{P}");
            MelonPrefs.RegisterFloat(Category, nameof(maxRandomOffsetX), 1.5f, "Maximum offset on X-Axis.[0, 2.5, 0.1, 1.5]{P}");
            MelonPrefs.RegisterFloat(Category, nameof(minRandomOffsetY), -1.5f, "Minimum offset on Y-Axis.[-2.5, 0, 0.1, -1.5]{P}");
            MelonPrefs.RegisterFloat(Category, nameof(maxRandomOffsetY), 1.5f, "Maximum offset on Y-Axis.[0, 2.5, 0.1, 1.5]{P}");

            MelonPrefs.RegisterString(Category, nameof(scaleTitle), "", scaleTitle);
            MelonPrefs.RegisterBool(Category, nameof(scaleEnabled), true, "Enables this modifier.");
            MelonPrefs.RegisterFloat(Category, nameof(scaleDuration), 20f, "Duration of this modifier. [10, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(scaleCooldown), 20f, "Cooldown before this modifier can be activated again. [0, 60, 1, 20]");
            MelonPrefs.RegisterFloat(Category, nameof(minScale), .5f, "Minimum scale.[0.1, 1, 0.1, 0.5]{P}");
            MelonPrefs.RegisterFloat(Category, nameof(maxScale), 3f, "Maximum scale.[1, 3, 0.1, 3]{P}");

            OnModSettingsApplied();
        }

        public static void OnModSettingsApplied()
        {
            foreach(var fieldInfo in typeof(Config).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (fieldInfo.FieldType == typeof(string)) fieldInfo.SetValue(null, MelonPrefs.GetString(Category, fieldInfo.Name));
                else if (fieldInfo.FieldType == typeof(bool)) fieldInfo.SetValue(null, MelonPrefs.GetBool(Category, fieldInfo.Name));
                else if (fieldInfo.FieldType == typeof(float)) fieldInfo.SetValue(null, MelonPrefs.GetFloat(Category, fieldInfo.Name));
                else if (fieldInfo.FieldType == typeof(int)) fieldInfo.SetValue(null, MelonPrefs.GetInt(Category, fieldInfo.Name));
            }
            MelonLogger.Log("Mod settings applied");
            AssignValues();
        }

        private static void AssignValues()
        {
            generalParams = new ModifierParams.General(enableCountdown, cooldownBetweenModifiers, enableTwitchModifiers, maxActiveModifiers, showModStatus);
            speedParams = new ModifierParams.Speed(speedEnabled, speedDuration, speedCooldown, minSpeed, maxSpeed);
            aimParams = new ModifierParams.AimAssist(aimAssistEnabled, aimAssistDuration, aimAssistCooldown, minAimAssist);
            psychadeliaParams = new ModifierParams.Psychedelia(psychedeliaEnabled, psychedeliaDuration, psychedeliaCooldown, minPsychedeliaSpeed, maxPsychedeliaSpeed);
            mineParams = new ModifierParams.Mines(minesEnabled, minesDuration, minesCooldown);
            wobbleParams = new ModifierParams.Wobble(wobbleEnabled, wobbleDuration, wobbleCooldown);
            invisGunsParams = new ModifierParams.InvisGuns(invisibleGunsEnabled, invisibleGunsDuration, invisibleGunsCooldown);
            particlesParams = new ModifierParams.Particles(particlesEnabled, particlesDuration, particlesCooldown, minParticles, maxParticles);
            zOffsetParams = new ModifierParams.ZOffset(zoffsetEnabled, zoffsetDuration, zoffsetCooldown, minZoffset, maxZoffset);
            betterMeleesParams = new ModifierParams.BetterMelees(betterMeleesEnabled, betterMeleesDuration, betterMeleesCooldown);
            randomOffsetParams = new ModifierParams.RandomOffset(randomOffsetEnabled, randomOffsetDuration, randomOffsetCooldown, minRandomOffsetX, maxRandomOffsetX, minRandomOffsetY, maxRandomOffsetY);
            scaleParams = new ModifierParams.Scale(scaleEnabled, scaleDuration, scaleCooldown, minScale, maxScale);
        }
    }
}
