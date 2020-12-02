using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;

namespace AudicaModding
{
    public class Wobble : Modifier
    {
        public ModifierParams.Wobble wobbleParams;
        private Mode mode;
        public Wobble(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.Wobble _wobbleParams, float _amount)
        {
            type = _type;
            defaultParams = _modifierParams;
            wobbleParams = _wobbleParams;
            defaultParams.duration = _wobbleParams.duration;
            defaultParams.cooldown = _wobbleParams.cooldown;
            mode = _amount == -3 ? Mode.Wrobl : _amount == -2 ? Mode.Wooble : _amount == 0 ? Mode.Wobble : Mode.Womble;
            amount = _amount;

            if (amount > wobbleParams.maxSpeed) amount = wobbleParams.maxSpeed;
            if (amount < wobbleParams.minSpeed) amount = wobbleParams.minSpeed;
        }

        public override void Activate()
        {
            base.Activate();

            if (mode == Mode.Wobble) GameplayModifiers.I.ActivateModifier(GameplayModifiers.Modifier.SpeedWobble);
            else MelonCoroutines.Start(DoWobble());

            MelonCoroutines.Start(ActiveTimer(defaultParams.duration));
            
        }

        private IEnumerator DoWobble()
        {
            float length = wobbleParams.maxSpeed - wobbleParams.minSpeed;
            float minRange = mode == Mode.Wooble ? 0f : mode == Mode.Wrobl ? -.1f : 0f;
            float maxRange = mode == Mode.Wooble ? .2f : mode == Mode.Wrobl ? .1f : 0f;
            while (defaultParams.active)
            {
                float randomAdd = 0f;
                if(mode == Mode.Wooble || mode == Mode.Wrobl)
                {
                    randomAdd = UnityEngine.Random.Range(minRange, maxRange);
                }
               
                float speed = Mathf.PingPong(Time.time + randomAdd, length) + wobbleParams.minSpeed;
                if (mode == Mode.Womble) speed *= amount;
                AudioDriver.I.SetSpeed(speed);
                yield return new WaitForSecondsRealtime(.01f);
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
            if(mode == Mode.Wobble) GameplayModifiers.I.DeactivateModifier(GameplayModifiers.Modifier.SpeedWobble);
            AudioDriver.I.SetSpeed(1f);
        }

        private enum Mode
        {
            Womble,
            Wobble,
            Wooble,
            Wrobl
        }
    }
}
