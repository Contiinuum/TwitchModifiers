using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using TimingAttack;
using UnityEngine;

namespace AudicaModding
{
    public class StutterChains : Modifier
    {
        public ModifierParams.StutterChains stutterChainParams;
        private SortedDictionary<float, int> stutters = new SortedDictionary<float, int>();
        public StutterChains(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.StutterChains _stutterChainParams, float _amount)
        {
            type = _type;
            defaultParams = _modifierParams;
            stutterChainParams = _stutterChainParams;
            defaultParams.duration = _stutterChainParams.duration;
            defaultParams.cooldown = _stutterChainParams.cooldown;
            amount = _amount;
            if (amount == 0) amount = (stutterChainParams.minRot + stutterChainParams.maxRot) / 2;
            else if (amount > stutterChainParams.maxRot) amount = stutterChainParams.maxRot;
            else if (amount < stutterChainParams.minRot) amount = stutterChainParams.minRot;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(ActiveTimer());
            PrepareStutter();
            MelonCoroutines.Start(DoStutter());
        }

        private void PrepareStutter()
        {
            SongCues.Cue[] cues = SongCues.I.mCues.cues;
            float currentTick = AudioDriver.I.mCachedTick;
            for (int i = 0; i < cues.Length; i++)
            {
                if (cues[i].tick < currentTick) continue;
                if (cues[i].behavior != Target.TargetBehavior.ChainStart) continue;
                List<SongCues.Cue> chain = new List<SongCues.Cue>();
                List<int> pitches = new List<int>();              
                RecursiveGetChain(cues[i], chain, pitches);
                int lastPitch = pitches.Last();
                
                int dir = 0;
                if(cues[i].pitch % 12 > lastPitch % 12)
                {
                    dir = -1;
                }
                else if(cues[i].pitch % 12 < lastPitch % 12)
                {
                    dir = 1;
                }
                else
                {
                    continue;
                }
                for (int j = 0; j < chain.Count; j++)
                {
                    if(!stutters.ContainsKey(chain[j].tick)) stutters.Add(chain[j].tick, dir);
                }
            }
        }

        private IEnumerator DoStutter()
        {
            while (defaultParams.active && stutters.Count > 0)
            {
                KeyValuePair<float, int> stutter = stutters.First();
                float neutralDir = 1f;
                if (AudioDriver.I.mCachedTick > stutter.Key)
                {
                    float rot = 0f;
                    if(stutter.Value == 0f)
                    {
                        rot = amount * neutralDir;
                        neutralDir *= -1f;
                    }
                    else
                    {
                        rot = amount * (float)stutter.Value;
                    }
                    
                    AudicaMod.RotateSkybox(rot);
                    stutters.Remove(stutter.Key);
                }
                yield return new WaitForSecondsRealtime(.01f);
            }
            
        }

        private void RecursiveGetChain(SongCues.Cue cue, List<SongCues.Cue> chain, List<int> pitches)
        {
            chain.Add(cue);
            pitches.Add(cue.pitch);
            if (cue.chainNext != null) RecursiveGetChain(cue.chainNext, chain, pitches);
            
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}
