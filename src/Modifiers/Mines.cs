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
    public class Mines : Modifier
    {
        public ModifierParams.Mines mineParams;
        private TargetSpawner spawner;
        private Vector2 minOffset = new Vector2(-.8f, -0.5f);
        private Vector2 maxOffset = new Vector2(.8f, 0.5f);
        public Mines(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.Mines _mineParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            mineParams = _mineParams;
            defaultParams.duration = _mineParams.duration;
            defaultParams.cooldown = _mineParams.cooldown;
            spawner = TargetSpawnerManager.I.mSpawners[100];
        }

        public override void Activate()
        {
            base.Activate();
            //if (!PlayerPreferences.I.NoFail.mVal) MelonCoroutines.Start(ResetNoFail());
            MelonCoroutines.Start(ActiveTimer());
            MelonCoroutines.Start(SpawnMines());
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public IEnumerator SpawnMines()
        {
            int factor = 960;
            int step = 480;

            while (defaultParams.active)
            {
                if (!InGameUI.I.pauseScreen.IsPaused())
                {
                    float tickStart = AudioDriver.I.mCachedTick;
                    int startVal = ((int)Math.Round((tickStart / (double)factor), MidpointRounding.AwayFromZero) * factor) + 960; //nearest half measure ahead of current time

                    float percentage = UnityEngine.Random.Range(0f, 1f);
                    if (percentage < .25f)
                    {
                        SpawnMine(startVal);

                    }
                    else if (percentage < .5f)
                    {
                        SpawnMine(startVal + step);
                    }
                    else if (percentage < .75f)
                    {
                        SpawnMine(startVal + step * 2);
                    }
                }
                yield return new WaitForSecondsRealtime(1f);
            }
        }

        private void SpawnMine(float tickStart)
        {
            if (tickStart > SongCues.I.GetLastCueStartTick()) return;
            float x = UnityEngine.Random.Range(minOffset.x, maxOffset.x);
            float y = UnityEngine.Random.Range(minOffset.y, maxOffset.y);
            Vector2 offset = new Vector2(x, y);
            SongCues.Cue cue = new SongCues.Cue((int)tickStart, 120, 100, 3, Target.TargetHandType.None, Target.TargetBehavior.Dodge, offset);
            spawner.SpawnCue(cue);
        }
    }
}
