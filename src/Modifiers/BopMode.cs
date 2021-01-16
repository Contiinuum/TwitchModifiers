using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using TimingAttack;
using UnityEngine;
using ArenaLoader;

namespace AudicaModding
{
    public class BopMode : Modifier
    {
        public ModifierParams.BopMode bopModeParams;
        private object fadeToken;
        public BopMode(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.BopMode _bopModeParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            bopModeParams = _bopModeParams;
            defaultParams.duration = _bopModeParams.duration;
            defaultParams.cooldown = _bopModeParams.cooldown;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(ActiveTimer());
            MelonCoroutines.Start(Bop());
        }

        private IEnumerator Bop()
        {
            float oldExposure = RenderSettings.skybox.GetFloat("_Exposure");
            ArenaLoaderMod.CurrentSkyboxExposure = oldExposure;
            List<SongCues.Cue> cues = SongCues.I.mCues.cues.ToList();
            for (int i = cues.Count - 1; i >= 0; i--) if (cues[i].tick < AudioDriver.I.mCachedTick || cues[i].behavior == Target.TargetBehavior.Dodge) cues.RemoveAt(i);
            ExposureState state = ExposureState.Light;
            while (defaultParams.active)
            {
                if (cues[0].tick <= AudioDriver.I.mCachedTick)
                {
                    if(cues[0].behavior != Target.TargetBehavior.Chain && cues[0].behavior != Target.TargetBehavior.ChainStart && cues[0].velocity == 2)
                    {
                        cues.RemoveAt(0);
                        continue;
                    }
                    if (cues[0].nextCue.tick == cues[0].tick)
                    {
                        if (cues[0].behavior == Target.TargetBehavior.Melee) cues.RemoveAt(1);
                        else cues.RemoveAt(0);

                    }
                    MelonCoroutines.Stop(fadeToken);
                    float diff = cues[0].nextCue.tick - cues[0].tick;
                    float end = cues[0].nextCue.tick - (diff / 4f);
                    bool isLight = state == ExposureState.Light;
                    float target = isLight ? 0f : (ModifierManager.originalExposure / 100f) * 80f;
                    if (diff >= 120f && cues[0].behavior != Target.TargetBehavior.Melee)
                    {
                        fadeToken = MelonCoroutines.Start(Fade(end, target));
                    }
                    else
                    {
                        RenderSettings.skybox.SetFloat("_Exposure", target);
                        RenderSettings.reflectionIntensity = target;
                        ArenaLoaderMod.CurrentSkyboxReflection = 0f;
                        ArenaLoaderMod.ChangeReflectionStrength(target);
                    }
                    state = isLight ? ExposureState.Dark : ExposureState.Light;                    
                    cues.RemoveAt(0);
                }
                yield return new WaitForSecondsRealtime(.01f);
            }
            yield return null;
        }

        private IEnumerator Fade(float endTick, float targetExposure)
        {
            float oldExposure = RenderSettings.skybox.GetFloat("_Exposure");
            float oldReflection = RenderSettings.reflectionIntensity;
            ArenaLoaderMod.CurrentSkyboxExposure = oldExposure;
            float startTick = AudioDriver.I.mCachedTick;
            while (true)
            {
                float percentage = ((AudioDriver.I.mCachedTick - startTick) * 100f) / (endTick - startTick);
                float currentExp = Mathf.Lerp(oldExposure, targetExposure, percentage / 100f);
                float currentRef = Mathf.Lerp(oldReflection, targetExposure, percentage / 100f);
                RenderSettings.skybox.SetFloat("_Exposure", currentExp);
                ArenaLoaderMod.CurrentSkyboxReflection = 0f;
                ArenaLoaderMod.ChangeReflectionStrength(currentRef);
                ArenaLoaderMod.CurrentSkyboxExposure = currentExp;
                yield return new WaitForSecondsRealtime(.01f);
            }
        }

        private void ChangeExposure(float exp)
        {
            RenderSettings.skybox.SetFloat("_Exposure", exp);
            ArenaLoaderMod.CurrentSkyboxExposure = exp;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            MelonCoroutines.Stop(fadeToken);
            ChangeExposure(ModifierManager.userExposure);
        }

        private enum ExposureState
        {
            Dark,
            FadeToLight,
            FadeToDark,
            Light
        }
    }
}
