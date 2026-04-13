using System;

// this class is where the user will put there name and the chatbot will give a personalised welcome message //

namespace CybersecurityChatbot
{
    public class User
    {
        public string Name { get; private set; }

        public void AskForName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter your name: ");
            Console.ResetColor();

            Name = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(Name))
            {
                Name = "Cyber Friend";
            }
        }
    }
}