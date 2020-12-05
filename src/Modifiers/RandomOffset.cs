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
    public class RandomOffset : Modifier
    {
        public ModifierParams.RandomOffset randomOffsetParams;
        private Dictionary<float, Vector2> oldOffsets = new Dictionary<float, Vector2>();
        public RandomOffset(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.RandomOffset _randomOffsetParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            randomOffsetParams = _randomOffsetParams;
            defaultParams.duration = _randomOffsetParams.duration;
            defaultParams.cooldown = _randomOffsetParams.cooldown;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(ActiveTimer());
            OffsetTargets(true);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            OffsetTargets(false);
        }

        public void OffsetTargets(bool enable)
        {
            SongCues.Cue[] songCues = SongCues.I.mCues.cues;
            Vector2 offset;
            for (int i = 0; i < songCues.Length; i++)
            {
                if (songCues[i].behavior == Target.TargetBehavior.Melee || songCues[i].behavior == Target.TargetBehavior.Dodge || songCues[i].behavior == Target.TargetBehavior.Chain ||songCues[i].behavior == Target.TargetBehavior.ChainStart) continue;

                if (enable)
                {
                    oldOffsets.Add(songCues[i].tick + songCues[i].pitch, songCues[i].gridOffset);
                    offset = new Vector2(UnityEngine.Random.Range(randomOffsetParams.minOffsetX, randomOffsetParams.maxOffsetX), UnityEngine.Random.Range(randomOffsetParams.minOffsetY, randomOffsetParams.maxOffsetY));
                    songCues[i].gridOffset = offset;
                }
                else
                {
                    if (!oldOffsets.ContainsKey(songCues[i].tick + songCues[i].pitch)) continue;
                    songCues[i].gridOffset = oldOffsets[songCues[i].tick + songCues[i].pitch];
                }
            }
        }
    }
}
