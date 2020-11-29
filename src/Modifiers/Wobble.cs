using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace AudicaModding
{
    public class Wobble : Modifier
    {
        public ModifierParams.Wobble wobbleParams;
        public Wobble(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.Wobble _wobbleParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            wobbleParams = _wobbleParams;
            defaultParams.duration = _wobbleParams.duration;
            defaultParams.cooldown = _wobbleParams.cooldown;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(Timer(defaultParams.duration));
            GameplayModifiers.I.ActivateModifier(GameplayModifiers.Modifier.SpeedWobble);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            GameplayModifiers.I.DeactivateModifier(GameplayModifiers.Modifier.SpeedWobble);
            AudioDriver.I.SetSpeed(1f);
        }
    }
}
