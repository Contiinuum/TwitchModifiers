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
            StatusTextManager.RequestPopup(type, type.ToString() + ": " + defaultParams.duration.ToString());
        }

        public virtual void Deactivate()
        {
            MelonLogger.Log(type.ToString() + " deactivated");
            defaultParams.active = false;
            StatusTextManager.DestroyPopup(type);
            ModifierManager.UnregisterModifier(this);
        }

        protected IEnumerator Timer(float countdownTimer)
        {
            while(countdownTimer > 0)
            {              
                if (ModifierManager.stopAllModifiers) yield break;
                StatusTextManager.UpdatePopup(type, type.ToString() + ": " + countdownTimer.ToString());
                if(!InGameUI.I.pauseScreen.IsPaused()) countdownTimer--;
                yield return new WaitForSecondsRealtime(1f);
            }
            Deactivate();
            yield return null;
        }

        protected IEnumerator ResetNoFail()
        {
            PlayerPreferences.I.NoFail.mVal = true;
            yield return new WaitForSecondsRealtime(.2f);
            PlayerPreferences.I.NoFail.mVal = false;
            yield return null;
        }

    }
}
