using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace AudicaModding
{
    public class InvisibleGuns : Modifier
    {
        public ModifierParams.InvisGuns invisGunsParams;
        public InvisibleGuns(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.InvisGuns _invisGunsParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            invisGunsParams = _invisGunsParams;
            defaultParams.duration = _invisGunsParams.duration;
            defaultParams.cooldown = _invisGunsParams.cooldown;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(ActiveTimer());
            GameplayModifiers.I.ActivateModifier(GameplayModifiers.Modifier.InvisibleGuns);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            GameplayModifiers.I.DeactivateModifier(GameplayModifiers.Modifier.InvisibleGuns);
        }
    }
}
