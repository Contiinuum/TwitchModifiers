﻿using System;
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
            base.Activate();
            MelonCoroutines.Start(Timer(defaultParams.duration));
            ChangeColors(true);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            ChangeColors(false);

        }

        public void ChangeColors(bool enable)
        {
            if (enable)
            {
                oldLeftHandColor = KataConfig.I.leftHandColor;
                oldRightHandColor = KataConfig.I.rightHandColor;

                leftHandColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
                rightHandColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
            }
            else
            {
                leftHandColor = oldLeftHandColor;
                rightHandColor = oldRightHandColor;
            }
            

            PlayerPreferences.I.GunColorLeft.Set(leftHandColor);
            PlayerPreferences.I.GunColorRight.Set(rightHandColor);
            Gun[] guns = GameObject.FindObjectsOfType<Gun>();
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
            for (int i = 0; i < guns.Length; i++)
            {
                guns[i].UpdateGunModel();
                guns[i].UpdateColor(leftHandColor, rightHandColor);
                guns[i].UpdateBeamTextures();
            }
            TargetColorSetter.I.updateColors = true;
            TargetColorSetter.I.UpdateSlowColors(leftHandColor, rightHandColor);
            TargetColorSetter.I.UpdateFastColors(leftHandColor, rightHandColor);
            TargetColorSetter.I.UpdatePreviewBeamColors(leftHandColor, rightHandColor);
            CueDartManager.I.SetUpColors();          
        }
    }
}
