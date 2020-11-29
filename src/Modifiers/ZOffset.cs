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
    public class ZOffset : Modifier
    {
        public ModifierParams.ZOffset zOffsetParams;
        private Direction direction = Direction.Up;
        public ZOffset(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.ZOffset _zOffsetParams, float _amount)
        {
            type = _type;
            defaultParams = _modifierParams;
            zOffsetParams = _zOffsetParams;
            defaultParams.duration = _zOffsetParams.duration;
            defaultParams.cooldown = _zOffsetParams.cooldown;
            amount = _amount;
        }

        public override void Activate()
        {
            base.Activate();
            SetZOffset(amount);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            SetZOffset(0f);
        }

        public void SetZOffset(float zOffset)
        {
            SongCues.Cue[] songCues = SongCues.I.mCues.cues;
            int count = 10;
            for (int i = 0; i < songCues.Length; i++)
            {
                if(songCues[i].behavior != Target.TargetBehavior.Melee && songCues[i].behavior != Target.TargetBehavior.Dodge)
                {
                    if(direction == Direction.Up) songCues[i].zOffset = Mathf.Lerp(0f, zOffset, (i+1) / count);
                    else songCues[i].zOffset = Mathf.Lerp(zOffset, 0f, (i + 1) / count);
                }
                i++;
                    
            }
            if(direction == Direction.Up) MelonCoroutines.Start(Timer(defaultParams.duration));
            direction = Direction.Down;
        }

        private enum Direction
        {
            Up,
            Down
        }
    }
}
