using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace AudicaModding
{
    public class Particles : Modifier
    {
        public ModifierParams.Particles particlesParams;
        public Particles(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.Particles _particlesParams, float _amount)
        {
            type = _type;
            defaultParams = _modifierParams;
            particlesParams = _particlesParams;
            defaultParams.duration = _particlesParams.duration;
            defaultParams.cooldown = _particlesParams.cooldown;
            amount = _amount;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(ActiveTimer(defaultParams.duration));
            SetParticleScale(amount);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            SetParticleScale(1f);
        }

        public void SetParticleScale(float particleAmount)
        {
            SongCues.Cue[] songCues = SongCues.I.mCues.cues;
            for (int i = 0; i < songCues.Length; i++)
            {
                songCues[i].particleReductionScale = particleAmount;
            }
        }
    }
}
