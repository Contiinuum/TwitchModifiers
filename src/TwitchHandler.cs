using MelonLoader;
using UnityEngine;
using Harmony;
using System;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using System.Linq;
using Il2CppSystem;


namespace AudicaModding
{
    public class TwitchHandler
    {
        public static System.Collections.Generic.List<string> requestList = new System.Collections.Generic.List<string>();
        public static System.Collections.Generic.List<string> requestQueue = new System.Collections.Generic.List<string>();

        public class ParsedTwitchMessage
        {
            public string badgeInfo = "";
            public string badges = "";
            public string bits = "";
            public string clientNonce = "";
            public string color = "";
            public string displayName = "";
            public string emotes = "";
            public string flags = "";
            public string id = "";
            public string mod = "";
            public string roomId = "";
            public string tmiSentTs = "";
            public string userId = "";
            public string message = "";
            public string user = "";
        }

        public static ParsedTwitchMessage ParseTwitchMessage(string msg)
        {
            ParsedTwitchMessage parsedMsg = new ParsedTwitchMessage();

            string separator = ":";
            string tagSeparator = ";";

            string tags = msg.Split(separator.ToCharArray())[0];

            parsedMsg.user = msg.Split(separator.ToCharArray())[1];
            parsedMsg.message = msg.Split(separator.ToCharArray())[2];

            foreach (string str in tags.Split(tagSeparator.ToCharArray()).ToList())
            {
                if (str.Contains("badge-info="))
                {
                    parsedMsg.badgeInfo = str.Replace("badge-info=", "");
                }
                else if (str.Contains("badges="))
                {
                    parsedMsg.badges = str.Replace("badges=", "");
                }
                else if (str.Contains("bits="))
                {
                    parsedMsg.bits = str.Replace("bits=", "");
                }
                else if (str.Contains("client-nonce="))
                {
                    parsedMsg.clientNonce = str.Replace("client-nonce=", "");
                }
                else if (str.Contains("color="))
                {
                    parsedMsg.color = str.Replace("color=", "");
                }
                else if (str.Contains("display-name="))
                {
                    parsedMsg.displayName = str.Replace("display-name=", "");
                }
                else if (str.Contains("emotes="))
                {
                    parsedMsg.emotes = str.Replace("emotes=", "");
                }
                else if (str.Contains("flags="))
                {
                    parsedMsg.flags = str.Replace("flags=", "");
                }
                else if (str.Substring(0, 3) == "id=")
                {
                    parsedMsg.id = str.Replace("id=", "");
                }
                else if (str.Contains("mod="))
                {
                    parsedMsg.mod = str.Replace("mod=", "");
                }
                else if (str.Contains("room-id="))
                {
                    parsedMsg.roomId = str.Replace("room-id=", "");
                }
                else if (str.Contains("tmi-sent-ts="))
                {
                    parsedMsg.tmiSentTs = str.Replace("tmi-sent-ts=", "");
                }
                else if (str.Contains("user-id="))
                {
                    parsedMsg.userId = str.Replace("user-id=", "");
                }
            }
            return parsedMsg;
        }

        public static void ParseCommand(string msg, string user)
        {
            if (!Config.generalParams.enableTwitchModifiers) return;
            if (msg.Substring(0, 1) == "!")
            {
                string command = msg.Replace("!", "").Split(" ".ToCharArray())[0];
                string arguments = msg.Replace("!" + command + " ", "");
                float amount = ParseAmount(arguments) / 100;
                if (amount < 0 && (command != "zoffset" || command != "shift")) return;
                if (command == "speed")
                {
                    CommandManager.RegisterModifier(ModifierType.Speed, amount, user);
                }
                else if(command == "aa")
                {
                    CommandManager.RegisterModifier(ModifierType.AA, amount, user);
                    MelonLogger.Log("!aa requested by " + user);                                           
                }
                else if(command == "psy")
                {
                    MelonLogger.Log("!psychadelia requested by " + user);
                    CommandManager.RegisterModifier(ModifierType.Psychadelia, amount, user);
                    
                }
                else if(command == "mines")
                {
                    CommandManager.RegisterModifier(ModifierType.Mines, user);
                    MelonLogger.Log("!mines requested by " + user);
                }
                else if(command == "invisguns")
                {
                    CommandManager.RegisterModifier(ModifierType.InvisGuns, user);
                }
                else if (command == "wobble")
                {
                      CommandManager.RegisterModifier(ModifierType.Wobble, user);
                }
                else if (command == "particles")
                {
                    CommandManager.RegisterModifier(ModifierType.Particles, amount, user);
                }
                else if (command == "zoffset")
                {
                    CommandManager.RegisterModifier(ModifierType.ZOffset, amount, user);
                }
            }
        }

        private static float ParseAmount(string msg)
        {
            int amount = -1;
            int.TryParse(msg, out amount);
            return amount;
        }
    }
}
