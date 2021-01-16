using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using System.Collections;
using UnityEngine;

namespace AudicaModding
{
    public class Nuke : Modifier
    {
        public ModifierParams.Nuke nukeParams;
        private string user;
        private string color;
        public Nuke(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.Nuke _nukeParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            nukeParams = _nukeParams;
            defaultParams.duration = _nukeParams.duration;
            defaultParams.cooldown = _nukeParams.cooldown;
        }

        public override void Activate()
        {
            MelonCoroutines.Start(ModifierManager.NukeReset(true));
            base.Activate();
            ModifierManager.nukeActive = true;
            MelonCoroutines.Start(Dropnuke());
           
        }

        public IEnumerator Dropnuke()
        {
            float cooldown = .5f;
            user = defaultParams.user;
            color = defaultParams.color;
            yield return new WaitForSecondsRealtime(1.6f);
            MelonCoroutines.Start(ActiveTimer());
            CommandManager.CreateModifier(ModifierType.AA, .3f, user, color);
            yield return new WaitForSecondsRealtime(cooldown);
            CommandManager.CreateModifier(ModifierType.BetterMelees, 0, user, color);
            yield return new WaitForSecondsRealtime(cooldown);
            CommandManager.CreateModifier(ModifierType.InvisGuns, 0, user, color);
            yield return new WaitForSecondsRealtime(cooldown);
            CommandManager.CreateModifier(ModifierType.Particles, 500, user, color);
            yield return new WaitForSecondsRealtime(cooldown);
            CommandManager.CreateModifier(ModifierType.Psychedelia, 500, user, color);
            yield return new WaitForSecondsRealtime(cooldown);
            CommandManager.CreateModifier(ModifierType.RandomColors, 0, user, color);
            yield return new WaitForSecondsRealtime(cooldown);
            CommandManager.CreateModifier(ModifierType.Speed, 1.2f, user, color);         
            if (Config.generalParams.allowScoreDisablingMods)
            {
                yield return new WaitForSecondsRealtime(cooldown);
                CommandManager.CreateModifier(ModifierType.StreamMode, 0, user, color);
            }
            yield return new WaitForSecondsRealtime(cooldown);
            if (Integrations.timingAttackFound) CommandManager.CreateModifier(ModifierType.TimingAttack, 0, user, color);
            else CommandManager.CreateModifier(ModifierType.HiddenTelegraphs, 0, user, color);        
            yield return new WaitForSecondsRealtime(cooldown);
            CommandManager.CreateModifier(ModifierType.ZOffset, .1f, user, color);
            yield return null;
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}
