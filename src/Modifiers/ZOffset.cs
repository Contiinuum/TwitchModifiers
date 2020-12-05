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
        private Dictionary<float, float> oldOffsets = new Dictionary<float, float>();
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
            if (amount > zOffsetParams.maxZOffset) amount = zOffsetParams.maxZOffset;
            else if (amount < zOffsetParams.minZOffset) amount = zOffsetParams.minZOffset;
            SetZOffset(amount);
        }

        public override void Deactivate()
        {
            SetZOffset(0f);
            base.Deactivate();
           
        }

        public void SetZOffset(float zOffset)
        {
            SongCues.Cue[] songCues = SongCues.I.mCues.cues;
            float currentTick = AudioDriver.I.mCachedTick;
            float count = 20f;
            float currentCount = 1f;
            for (int i = 0; i < songCues.Length; i++)
            {
                if (songCues[i].tick < currentTick) continue;

                if(songCues[i].behavior != Target.TargetBehavior.Melee && songCues[i].behavior != Target.TargetBehavior.Dodge)
                {
                    //MelonLogger.Log(Mathf.Lerp(0f, zOffset, i / count).ToString());

                    if(direction == Direction.Up)
                    {
                        if(!oldOffsets.ContainsKey(songCues[i].tick + songCues[i].pitch))
                        {
                            oldOffsets.Add(songCues[i].tick + songCues[i].pitch, songCues[i].zOffset);
                        }                      
                        songCues[i].zOffset = Mathf.Lerp(songCues[i].zOffset, zOffset + songCues[i].zOffset, currentCount / count);
                    }

                    else
                    {
                        if (!oldOffsets.ContainsKey(songCues[i].tick + songCues[i].pitch)) continue;
                        songCues[i].zOffset = Mathf.Lerp(songCues[i].zOffset, oldOffsets[songCues[i].tick + songCues[i].pitch], currentCount / count);
                    }
                       
                    currentCount++;
                }                    
            }
            if(direction == Direction.Up) MelonCoroutines.Start(ActiveTimer());
            direction = Direction.Down;
        }

        private enum Direction
        {
            Up,
            Down
        }
    }
}
