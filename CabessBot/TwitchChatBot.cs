using CabessBot.Commands;
using CabessBot.Interfaces;
using System;
using System.Collections.Generic;
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
        public ConnectionCredentials Credentials { private get; set; }
        private List<ICommand> Commands { get; set; }
        private TwitchClient TwitchClient { get; set; }


        public TwitchChatBot()
        {
            Credentials = new ConnectionCredentials(TwitchInfo.BotUserName, TwitchInfo.BotToken);
            TwitchClient = new TwitchClient(Credentials, TwitchInfo.ChannelName);

            Commands = new List<ICommand>();
            Commands.Add(new UpTimeCommand(TwitchClient));
            Commands.Add(new SalveCommand(TwitchClient));
            Commands.Add(new MemeCommand("cabess"));
            Commands.Add(new MemeCommand("aipaipara"));
            Commands.Add(new GifCommand("blowingmind"));
            Commands.Add(new GifCommand("truestory"));
        }

        internal void Connect()
        {
            Console.WriteLine("Connecting...");

            TwitchClient.ChatThrottler = new MessageThrottler(20, TimeSpan.FromSeconds(30));

            TwitchClient.OnLog += Client_OnLog;
            TwitchClient.OnConnectionError += Client_OnConnectionError;
            TwitchClient.OnMessageReceived += Client_OnMessageReceived;
            TwitchClient.OnConnected += Client_OnConnected;

            TwitchClient.Connect();

            TwitchAPI.Settings.ClientId = TwitchInfo.ClientId;
        }
        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            TwitchClient.SendMessage("Confira todos os comandos com !Comandos e todos os Memes com !Memes");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            ICommand command = Commands.FirstOrDefault(x => x.CommandDescription.Equals(e.ChatMessage.Message));
            if (command != null)
               command.Execute(e, command.MemeSong, command.GifFile);               
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
            TwitchClient.Disconnect();
        }
    }
}