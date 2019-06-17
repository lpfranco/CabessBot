using System;
using System.Linq;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.API.v5.Users;
using TwitchLib.Models.Client;
using TwitchLib.Services;

namespace CabessBot
{
    internal class TwitchChatBot
    {
        readonly ConnectionCredentials credentials = new ConnectionCredentials(TwitchInfo.BotUserName, TwitchInfo.BotToken);
        TwitchClient client;

        internal void Connect()
        {
            Console.WriteLine("Connecting...");
            client = new TwitchClient(credentials, TwitchInfo.ChannelName);

            client.ChatThrottler = new MessageThrottler(20/2, TimeSpan.FromSeconds(30));

            client.OnLog += Client_OnLog;
            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;

            client.Connect();

            TwitchAPI.Settings.ClientId = TwitchInfo.ClientId;
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            var mp = new MediaPlayer();
            if (e.ChatMessage.Message.StartsWith("!salve", StringComparison.InvariantCultureIgnoreCase))
                client.SendMessage($"Tá Salvo {e.ChatMessage.DisplayName}!");
            else if(e.ChatMessage.Message.StartsWith("!uptime", StringComparison.InvariantCultureIgnoreCase))
            {
                var upTime = GetUpTime().ToString();
                if (!string.IsNullOrWhiteSpace(upTime))
                    client.SendMessage(GetUpTime().ToString().Substring(0, 8));
                else
                    client.SendMessage("A stream está offline bro!");
            }
            else if(e.ChatMessage.Message.Equals("!meme cabess"))
            {
                mp.Play("Cabess");
            }
                            
        }

        private TimeSpan? GetUpTime()
        {
            string userId = GetUserId(TwitchInfo.ChannelName);
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            return TwitchAPI.Streams.v5.GetUptime(userId).Result;
        }

        private string GetUserId(string userName)
        {
            User[] lstUsers = TwitchAPI.Users.v5.GetUserByName(userName).Result.Matches;
            if (lstUsers == null || lstUsers.Length == 0)
                return null;

            return lstUsers.FirstOrDefault().Id;
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Console.WriteLine($"Error! { e.Error }");
        }

        internal void Disconnect()
        {
            client.Disconnect();
        }
    }
}