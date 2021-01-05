using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using TimingAttack;
using UnityEngine;

namespace AudicaModding
{
    public class BopMode : Modifier
    {
        public ModifierParams.BopMode bopModeParams;
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
            AudicaMod.currentSkyboxExposure = oldExposure;
            List<SongCues.Cue> cues = SongCues.I.mCues.cues.ToList();
            while (defaultParams.active)
            {
                float currentExp;
                float offset = cues.First().tick % 480;
                int mod = (int)(AudioDriver.I.mCachedTick) % 960;               
                float tick = AudioDriver.I.mCachedTick - mod + offset;
                if(mod >= 840 - offset && mod <= 960 - offset)
                {
                   if(cues.Any(cue => cue.tick == (int)tick))
                   {
                        currentExp = Mathf.Lerp(0f, ModifierManager.originalExposure, (mod - 840f + offset) / 120f);
                        ChangeExposure(currentExp);
                   }                 
                }
                else if(mod >= 360 - offset&& mod <= 480 - offset)
                {
                    if (SongCues.I.mCues.cues.Any(cue => cue.tick == tick))
                    {
                        currentExp = Mathf.Lerp(ModifierManager.originalExposure, 0f, (mod - 360f + offset) / 120f);
                        ChangeExposure(currentExp);
                    }
                }
                yield return new WaitForSecondsRealtime(0.01f);

            }
            yield return null;
        }

        private void ChangeExposure(float exp)
        {
            RenderSettings.skybox.SetFloat("_Exposure", exp);
            AudicaMod.currentSkyboxExposure = exp;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            ChangeExposure(ModifierManager.userExposure);
        }
    }
}
