using CabessBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.Client;

namespace CabessBot.Classes
{
    public class SalveCommand : ICommand
    {
        public string CommandDescription { get; set; }
        public OnMessageReceivedArgs Event { get; set; }
        public TwitchClient TwitchClient { get; set; }
        public string MemeSong { get; set; }

        public SalveCommand(TwitchClient twitchClient)
        {
            CommandDescription = "!salve";
            TwitchClient = twitchClient;
        }
        public void Execute(OnMessageReceivedArgs e, string memeSong = null)
        {
            TwitchClient.SendMessage($"Tá Salvo {e.ChatMessage.DisplayName}!");
        }
    }
}
