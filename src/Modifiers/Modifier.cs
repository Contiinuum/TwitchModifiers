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
            ModStatusHandler.RequestStatusDisplays(type, defaultParams.name,  defaultParams.duration.ToString(), defaultParams.user, defaultParams.color);          
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
            ModStatusHandler.RemoveStatusDisplays(type, ModStatusHandler.UpdateType.Ingame);
            ModStatusHandler.UpdateStatusDisplays(type, defaultParams.name, defaultParams.cooldown.ToString(), defaultParams.user, defaultParams.color, ModStatusHandler.UpdateType.ScoreOverlay);         
            MelonCoroutines.Start(CooldownTimer(defaultParams.cooldown));
            if (ModifierManager.nukeActive)
            {
                foreach (Modifier mod in ModifierManager.activeModifiers)
                {
                    if (mod.defaultParams.active) return;
                }
                ModifierManager.nukeActive = false;
            }
        }

        protected IEnumerator ActiveTimer()
        {
            while(defaultParams.duration > 0)
            {
                if (ModifierManager.stopAllModifiers) yield break;
                ModStatusHandler.UpdateStatusDisplays(type, defaultParams.name, defaultParams.duration.ToString(), defaultParams.user, defaultParams.color, ModStatusHandler.UpdateType.All);
                if (!InGameUI.I.pauseScreen.IsPaused()) defaultParams.duration--;
                yield return new WaitForSecondsRealtime(1f);
            }
            Deactivate();
            yield return null;
        }

        protected IEnumerator CooldownTimer(float cooldownTimer)
        {
            while (cooldownTimer > 0)
            {
                ModStatusHandler.UpdateStatusDisplays(type, defaultParams.name, cooldownTimer.ToString(), defaultParams.user, defaultParams.color, ModStatusHandler.UpdateType.ScoreOverlay);
                if (ModifierManager.stopAllModifiers) yield break;
                if (!InGameUI.I.pauseScreen.IsPaused()) cooldownTimer--;
                yield return new WaitForSecondsRealtime(1f);
            }
            ModStatusHandler.RemoveStatusDisplays(type, ModStatusHandler.UpdateType.ScoreOverlay);
            ModifierManager.RemoveActiveModifier(this);
        }

        protected IEnumerator ToggleNoFail()
        {
            if (PlayerPreferences.I.NoFail.mVal) yield break;
            ModifierManager.invalidateScore = true;
            PlayerPreferences.I.NoFail.mVal = true;
            yield return new WaitForSecondsRealtime(.2f);
            PlayerPreferences.I.NoFail.mVal = false;
            yield return null;
        }

    }
}
