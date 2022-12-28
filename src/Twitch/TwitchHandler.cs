using MelonLoader;
using System.Linq;


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
            public string customRewardId = "";
            public string clientNonce = "";
            public string color = "";
            public string displayName = "";
            public string emotes = "";
            public string flags = "";
            public string id = "";
            public string mod = "";
            public string broadcaster = "";
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
            //string color = msg.Split(tagSeparator.ToCharArray())[3];
            
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
                else if (str.Contains("custom-reward-id="))
                {
                    parsedMsg.customRewardId = str.Replace("custom-reward-id=", "");
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
            if (parsedMsg.badges.Contains("broadcaster"))
            {
                parsedMsg.broadcaster = "1";
            }
            //parsedMsg.color = color;
            return parsedMsg;
        }

        public static void ParseCommand(ParsedTwitchMessage message)
        {
            string msg = message.message;
            string user = message.displayName;
            string color = message.color;
            string customRewardId = message.customRewardId;
            if (msg.Substring(0, 1) == "!")
            {
                string command = msg.Replace("!", "").Split(" ".ToCharArray())[0];
                string arguments = msg.Replace("!" + command + " ", "");
                if (color.Length == 0) color = "\"white\"";
                float amount = ParseAmount(arguments) / 100;

                if (!Config.generalParams.enableTwitchModifiers)
                {
                    if (command == "twitchmodson" && (message.mod == "1" || message.broadcaster == "1"))
                    {
                        Config.EnableAll(true);
                    }
                    return;
                }
                if (command == "twitchmodsoff" && (message.mod == "1" || message.broadcaster == "1"))
                {
                    Config.EnableAll(false);
                    return;
                }

                if (customRewardId.Length == 0 && Config.generalParams.useChannelPoints)
                {
                    MelonLogger.Msg("No channel points redeemed.");
                    return;
                }

                if (amount < 0 && command != "zoffset") return;
                if(amount > 0 || (amount < 0 && command == "zoffset"))
                {
                    if (command == "speed")
                    {
                        if (amount == 1f) return;
                        CommandManager.RegisterModifier(ModifierType.Speed, amount, user, color);
                    }
                    else if (command == "aa")
                    {
                        CommandManager.RegisterModifier(ModifierType.AA, amount, user, color);
                    }
                    else if (command == "psy")
                    {
                        CommandManager.RegisterModifier(ModifierType.Psychedelia, amount, user, color);
                    }
                    else if (command == "womble")
                    {
                        if (amount == 1f) return;
                        CommandManager.RegisterModifier(ModifierType.Wobble, amount, user, color);
                    }
                    else if (command == "particles")
                    {
                        CommandManager.RegisterModifier(ModifierType.Particles, amount, user, color);
                    }
                    else if (command == "zoffset")
                    {
                        if (amount == 0) return;
                        CommandManager.RegisterModifier(ModifierType.ZOffset, amount, user, color);
                    }
                    else if (command == "scale")
                    {
                        CommandManager.RegisterModifier(ModifierType.Scale, amount, user, color);
                    }
                    else if (command == "stutterchains")
                    {
                        CommandManager.RegisterModifier(ModifierType.StutterChains, amount, user, color);
                    }
                }
                else
                {
                    if (command == "mines")
                    {
                        CommandManager.RegisterModifier(ModifierType.Mines, user, color);
                    }
                    else if (command == "invisguns")
                    {
                        CommandManager.RegisterModifier(ModifierType.InvisGuns, user, color);
                    }
                    else if (command == "wobble")
                    {
                        CommandManager.RegisterModifier(ModifierType.Wobble, user, color);
                    }
                    else if (command == "wooble")
                    {
                        CommandManager.RegisterModifier(ModifierType.Wobble, -2, user, color);
                    }
                    else if (command == "wrobl")
                    {
                        CommandManager.RegisterModifier(ModifierType.Wobble, -3, user, color);
                    }
                    else if (command == "randomoffset")
                    {
                        CommandManager.RegisterModifier(ModifierType.RandomOffset, user, color);
                    }
                    else if (command == "bettermelees")
                    {
                        CommandManager.RegisterModifier(ModifierType.BetterMelees, user, color);
                    }
                    else if (command == "randomcolors")
                    {
                        CommandManager.RegisterModifier(ModifierType.RandomColors, user, color);
                    }
                    else if (command == "colorswap")
                    {
                        CommandManager.RegisterModifier(ModifierType.ColorSwap, user, color);
                    }
                    else if (command == "streammode")
                    {
                        CommandManager.RegisterModifier(ModifierType.StreamMode, user, color);
                    }
                    else if (command == "hiddenteles")
                    {
                        CommandManager.RegisterModifier(ModifierType.HiddenTelegraphs, user, color);
                    }
                    else if(command == "unifycolors")
                    {
                        CommandManager.RegisterModifier(ModifierType.UnifyColors, user, color);
                    }
                    else if(command == "rtxon")
                    {
                        CommandManager.RegisterModifier(ModifierType.TimingAttack, user, color);
                    }
                    else if(command == "dropnuke")
                    {
                        //CommandManager.RegisterModifier(ModifierType.Nuke, user, color);
                    }
                    else if(command == "lightshow")
                    {
                        //CommandManager.RegisterModifier(ModifierType.BopMode, user, color);
                    }

                }
            }
        }

        private static float ParseAmount(string msg)
        {
            int.TryParse(msg, out int amount);
            return amount;
        }
    }
}
