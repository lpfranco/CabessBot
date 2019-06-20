using CabessBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Events.Client;

namespace CabessBot.Commands
{
    public class GifCommand : ICommand
    {
        public string CommandDescription { get; set; }
        public OnMessageReceivedArgs Event { get; set; }
        public TwitchClient TwitchClient { get; set; }

        private GifAnimator GifAnimator { get; set; }
        public string MemeSong { get; set; }
        public string GifFile { get; set; }

        public GifCommand(string gifFile)
        {
            GifFile = gifFile.ToLower();
            CommandDescription = $"!gif {gifFile}";

            GifAnimator = new GifAnimator();
        }

        public void Execute(OnMessageReceivedArgs e, string memeSong = null, string gifFile = null)
        {
            GifAnimator.Animate(gifFile);
        }
    }
}
