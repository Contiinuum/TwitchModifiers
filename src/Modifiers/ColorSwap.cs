using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;

namespace AudicaModding
{
    public class ColorSwap : Modifier
    {
        public ModifierParams.ColorSwap colorSwitchParams;
        public ColorSwap(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.ColorSwap _colorSwitchParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            colorSwitchParams = _colorSwitchParams;
            defaultParams.duration = _colorSwitchParams.duration;
            defaultParams.cooldown = _colorSwitchParams.cooldown;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(Timer(defaultParams.duration));
            SwapColors();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            SwapColors();

        }

        public void SwapColors()
        {


            Color leftHandColor = KataConfig.I.rightHandColor;
            Color rightHandColor = KataConfig.I.leftHandColor;
            


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
