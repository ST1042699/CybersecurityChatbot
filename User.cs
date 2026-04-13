using System;

// This class is created to handle the user's input // 

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