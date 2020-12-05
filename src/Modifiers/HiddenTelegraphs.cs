using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace AudicaModding
{
    public class HiddenTelegraphs : Modifier
    {
        public ModifierParams.HiddenTelegraphs hiddenTelegraphParams;
        public HiddenTelegraphs(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.HiddenTelegraphs _hiddenTelegraphParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            hiddenTelegraphParams = _hiddenTelegraphParams;
            defaultParams.duration = _hiddenTelegraphParams.duration;
            defaultParams.cooldown = _hiddenTelegraphParams.cooldown;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(ActiveTimer());
            Hooks.hideTeles = true;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Hooks.hideTeles = false;
        }
    }
}
