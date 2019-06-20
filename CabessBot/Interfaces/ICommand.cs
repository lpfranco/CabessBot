using TwitchLib;
using TwitchLib.Events.Client;

namespace CabessBot.Interfaces
{
    public interface ICommand
    {
        string CommandDescription { get; set; }
        OnMessageReceivedArgs Event { get; set; }
        TwitchClient TwitchClient { get; set; }
        string MemeSong { get; set; }
        string GifFile { get; set; }
        void Execute(OnMessageReceivedArgs e, string memeSong = null, string gifFile = null);
    }
}
