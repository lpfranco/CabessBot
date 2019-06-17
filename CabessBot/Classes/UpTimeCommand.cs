using CabessBot.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.API.v5.Users;
using TwitchLib.Models.Client;

namespace CabessBot.Classes
{
    public class UpTimeCommand : ICommand
    {
        public string CommandDescription { get; set; }
        public OnMessageReceivedArgs Event { get; set; }
        public TwitchClient TwitchClient { get; set; }
        public string MemeSong { get; set; }

        public UpTimeCommand(TwitchClient twitchClient)
        {
            CommandDescription = "!uptime";
            TwitchClient = twitchClient;
        }

        public void Execute(OnMessageReceivedArgs e, string memeSong = null)
        {
            var upTime = GetUpTime().ToString();
            if (!string.IsNullOrWhiteSpace(upTime))
                TwitchClient.SendMessage(GetUpTime().ToString().Substring(0, 8));
            else
                TwitchClient.SendMessage("A stream está offline bro!");
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
            try
            {
                User[] lstUsers = TwitchAPI.Users.v5.GetUserByName(userName).Result.Matches;

                return lstUsers.FirstOrDefault().Id;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Ocorreu o erro: {e.Message}");
                return null;
            }       
        }
    }
}
