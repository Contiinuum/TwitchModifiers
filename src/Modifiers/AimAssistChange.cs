using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace AudicaModding
{
    public class AimAssistChange : Modifier
    {
        public ModifierParams.AimAssist aimAssistParams { get; }
        public AimAssistChange(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.AimAssist _aimAssistParams, float _amount)
        {
            type = _type;
            defaultParams = _modifierParams;
            aimAssistParams = _aimAssistParams;
            defaultParams.duration = _aimAssistParams.duration;
            defaultParams.cooldown = _aimAssistParams.cooldown;
            amount = _amount;
        }

        public override void Activate()
        {           
            base.Activate();
            if (amount < aimAssistParams.minAimAssist) amount = aimAssistParams.minAimAssist;
            PlayerPreferences.I.AimAssistAmount.mVal = amount;
            MelonCoroutines.Start(Timer(defaultParams.duration));         
        }

        public override void Deactivate()
        {
            base.Deactivate();
            PlayerPreferences.I.AimAssistAmount.mVal = 1f;
        }
    }
}
