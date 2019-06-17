using System;

namespace CabessBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new TwitchChatBot();
            bot.Connect();
            Console.ReadKey();
            bot.Disconnect();
        }
    }
}
