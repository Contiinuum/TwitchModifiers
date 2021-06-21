using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;

namespace AudicaModding
{
    public class UnifyColors : Modifier
    {
        public ModifierParams.UnifyColors unifyColorsParams;

        private Color oldColorLeft;
        private Color oldColorRight;

        /*private Color oldLeftHandColor;
        private Color oldRightHandColor;
        */
        public UnifyColors(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.UnifyColors _unifyColorsParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            unifyColorsParams = _unifyColorsParams;
            defaultParams.duration = _unifyColorsParams.duration;
            defaultParams.cooldown = _unifyColorsParams.cooldown;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(ActiveTimer());
            ChangeColors(true);
            Hooks.updateChainColor = true;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            ChangeColors(false);
        }

        public void ChangeColors(bool enable)
        {
            Color leftHandColor;
            Color rightHandColor;
            if (enable)
            {
                oldColorLeft = KataConfig.I.leftHandColor;
                oldColorRight = KataConfig.I.rightHandColor;

                float h1 = UnityEngine.Random.Range(0f, 1f);
                float s = 1f;
                float v = 1f;
                leftHandColor = Color.HSVToRGB(h1, s, v);
                rightHandColor = Color.HSVToRGB(h1, s, v);

            }
            else
            {
                leftHandColor = oldColorLeft;
                rightHandColor = oldColorRight;
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

            Target[] targets = GameObject.FindObjectsOfType<Target>();
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].mCue.behavior != Target.TargetBehavior.Chain) continue;
                if (targets[i].mCue.handType == Target.TargetHandType.Left)
                {
                    targets[i].chainLine.startColor = leftHandColor;
                    targets[i].chainLine.endColor = leftHandColor;
                }
                else
                {
                    targets[i].chainLine.startColor = rightHandColor;
                    targets[i].chainLine.endColor = rightHandColor;
                }

            }
            /*
            foreach (Il2CppSystem.Collections.Generic.KeyValuePair<int, TargetSpawner> spawner in TargetSpawnerManager.I.mSpawners)
            {
                foreach (Il2CppSystem.Collections.Generic.KeyValuePair<int, Il2CppSystem.Collections.Generic.List<Target>> targetPool in spawner.value.mTargetPool)
                {
                    foreach (Target target in targetPool.value)
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
            */
            TargetColorSetter.I.updateColors = true;
            TargetColorSetter.I.UpdateSlowColors(leftHandColor, rightHandColor);
            TargetColorSetter.I.UpdateFastColors(leftHandColor, rightHandColor);
            TargetColorSetter.I.UpdatePreviewBeamColors(leftHandColor, rightHandColor);
            if (MenuState.sState == MenuState.State.Launched) CueDartManager.I.SetUpColors();
        }
    }
}
