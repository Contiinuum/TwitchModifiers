using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;

namespace AudicaModding
{
    public class RandomColors : Modifier
    {
        public ModifierParams.RandomColors randomColorParams;
        private Color leftHandColor;
        private Color rightHandColor;

        private Color oldLeftHandColor;
        private Color oldRightHandColor;
        public RandomColors(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.RandomColors _randomColorParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            randomColorParams = _randomColorParams;
            defaultParams.duration = _randomColorParams.duration;
            defaultParams.cooldown = _randomColorParams.cooldown;
        }

        public override void Activate()
        {
            ModifierManager.randomColorsActive = true;
            base.Activate();      
            MelonCoroutines.Start(ActiveTimer(defaultParams.duration));
            ChangeColors(true);
        }

        public override void Deactivate()
        {
            ModifierManager.randomColorsActive = false;
            base.Deactivate();            
            ChangeColors(false);          
        }

        public void ChangeColors(bool enable)
        {
            if (enable)
            {
                oldLeftHandColor = KataConfig.I.leftHandColor;
                oldRightHandColor = KataConfig.I.rightHandColor;

                float h1 = UnityEngine.Random.Range(0f, .49f);
                float h2 = UnityEngine.Random.Range(.5f, 1f);
                float s = 1f;
                float v = 1f;
                if(h2 > .75f)
                {
                    leftHandColor = Color.HSVToRGB(h1, s, v);
                    rightHandColor = Color.HSVToRGB(h2, s, v);
                }
                else
                {
                    leftHandColor = Color.HSVToRGB(h2, s, v);
                    rightHandColor = Color.HSVToRGB(h1, s, v);
                }
               
            }
            else
            {
                leftHandColor = oldLeftHandColor;
                rightHandColor = oldRightHandColor;
            }
            

            PlayerPreferences.I.GunColorLeft.Set(leftHandColor);
            PlayerPreferences.I.GunColorRight.Set(rightHandColor);
            KataConfig.I.leftHandColor = leftHandColor;
            KataConfig.I.rightHandColor = rightHandColor;
            for (int i = 0; i < PlayerPreferences.I.mColorPrefs.Count; i++)
            {
                if (PlayerPreferences.I.mColorPrefs[i].mName == "gun_color_left")
                {
                    PlayerPreferences.I.mColorPrefs[i].mVal = leftHandColor;
                }
                else if (PlayerPreferences.I.mColorPrefs[i].mName == "gun_color_right")
                {
                    PlayerPreferences.I.mColorPrefs[i].mVal = rightHandColor;
                }
            }


            foreach(Il2CppSystem.Collections.Generic.KeyValuePair<int, TargetSpawner> spawner in TargetSpawnerManager.I.mSpawners)
            {
                foreach(Il2CppSystem.Collections.Generic.KeyValuePair<int, Il2CppSystem.Collections.Generic.List<Target>> targets in spawner.value.mTargetPool)
                {
                    foreach(Target target in targets.value)
                    {
                        Color rightColor = PlayerPreferences.I.GunColorRight.Get() / 2;
                        Color leftColor = PlayerPreferences.I.GunColorLeft.Get() / 2;
                        if (target.mCue.handType == Target.TargetHandType.Right)
                        {
                            target.chainLine.startColor = rightColor;
                            target.chainLine.endColor = rightColor;
                        }
                        else if (target.mCue.handType == Target.TargetHandType.Left)
                        {
                            target.chainLine.startColor = leftColor;
                            target.chainLine.endColor = leftColor;
                        }
                    }
                }               
            }
    
            TargetColorSetter.I.updateColors = true;
            TargetColorSetter.I.UpdateSlowColors(leftHandColor, rightHandColor);
            TargetColorSetter.I.UpdateFastColors(leftHandColor, rightHandColor);
            TargetColorSetter.I.UpdatePreviewBeamColors(leftHandColor, rightHandColor);
            if (MenuState.sState == MenuState.State.Launched) CueDartManager.I.SetUpColors();          
        }
    }
}
