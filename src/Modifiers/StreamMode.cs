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
    public class StreamMode : Modifier
    {
        public ModifierParams.StreamMode unhookChainsParams;

        private SortedDictionary<float, Chain> oldChains = new SortedDictionary<float, Chain>();
        private SortedDictionary<float, Chain> queuedChains = new SortedDictionary<float, Chain>();
        private Dictionary<int, int> speedToTicks = new Dictionary<int, int>();
        public StreamMode(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.StreamMode _streamModeParams, float _amount)
        {
            type = _type;
            defaultParams = _modifierParams;
            unhookChainsParams = _streamModeParams;
            defaultParams.duration = _streamModeParams.duration;
            defaultParams.cooldown = _streamModeParams.cooldown;
            amount = _amount;

            //speedToTicks.Add(64, 30);
            //speedToTicks.Add(48, 40);
            speedToTicks.Add(32, 60);

            speedToTicks.Add(30, 80);
            speedToTicks.Add(28, 80);
            speedToTicks.Add(26, 80);
            speedToTicks.Add(24, 80);

            speedToTicks.Add(22, 120);
            speedToTicks.Add(20, 120);
            speedToTicks.Add(18, 120);
            speedToTicks.Add(16, 120);

            speedToTicks.Add(14, 160);
            speedToTicks.Add(12, 160);

            speedToTicks.Add(10, 240);
            speedToTicks.Add(8, 240);

            speedToTicks.Add(6, 320);

            speedToTicks.Add(4, 480);
           
        }

        public override void Activate()
        {
            base.Activate();
            if (!PlayerPreferences.I.NoFail.mVal) MelonCoroutines.Start(ResetNoFail());
            ModifierManager.invalidateScore = true;
            MelonCoroutines.Start(ActiveTimer());
            ModifyChains(true);
        }


        public override void Deactivate()
        {
            base.Deactivate();
            ModifyChains(false);
        }

        private void ModifyChains(bool unhook)
        {
            SongCues.Cue[] cues = SongCues.I.mCues.cues;
            if (unhook)
            {
                bool checkForChain = false;
                for (int i = 0; i < cues.Length; i++)
                {
                    if (cues[i].behavior == Target.TargetBehavior.Chain) continue;
                    if (cues[i].tick < AudioDriver.I.mCachedTick + 1920) continue;
                    if (checkForChain)
                    {
                        List<SongCues.Cue> nodes = queuedChains.Last().Value.nodes;
                        if (queuedChains.ContainsKey(cues[i].tick))
                        {
                            queuedChains.Remove(queuedChains.Last().Key);
                            continue;
                        }
                        if (nodes[0].tick >= cues[i].tick && nodes[nodes.Count - 1].tick <= cues[i].tick)
                        {
                            queuedChains.Remove(queuedChains.Last().Key);
                            continue;
                        }
                        checkForChain = false;
                    }
                   
                    if (cues[i].behavior != Target.TargetBehavior.ChainStart) continue;
                

                    Target.TargetHandType handType = cues[i].handType;
                    List<SongCues.Cue> chain = new List<SongCues.Cue>();
                    RecursiveAdd(cues[i], chain);
                    queuedChains.Add(cues[i].tick, new Chain(handType, chain));
                    checkForChain = true;
                
                    
                }
                UnhookQueuedChains();
            }
            else
            {
                RepairChains();
            }
           
        }

        private void RecursiveAdd(SongCues.Cue cue, List<SongCues.Cue> chain)
        {
            chain.Add(cue);
            if (cue.chainNext != null) RecursiveAdd(cue.chainNext, chain);
        }

        private void UnhookQueuedChains()
        {
            oldChains = queuedChains;
            foreach (KeyValuePair<float, Chain> chain in queuedChains)
            {
                if (chain.Value.nodes[0].nextCue.tick - chain.Value.nodes[0].tick < speedToTicks[unhookChainsParams.maxStreamSpeed]) continue;
                Target.TargetHandType handType = chain.Value.handType;
                foreach (SongCues.Cue cue in chain.Value.nodes)
                {
                    
                    cue.behavior = Target.TargetBehavior.Standard;
                    cue.handType = handType;
                    if (handType == Target.TargetHandType.Left) handType = Target.TargetHandType.Right;
                    else handType = Target.TargetHandType.Left;
                }
            }
        }
        /*
        private void RecursiveUnhook(SongCues.Cue cue, List<SongCues.Cue> chain, Target.TargetHandType handType)
        {
            chain.Add(cue);
            cue.behavior = Target.TargetBehavior.Standard;
            cue.handType = handType;
            if (handType == Target.TargetHandType.Left) handType = Target.TargetHandType.Right;
            else handType = Target.TargetHandType.Left;
            if (cue.chainNext != null) RecursiveUnhook(cue.chainNext, chain, handType);
        }
        */
        private void RepairChains()
        {        
            foreach(KeyValuePair<float, Chain> entry in oldChains)
            {
                if (entry.Value.nodes[0].tick < AudioDriver.I.mCachedTick + 960) continue;
                bool startSet = false;
                Target.TargetHandType handType = entry.Value.handType;
                foreach (SongCues.Cue cue in entry.Value.nodes)
                {
                    if (startSet)
                    {
                        cue.behavior = Target.TargetBehavior.Chain;
                    }
                    else
                    {
                        cue.behavior = Target.TargetBehavior.ChainStart;
                        startSet = true;
                    }
                    cue.handType = entry.Value.handType;
                }
            }  
        }

        public struct Chain
        {
            public Target.TargetHandType handType;
            public List<SongCues.Cue> nodes;

            public Chain(Target.TargetHandType _handType, List<SongCues.Cue> _nodes)
            {
                handType = _handType;
                nodes = _nodes;
            }
        }
    }
}
