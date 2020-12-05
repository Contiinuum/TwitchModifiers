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
    public class Scale : Modifier
    {
        public ModifierParams.Scale scaleParams;
        private Dictionary<float, Vector2> oldOffsets = new Dictionary<float, Vector2>();
        public Scale(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.Scale _scaleParams, float _amount)
        {
            type = _type;
            defaultParams = _modifierParams;
            scaleParams = _scaleParams;
            defaultParams.duration = _scaleParams.duration;
            defaultParams.cooldown = _scaleParams.cooldown;
            amount = _amount;
        }

        public override void Activate()
        {
            base.Activate();
            if (amount > scaleParams.maxScale) amount = scaleParams.maxScale;
            else if (amount < scaleParams.minScale) amount = scaleParams.minScale;
            MelonCoroutines.Start(ActiveTimer());
            ScaleTargets(amount, true);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            ScaleTargets(0f, false);
        }
      
        public void ScaleTargets(float scale, bool enable)
        {
            SongCues.Cue[] songCues = SongCues.I.mCues.cues;
            float newScale = scale * .5f;
           
            for (int i = 0; i < songCues.Length; i++)
            {
                if (songCues[i].behavior == Target.TargetBehavior.Melee || songCues[i].behavior == Target.TargetBehavior.Dodge || songCues[i].behavior == Target.TargetBehavior.Chain || songCues[i].behavior == Target.TargetBehavior.ChainStart) continue;
                
                if (enable)
                {
                    Vector2 offset = songCues[i].gridOffset;
                    if (songCues[i].gridOffset.magnitude == 0)
                    {
                        if(songCues[i].pitch % 12 < 6)
                        {
                            offset.x -= newScale;
                        }
                        else
                        {
                            offset.x += newScale;
                        }

                        if(Math.Floor((float)(songCues[i].pitch / 12)) < 3)
                        {
                            offset.y -= newScale * .5f;
                        }
                        else
                        {
                            offset.y += newScale * .5f;
                        }
                    }
                   
                    oldOffsets.Add(songCues[i].tick + songCues[i].pitch, songCues[i].gridOffset);
                    offset.x *= newScale;
                    offset.y *= newScale;
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
