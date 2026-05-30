using System;
using System.Threading;

namespace CybersecurityChatbot
{
    public class Chatbot
    {
        private readonly User _user = new User();
        private readonly string _asciiArt = @"
__________                                                               __    
\______   \_____  ___  __  ____    ____    ____ _______  ___.__.______ _/  |_  
 |       _/\__  \ \  \/ /_/ __ \  /    \ _/ ___\\_  __ \<   |  |\____ \\   __\ 
 |    |   \ / __ \_\   / \  ___/ |   |  \\  \___ |  | \/ \___  ||  |_> >|  |   
 |____|_  /(____  / \_/   \___  >|___|  / \___  >|__|    / ____||   __/ |__|   
        \/      \/            \/      \/      \/         \/     |__|           
";

        public void Start()
        {
            PlayGreeting();
            DisplayAsciiArt();

            _user.AskForName();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nHello {_user.Name}! Welcome to the Cybersecurity Awareness Bot.");
            Console.WriteLine("I'm here to help you stay safe online.\n");
            Console.ResetColor();

            ShowHeader();

            bool isRunning = true;
            while (isRunning)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\nAsk me anything about cybersecurity (or type 'exit' to quit): ");
                Console.ResetColor();

                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    HandleInvalidInput();
                    continue;
                }

                if (IsExitCommand(input))
                {
                    ExitChat();
                    isRunning = false;
                    continue;
                }

                string response = GetCybersecurityResponse(input.ToLower());

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("Bot: ");
                TypeWriterEffect(response);
                Console.ResetColor();
            }
        }

        private void PlayGreeting()
        {
            try
            {
                AudioPlayer.PlayGreeting();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error playing greeting: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(_asciiArt);
            Console.ResetColor();
        }

        private void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("======================================================================");
            Console.WriteLine("                CYBERSECURITY AWARENESS CHATBOT                       ");
            Console.WriteLine("======================================================================");
            Console.ResetColor();
        }

        private bool IsExitCommand(string input)
        {
            string lower = input.ToLower();
            return lower == "exit" || lower == "quit" || lower == "bye";
        }

        private void ExitChat()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThank you for using the Cybersecurity Awareness Bot. Stay safe online!");
            Console.ResetColor();
        }

        private void HandleInvalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please type a question. Empty messages are not allowed.");
            Console.ResetColor();
        }

        private void TypeWriterEffect(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(25);
            }
            Console.WriteLine();
        }

        private string GetCybersecurityResponse(string input)
        {
            // Basic keyword recognition
            if (input.Contains("password") || input.Contains("strong password"))
                return "Use strong, unique passwords with uppercase, lowercase, numbers, and symbols. Never reuse the same password across different accounts. Consider using a password manager.";
            if (input.Contains("phishing") || input.Contains("fake email"))
                return "Phishing is when attackers send fake emails or messages pretending to be from trusted companies to steal your personal information. Always check the sender's email address and never click suspicious links.";
            if (input.Contains("two factor") || input.Contains("2FA") || input.Contains("multi factor"))
                return "Two-Factor Authentication (2FA) adds an extra layer of security. Even if someone gets your password, they still need your second factor (like a code sent to your phone) to log in.";
            if (input.Contains("virus") || input.Contains("malware") || input.Contains("ransomware"))
                return "Malware includes viruses, ransomware, and trojans. Keep your antivirus software updated, avoid downloading cracked software, and never open email attachments from unknown senders.";
            if (input.Contains("safe browsing") || input.Contains("https"))
                return "Browsing safety tips: Look for HTTPS and the padlock in the address bar. Avoid public Wi-Fi for sensitive transactions. Keep browsers and OS updated.";
            if (input.Contains("social engineering"))
                return "Social engineering involves manipulation to obtain confidential information. Be cautious when asked for passwords or personal info.";
            if (input.Contains("update") || input.Contains("software update"))
                return "Always install software updates promptly. They patch vulnerabilities that cybercriminals exploit.";
            if (input.Contains("backup") || input.Contains("data backup"))
                return "Regular backups are essential. Keep at least three copies, including one offline.";
            // Default response
            return "I'm sorry, I could not understand your question. Can you please rephrase?";
        }
    }
}