using MelonLoader;
using System.Collections;
using UnityEngine;

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
            ModStatusHandler.RequestStatusDisplays(type, type.ToString() + ": " + defaultParams.duration.ToString());          
        }

        public virtual void Deactivate()
        {
            if (!defaultParams.active)
            {
                MelonLogger.Log(type.ToString() + " cancelled");
                return;
            }
            MelonLogger.Log(type.ToString() + " deactivated");
            defaultParams.active = false;
            //ModifierManager.UnregisterModifier(this);
            ModStatusHandler.RemoveStatusDisplays(type, ModStatusHandler.UpdateType.Ingame);
            ModStatusHandler.UpdateStatusDisplays(type, type.ToString() + " CD: " + defaultParams.cooldown.ToString(), ModStatusHandler.UpdateType.ScoreOverlay);
            MelonCoroutines.Start(CooldownTimer(defaultParams.cooldown));
        }

        protected IEnumerator ActiveTimer(float countdownTimer)
        {
            while(countdownTimer > 0)
            {
                if (ModifierManager.stopAllModifiers) yield break;
                ModStatusHandler.UpdateStatusDisplays(type, type.ToString() + ": " + countdownTimer.ToString(), ModStatusHandler.UpdateType.All);
                if (!InGameUI.I.pauseScreen.IsPaused()) countdownTimer--;
                yield return new WaitForSecondsRealtime(1f);
            }
            Deactivate();
            yield return null;
        }

        protected IEnumerator CooldownTimer(float cooldownTimer)
        {
            while(cooldownTimer > 0)
            {
                //remainingCooldown = cooldownTimer;
                ModStatusHandler.UpdateStatusDisplays(type, type.ToString() + " CD: " + cooldownTimer.ToString(), ModStatusHandler.UpdateType.ScoreOverlay);
                if (ModifierManager.stopAllModifiers) yield break;
                if (!InGameUI.I.pauseScreen.IsPaused()) cooldownTimer--;
                yield return new WaitForSecondsRealtime(1f);
            }
            ModStatusHandler.RemoveStatusDisplays(type, ModStatusHandler.UpdateType.ScoreOverlay);
            //CooldownManager.RemoveFromList(this);
            ModifierManager.RemoveActiveModifier(this);
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
