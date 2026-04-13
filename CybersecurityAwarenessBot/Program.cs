using System;

namespace CybersecurityAwarenessBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.ForegroundColor = ConsoleColor.Cyan;

            ChatBot bot = new ChatBot();

            bot.PlayVoiceGreeting();  // Now shows clear status
            bot.DisplayASCIILogo();
            bot.GetUserNameAndGreet();
            bot.StartChat();
        }
    }
}