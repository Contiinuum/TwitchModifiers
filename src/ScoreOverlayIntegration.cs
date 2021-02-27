using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using ScoreOverlay;

namespace AudicaModding
{
    public class ScoreOverlay
    {
        //public static bool scoreOverlayFound = false;
        private static bool addNewLine = false;
        private static bool spacingSet = false;
        private static Dictionary<ModifierType, string> overlays = new Dictionary<ModifierType, string>();
        private static string overlayText;
        private static string enabledText = "TWITCH MODIFIERS ENABLED";
        private static string channelPointText = "\n<color=\"red\">Requires Channel Points</color>";
        
        public static void RequestOverlayDisplay(ModifierType type, string command, string amount, string user, string color)
        {
            if (!Config.generalParams.showOnScoreOverlay) return;
            if (overlays.ContainsKey(type)) return;

            if (ScoreOverlayMod.ui.ModifierText.text.Length > 0)
            {
                addNewLine = true;
            }
            else
            {
                addNewLine = false;
            }
            if (!spacingSet)
            {
                spacingSet = true;
                ScoreOverlayMod.ui.ModifierText.lineSpacing = -1f;
            }
            overlays.Add(type, ComposeString(command, amount, user, color, State.Active));
            UpdateOverlayString();
        }

        public static void ShowEnabledString()
        {
            if (!Config.generalParams.enableTwitchModifiers || !Config.generalParams.showOnScoreOverlay) return;
            if (overlays.Count > 0) return;
            string space = ScoreOverlayMod.ui.ModifierText.text.Length > 0 ? "\n" : "";
            string txt = ScoreOverlayMod.ui.ModifierText.text + space + enabledText;
            if (Config.generalParams.useChannelPoints) txt += channelPointText;
            ScoreOverlayMod.ui.ModifierText.SetText(txt);
        }

        private static void UpdateOverlayString()
        {
            overlayText = "";
            if (addNewLine) overlayText += "\n";
            foreach(KeyValuePair<ModifierType, string> entry in overlays)
            {
                overlayText += entry.Value + "\n";
            }
            ScoreOverlayMod.ui.ModifierText.SetText(ScoreOverlayMod.ui.ModifierText.text + overlayText);
        }

        private static string ComposeString(string command, string amount, string user, string color, State state)
        {
            string statusColor = state == State.Active ? "<color=\"green\">" : "<color=\"red\">";
            color = color.Replace("\"", string.Empty); 
            string s = statusColor;
            s += command + "</color> ";
            if (color.Length > 0)
            {
                s += "<" + color + ">(" + user + ")</color>";
            }
            else
            {
                s += "(" + user + ")";
            }
            
            s += statusColor;
            s += state == State.Active ? ": " : " CD: ";
            s += amount;
            s += "</color>";
            return s;
        }
           

        public static void UpdateOverlay(ModifierType type, string command, string amount, string user, string color, State state)
        {
            if (!Config.generalParams.showOnScoreOverlay) return;
            if (!overlays.ContainsKey(type)) return;

            overlays[type] = ComposeString(command, amount, user, color, state);
            UpdateOverlayString();
        }

        public static void RemoveOverlay(ModifierType type)
        {
            if (!Config.generalParams.showOnScoreOverlay) return;
            if (!overlays.ContainsKey(type)) return;

            overlays.Remove(type);
            UpdateOverlayString();
            if (overlays.Count == 0) ShowEnabledString();
        }

        public static void RemoveAllOverlays()
        {
            if (!Config.generalParams.showOnScoreOverlay) return;

            overlays.Clear();
            ScoreOverlayMod.ui.ModifierText.SetText("");
            //UpdateOverlayString();
            ShowEnabledString();
        }

        public enum State
        {
            Active,
            Cooldown
        }
    }
}
