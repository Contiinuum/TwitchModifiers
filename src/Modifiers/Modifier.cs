using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using System.Collections;
using UnityEngine;
using System.Threading;

namespace AudicaModding
{
    public class Modifier
    {
        public ModifierParams.Default defaultParams;
        public float amount;
        public ModifierType type;

        public virtual void Activate()
        {
            MelonLogger.Log(type.ToString() + " activated");
            defaultParams.active = true;
        }

        public virtual void Deactivate()
        {
            MelonLogger.Log(type.ToString() + " deactivated");
            defaultParams.active = false;
            ModifierManager.UnregisterModifier(this);
        }

        protected IEnumerator Timer(float countdownTimer)
        {
            float now = Time.realtimeSinceStartup;
            float want = Time.realtimeSinceStartup + countdownTimer;
            while(now < want)
            {
                if (ModifierManager.stopAllModifiers) yield break;
                now = Time.realtimeSinceStartup;
                Thread.Sleep(16);
                yield return null;
            }
            Deactivate();
            yield return null;
        }

    }
}
