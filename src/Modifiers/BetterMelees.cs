using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;

namespace AudicaModding
{
    public class BetterMelees : Modifier
    {
        public ModifierParams.BetterMelees betterMeleesParams;

        private Dictionary<float, BehaviorType> oldBehavior = new Dictionary<float, BehaviorType>();
        public BetterMelees(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.BetterMelees _betterMeleesParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            betterMeleesParams = _betterMeleesParams;
            defaultParams.duration = _betterMeleesParams.duration;
            defaultParams.cooldown = _betterMeleesParams.cooldown;
        }

        public override void Activate()
        {
            base.Activate();
            //if (!PlayerPreferences.I.NoFail.mVal) MelonCoroutines.Start(ResetNoFail());
            ModifierManager.invalidateScore = true;
            MelonCoroutines.Start(ActiveTimer());
            MeleesToMines(true);
        }
        

        public override void Deactivate()
        {
            base.Deactivate();
            MeleesToMines(false);
        }

        private void MeleesToMines(bool enable)
        {
            SongCues.Cue[] cues = SongCues.I.mCues.cues;

            for(int i = 0; i < cues.Length; i++)
            {
                if (cues[i].behavior == Target.TargetBehavior.Melee || cues[i].behavior == Target.TargetBehavior.Dodge)
                {
                    if (enable)
                    {
                        if (oldBehavior.ContainsKey(cues[i].tick + cues[i].pitch)) continue;
                        oldBehavior.Add(cues[i].tick + cues[i].pitch, new BehaviorType(cues[i].behavior, cues[i].handType));
                        cues[i].behavior = Target.TargetBehavior.Dodge;
                        cues[i].handType = Target.TargetHandType.None;
                    }
                    else
                    {
                        if (!oldBehavior.ContainsKey(cues[i].tick + cues[i].pitch)) continue;
                        if (oldBehavior[cues[i].tick + cues[i].pitch].behavior == Target.TargetBehavior.Melee)
                        {
                            cues[i].behavior = oldBehavior[cues[i].tick + cues[i].pitch].behavior;
                            cues[i].handType = oldBehavior[cues[i].tick + cues[i].pitch].handType;
                        }
                          
                    }
                }
               
            }
        }

        private struct BehaviorType
        {
            public Target.TargetBehavior behavior;
            public Target.TargetHandType handType;

            public BehaviorType(Target.TargetBehavior _behavior, Target.TargetHandType _handType)
            {
                behavior = _behavior;
                handType = _handType;
            }
        }
    }
}
