using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudicaModding
{
    public class ModStatusHandler
    {
        public static void RequestStatusDisplays(ModifierType type, string text)
        {
            StatusTextManager.RequestPopup(type, text);           
            if (ScoreOverlayIntegration.scoreOverlayFound)
            {
                ScoreOverlayIntegration.RequestOverlayDisplay(type, "<color=\"green\">" + text);              
            }               
        }

        public static void UpdateStatusDisplays(ModifierType type, string text, UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.All:
                    StatusTextManager.UpdatePopup(type, text);
                    if (ScoreOverlayIntegration.scoreOverlayFound)
                    {
                        ScoreOverlayIntegration.UpdateOverlay(type, "<color=\"green\">" + text);                  
                    }
                    break;
                case UpdateType.Ingame:
                    StatusTextManager.UpdatePopup(type, text);
                    break;
                case UpdateType.ScoreOverlay:
                    if (ScoreOverlayIntegration.scoreOverlayFound)
                    {
                        ScoreOverlayIntegration.UpdateOverlay(type, "<color=\"red\">" + text);                    
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
                    if (ScoreOverlayIntegration.scoreOverlayFound)
                    {
                        ScoreOverlayIntegration.RemoveOverlay(type);
                    }
                    break;
                case UpdateType.Ingame:
                    StatusTextManager.RemovePopup(type);
                    break;
                case UpdateType.ScoreOverlay:
                    if (ScoreOverlayIntegration.scoreOverlayFound)
                    {
                        ScoreOverlayIntegration.RemoveOverlay(type);
                    }
                    break;
            }
        }

        public static void RemoveAllDisplays()
        {
            StatusTextManager.DestroyAllPopups();
            if (ScoreOverlayIntegration.scoreOverlayFound)
            {
                ScoreOverlayIntegration.RemoveAllOverlays();
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
