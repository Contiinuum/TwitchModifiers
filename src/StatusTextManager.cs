using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AudicaModding
{
    public class StatusTextManager
    {
        private static Vector3 statusTextPosition = new Vector3(0f, -2f, 8f);
        private static Dictionary<ModifierType, PopupSlot> activePopups = new Dictionary<ModifierType, PopupSlot>();
        private static List<int> popupSlots = new List<int>();

        public static void RequestPopup(ModifierType type, string command, string amount)
        {
            if (!Config.generalParams.showModStatus) return;
            if (activePopups.ContainsKey(type)) return;

            if (popupSlots.Count == 0)
            {
                // Config.generalParams.maxActiveModifiers
                for (int i = 0; i < Enum.GetNames(typeof(ModifierType)).Length; i++)
                {
                    popupSlots.Add(i);
                }
            }
            for(int i = 0; i < popupSlots.Count; i++)
            {
                if (popupSlots[i] == -1) continue;
                activePopups.Add(type, new PopupSlot(popupSlots[i], StatusText(ComposeString(command, amount, popupSlots[i]), popupSlots[i])));
                popupSlots[i] = -1;
                break;
            }
        }

        public static void UpdatePopup(ModifierType type, string command, string amount)
        {
            if (!Config.generalParams.showModStatus) return;
            if (!activePopups.ContainsKey(type)) return;
           
            activePopups[type].popup.textMesh.text = ComposeString(command, amount, activePopups[type].slot);
        }

        private static string ComposeString(string command, string amount, int slot)
        {
            string colortag = "<color=#57d32e>";
            if (slot % 2 == 0) colortag = "<color=\"white\">";
            string txt = colortag + command + ": " + amount + "</color>";
            return txt;
        }

        public static void RemovePopup(ModifierType type)
        {
            if (!Config.generalParams.showModStatus) return;
            if (!activePopups.ContainsKey(type)) return;

            DebugTextPopup popup = activePopups[type].popup;          
            popup.Setup("", 1f, false, 100f);
            popupSlots[activePopups[type].slot] = activePopups[type].slot;
            activePopups.Remove(type);
        }

        public static void DestroyAllPopups()
        {
            if (!Config.generalParams.showModStatus) return;
            if (activePopups.Count == 0) return;

            foreach (KeyValuePair<ModifierType, PopupSlot> entry in activePopups)
            {
                entry.Value.popup.Setup("", 1f, false, 100f);
            }
            activePopups.Clear();
            popupSlots = new List<int>();
        }

        private static DebugTextPopup StatusText(string text, int slot)
        {
            Vector3 pos = statusTextPosition;
            pos.y -= .4f * slot;
            return KataConfig.I.CreateDebugText(text, pos, 3f, null, false, 0f);
        }

        private struct PopupSlot
        {
            public int slot;
            public DebugTextPopup popup;

            public PopupSlot(int _slot, DebugTextPopup _popup)
            {
                slot = _slot;
                popup = _popup;
            }
        }
    }
}
