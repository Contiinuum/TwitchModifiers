using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudicaModding
{
    public class ModStatusHandler
    {
        public static void ShowEnabledString()
        {
            if (Integrations.scoreOverlayFound)
            {
                ScoreOverlay.ShowEnabledString();
            }
        }
        public static void RequestStatusDisplays(ModifierType type, string command, string amount, string user, string color)
        {
            StatusTextManager.RequestPopup(type, command, amount);           
            if (Integrations.scoreOverlayFound)
            {
                ScoreOverlay.RequestOverlayDisplay(type, command, amount, user, color);              
            }               
        }

        public static void UpdateStatusDisplays(ModifierType type, string command, string amount, string user, string color, UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.All:
                    StatusTextManager.UpdatePopup(type, command, amount);
                    if (Integrations.scoreOverlayFound)
                    {
                        ScoreOverlay.UpdateOverlay(type, command, amount, user, color, ScoreOverlay.State.Active);                  
                    }
                    break;
                case UpdateType.Ingame:
                    StatusTextManager.UpdatePopup(type, command, amount);
                    break;
                case UpdateType.ScoreOverlay:
                    if (Integrations.scoreOverlayFound)
                    {
                        ScoreOverlay.UpdateOverlay(type, command, amount, user, color, ScoreOverlay.State.Cooldown);                    
                    }
                    break;
            }

        }

        public static void RemoveStatusDisplays(ModifierType type, UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.All:
                    StatusTextManager.RemovePopup(type);
                    if (Integrations.scoreOverlayFound)
                    {
                        ScoreOverlay.RemoveOverlay(type);
                    }
                    break;
                case UpdateType.Ingame:
                    StatusTextManager.RemovePopup(type);
                    break;
                case UpdateType.ScoreOverlay:
                    if (Integrations.scoreOverlayFound)
                    {
                        ScoreOverlay.RemoveOverlay(type);
                    }
                    break;
            }
        }

        public static void RemoveAllDisplays()
        {
            StatusTextManager.DestroyAllPopups();
            if (Integrations.scoreOverlayFound)
            {
                ScoreOverlay.RemoveAllOverlays();
            }
                
        }


        public enum UpdateType
        {
            All,
            ScoreOverlay,
            Ingame
        }
    }
}
