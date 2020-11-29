using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;

namespace AudicaModding
{
    public class SpeedChange : Modifier
    {
        public ModifierParams.Speed speedParams;
        private RampMode rampmode = RampMode.Up;
        private bool tempoRampActive = false;
        public SpeedChange(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.Speed _speedParams, float _amount)
        {
            type = _type;
            defaultParams = _modifierParams;
            speedParams = _speedParams;
            defaultParams.duration = _speedParams.duration;
            defaultParams.cooldown = _speedParams.cooldown;
            amount = _amount;
        }

        public override void Activate()
        {
            base.Activate();
            if (amount > speedParams.maxSpeed) amount = speedParams.maxSpeed;
            else if (amount < speedParams.minSpeed) amount = speedParams.minSpeed;
            tempoRampActive = true;
            MelonCoroutines.Start(TempoRamp());
        }

        public override void Deactivate()
        {
            tempoRampActive = true;
            MelonCoroutines.Start(TempoRamp());
        }

        public IEnumerator TempoRamp()
        {
            float progress = 0;
            while (tempoRampActive)
            {
                if (ModifierManager.stopAllModifiers)
                {
                    AudioDriver.I.SetSpeed(1f);
                    tempoRampActive = false;
                    base.Deactivate();
                    yield break;
                }
                if (rampmode == RampMode.Up)
                {
                    AudioDriver.I.SetSpeed(Mathf.Lerp(1f, amount, progress / 200f));
                    if ((amount > 1f && AudioDriver.I.mSpeed >= amount) || (amount < 1f && AudioDriver.I.mSpeed <= amount))
                    {
                        AudioDriver.I.SetSpeed(amount);
                        tempoRampActive = false;
                        rampmode = RampMode.Down;
                        MelonCoroutines.Start(Timer(defaultParams.duration));
                        yield break;
                    }
                }
                else
                {
                    AudioDriver.I.SetSpeed(Mathf.Lerp(amount, 1f, progress / 200f));
                    if ((amount < 1f && AudioDriver.I.mSpeed >= 1f) || (amount > 1f && AudioDriver.I.mSpeed <= 1f))
                    {
                        AudioDriver.I.SetSpeed(1f);
                        tempoRampActive = false;
                        base.Deactivate();
                        yield break;
                    }
                }
                progress++;
                yield return new WaitForSecondsRealtime(.002f);
                //Thread.Sleep(16);
                //yield return null;
            }
            
        }

        private enum RampMode
        {
            Up,
            Down
        }
    }
}
