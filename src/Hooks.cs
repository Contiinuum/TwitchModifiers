using Harmony;
using System;
using TwitchChatter;
using MelonLoader;
using System.Linq;
using UnityEngine;

namespace AudicaModding
{
    internal static class Hooks
    {
        [HarmonyPatch(typeof(TwitchChatStream), "write_chat_msg", new Type[] { typeof(string) })]
        private static class PatchWriteChatMsg
        {
            private static void Prefix(string msg)
            {
                if (msg.Length > 1)
                {
                    if (msg.Substring(0, 1) == "@")
                    {
                        if (msg.Contains("tmi.twitch.tv PRIVMSG "))
                        {
                            TwitchHandler.ParsedTwitchMessage parsedMsg = TwitchHandler.ParseTwitchMessage(msg);
                            TwitchHandler.ParseCommand(parsedMsg.message, parsedMsg.displayName);
                        }
                    }
                }
            }
        }     
        
        [HarmonyPatch(typeof(MenuState), "SetState", new Type[] { typeof(MenuState.State) })]
        private static class PatchSetMenuState
        {
            private static void Postfix(MenuState __instance, ref MenuState.State state)
            {
                if (state == MenuState.State.Launched) MelonCoroutines.Start(ModifierManager.Reset());
            }
        }
        
        [HarmonyPatch(typeof(InGameUI), "Restart")]
        private static class PatchRestart
        {
            private static void Prefix(InGameUI __instance)
            {
                MelonCoroutines.Start(ModifierManager.Reset());
            }
           
        }      
        
    }
}