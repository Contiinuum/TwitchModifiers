using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using System.Threading;

namespace AudicaModding
{
    public class Psychadelia : Modifier
    {
        public ModifierParams.Psychedelia psychadeliaParams;

        private static float defaultPsychadeliaPhaseSeconds = 14.28f;
        private float psychadeliaTimer = 0.0f;
        public Psychadelia(ModifierType _type, ModifierParams.Default _modifierParams, ModifierParams.Psychedelia _psychadeliaParams, float _amount)
        {
            type = _type;
            defaultParams = _modifierParams;
            psychadeliaParams = _psychadeliaParams;
            defaultParams.duration = _psychadeliaParams.duration;
            defaultParams.cooldown = _psychadeliaParams.cooldown;
            amount = _amount;
        }

        public override void Activate()
        {
            base.Activate();
            MelonCoroutines.Start(Timer(defaultParams.duration));
            if (amount > psychadeliaParams.maxPsychadeliaSpeed) amount = psychadeliaParams.maxPsychadeliaSpeed;
            else if (amount < psychadeliaParams.minPsychadeliaSpeed) amount = psychadeliaParams.minPsychadeliaSpeed;      
            MelonCoroutines.Start(DoPsychedelia());
        }

        public override void Deactivate()
        {
            base.Deactivate();
            GameplayModifiers.I.mPsychedeliaPhase = 0.000001f;     
        }

        private IEnumerator DoPsychedelia()
        {
            while (defaultParams.active)
            {
                float phaseTime = defaultPsychadeliaPhaseSeconds / amount;

                if (psychadeliaTimer <= phaseTime)
                {
                    psychadeliaTimer += Time.deltaTime;

                    float forcedPsychedeliaPhase = psychadeliaTimer / phaseTime;
                    GameplayModifiers.I.mPsychedeliaPhase = forcedPsychedeliaPhase;
                }
                else
                {
                    psychadeliaTimer = 0;
                }
                Thread.Sleep(16);
                yield return null;
            }           
        }
    }
}
