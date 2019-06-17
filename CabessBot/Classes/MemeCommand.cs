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
    public class MemeCommand : ICommand
    {
        public string CommandDescription { get; set; }
        public OnMessageReceivedArgs Event { get; set; }
        public TwitchClient TwitchClient { get; set; }

        private MediaPlayer MediaPlayer { get; set; }
        public string MemeSong { get; set; }

        public MemeCommand(string memeSong)
        {
            MemeSong = memeSong.ToLower();
            CommandDescription = $"!meme {memeSong}";

            MediaPlayer = new MediaPlayer();
        }
        public void Execute(OnMessageReceivedArgs e, string memeSong)
        {
            MediaPlayer.Play(memeSong);
        }
    }
}
