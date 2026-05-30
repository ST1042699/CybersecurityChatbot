using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        // this is where i added my ASCII for part 1 
        private readonly ChatBot _chatBot;
        private readonly string ASCIIArt = @"
__________                                                               __    
\______   \_____  ___  __  ____    ____    ____ _______  ___.__.______ _/  |_  
 |       _/\__  \ \  \/ /_/ __ \  /    \ _/ ___\\_  __ \<   |  |\____ \\   __\ 
 |    |   \ / __ \_\   / \  ___/ |   |  \\  \___ |  | \/ \___  ||  |_> >|  |   
 |____|_  /(____  / \_/   \___  >|___|  / \___  >|__|    / ____||   __/ |__|   
        \/      \/            \/      \/      \/         \/     |__|           
";

        public MainWindow()
        {
            InitializeComponent();

            // my ascii art is displayed here
            AsciiArtTextBlock.Text = ASCIIArt;

           
            _chatBot = new ChatBot();

            // this is where i displayed my greeting 
            AppendBotMessage(_chatBot.GetGreeting());

            // this is where my greeting sound will be played 
            PlayGreetingSound();
        }

        private void PlayGreetingSound()
        {
            try
            {
                AudioPlayer.PlayGreeting();
            }
            catch
            {
               
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string userText = UserInputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(userText))
                return;

            AppendUserMessage(userText);
            UserInputTextBox.Clear();

            string response = _chatBot.ProcessInput(userText);
            AppendBotMessage(response);
        }

        private void AppendUserMessage(string message)
        {
            var txt = new TextBlock
            {
                Text = "You: " + message,
                Foreground = Brushes.LightGreen,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 2, 0, 2)
            };
            ChatStackPanel.Children.Add(txt);
            ScrollToBottom();
        }

        private void AppendBotMessage(string message)
        {
            var txt = new TextBlock
            {
                Text = "Bot: " + message,
                Foreground = Brushes.LightCyan,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 2, 0, 2)
            };
            ChatStackPanel.Children.Add(txt);
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToEnd();
        }
    }
}