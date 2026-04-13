using CybersecurityCahtbot;
using System;
using System.Threading;

// This part of my chat bot is used to create my ASCII art //
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
            // VOICE GREETING IS PLACED HERE //
            AudioPlayer.PlayGreeting();

            // DISPLAY ASCII ART HERE //
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(_asciiArt);
            Console.ResetColor();

            // GET USER'S NAME //
            _user.AskForName();

            // A PERSONALISED WELCOME //
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nHello {_user.Name}! Welcome to the Cybersecurity Awareness Bot.");
            Console.WriteLine("I'm here to help you stay safe online.\n");
            Console.WriteLine("I'm here to help you to stay safe online.\n");
            Console.ResetColor();

            ShowHeader();

            bool running = true;
            while (running)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n Ask me anything about cybersecuirty (or type 'exit' to quit): ");
                Console.ResetColor();

                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    HandleInvalidInput();
                    continue;
                }

                if (input.ToLower() == "exit" || input.ToLower() == "quit" || input.ToLower() == "bye")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThank you for using the Cybersecurity Awareness Bot. Stay safe online!");
                    Console.ResetColor();
                    running = false;
                    continue;
                }

                // Get response and show the typing effect //
                string response = GetCybersecurityResponse(input.ToLower());
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("Bot: ");
                TypeWriterEffect(response);
                Console.ResetColor();
            }

        }

        private void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("======================================================================");
            Console.WriteLine("                CYBERSECURITY AWARENESS CHATBOT                       ");
            Console.WriteLine("======================================================================");
            Console.ResetColor();
        }

       // This part of my chatbot is where the chatbot responses are placed //
        private string GetCybersecurityResponse(string input)
        {
            // CHATBOT RESPONSE TOPICS //
            if (input.Contains("password") || input.Contains("strong password"))
                return "Use strong, unique passwords with uppercase, lowercase, numbers, ans symbols. Never reuse the same password across different accounts. Consider using a password manager.";

            if (input.Contains("phishing") || input.Contains("fake email"))
                return " Phishing is when attackers send fake emails or messages pretending to be from trusted companies to steal your personal information. Always check the sender's email address and never click suspicious links.";

            if (input.Contains("two factor") || input.Contains("2FA") || input.Contains("multi factor"))
                return "Two-Factor Authentication (2FA) adds an extra layer of security. Even if someone gets your password, they still need your second factor (like a code sent to your phone) to log in.";

            if (input.Contains("virus") || input.Contains("malware") || input.Contains("ransomware"))
                return "Malware does include virruses, ransome, as well as trojans. Always keep your antivrisus software updated, avoid downloading cracked software, and never open any email attachments from any unkknown senders.";

            if (input.Contains("safe browsing") || input.Contains("https"))
                return "Browsing safety tips: Always look for HTTPS and the padlock icon in the address bar. When banking or shopping avoid using any public Wi-Fi. Keep your broswers and operating systems updaeted.";

            if (input.Contains("social engineering"))
                return "Social engineering happens when attackers manipulate people into giving out confidential information. Be cautious when someone is pressuring for passwords or anything personal.";

            if (input.Contains("update") || input.Contains("software update"))
                return "Always install software promptly. Many cyberattacks will exploit know vulnerable information that have already been patched by developers.";

            if (input.Contains("backup") || input.Contains("data backup"))
                return "Regular backups are very essential. Make at least 3 copies of your data. Save 1 on your actual laptop and 2 on a hard drive or flash drive.";

            // THIS IS A 9TH QUESTION A FRIENDLY OPTION IF THE REQUESTED QUESTION IS INVALID //
            return "I'm sorry, I could not understand your question. Can you please rephrase?";
        }

        private void TypeWriterEffect(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(25);
            }
            Console.WriteLine("\n");
        }

        private void HandleInvalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please type a question. Empty messages are not allowed.");
            Console.ResetColor();
        }
    }
}