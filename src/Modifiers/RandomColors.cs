using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AudicaModding
{
    public class RandomColors : Modifier
    {
        public ModifierParams.RandomColors randomColorParams;
        public Color leftHandColor;
        private Color rightHandColor;
        public RandomColors(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.RandomColors _randomColorParams)
        {
            type = _type;
            defaultParams = _modifierParams;
            randomColorParams = _randomColorParams;
            defaultParams.duration = _randomColorParams.duration;
            defaultParams.cooldown = _randomColorParams.cooldown;
        }

        public void RandomizeColor()
        {
            leftHandColor = new Color(UnityEngine.Random.Range(0, 255), UnityEngine.Random.Range(0, 255), UnityEngine.Random.Range(0, 255));
            rightHandColor = new Color(UnityEngine.Random.Range(0, 255), UnityEngine.Random.Range(0, 255), UnityEngine.Random.Range(0, 255));
        }

        public void ChangeColor(Target target)
        {
            Telegraph tele = target.mTelegraph;
            TintMaterial(target.mMeshRenderer.material, target.GetHand() == Target.TargetHandType.Left ? leftHandColor : rightHandColor);
            TintMaterial(target.timingIndicator.material, target.GetHand() == Target.TargetHandType.Left ? leftHandColor : rightHandColor);
            TintMaterial(target.mFakeBloomMeshRenderer.material, target.GetHand() == Target.TargetHandType.Left ? leftHandColor : rightHandColor);
            TintMaterial(tele.mFakeBloomMeshRenderer.material, target.GetHand() == Target.TargetHandType.Left ? leftHandColor : rightHandColor);
            TintMaterial(tele.cloud.material, target.GetHand() == Target.TargetHandType.Left ? leftHandColor : rightHandColor);
            //tele.questCloudMaterial; nullrefs, at least on PC
        }

        public void ChangeColor(Material mat, Target.TargetHandType handType)
        {
            TintMaterial(mat, handType == Target.TargetHandType.Left ? leftHandColor : rightHandColor);
        }
        private void TintMaterial(Material mat, Color color)
        {
            mat.SetColor("Tint", color);
            mat.SetColor("Color", color);
            mat.SetColor("Tint Color", color);
            mat.SetColor("Main Color", color);
        }
    }
}
