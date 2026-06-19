using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private readonly ChatBot _chatBot;
        private readonly TaskManager _taskManager;
        private readonly QuizManager _quizManager;
        private readonly ActivityLogger _activityLogger;

        private string selectedAnswer = "";

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

            _activityLogger = new ActivityLogger();
            _taskManager = new TaskManager(_activityLogger);
            _quizManager = new QuizManager();

            _chatBot = new ChatBot(_taskManager, _activityLogger, _quizManager);

            AsciiArtTextBlock.Text = ASCIIArt;
            AppendBotMessage(_chatBot.GetGreeting());
            PlayGreetingSound();

            LoadTasksToUI();
        }

        private void PlayGreetingSound()
        {
            try { AudioPlayer.PlayGreeting(); } catch { }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e) => SendMessage();
        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SendMessage();
        }

        private void SendMessage()
        {
            string userText = UserInputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(userText)) return;

            AppendUserMessage(userText);
            UserInputTextBox.Clear();

            string response = _chatBot.ProcessInput(userText);
            AppendBotMessage(response);
        }

        private void AppendUserMessage(string message)
        {
            var txt = new TextBlock { Text = "You: " + message, Foreground = Brushes.LightGreen, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 2, 0, 2) };
            ChatStackPanel.Children.Add(txt);
            ScrollToBottom();
        }

        private void AppendBotMessage(string message)
        {
            var txt = new TextBlock { Text = "Bot: " + message, Foreground = Brushes.LightCyan, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 2, 0, 2) };
            ChatStackPanel.Children.Add(txt);
            ScrollToBottom();
        }

        private void ScrollToBottom() => ChatScrollViewer.ScrollToEnd();

        // task management
        private void LoadTasksToUI()
        {
            if (lvTasks != null)
                lvTasks.ItemsSource = _taskManager.GetAllTasks();
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskTitle?.Text)) return;
            _taskManager.AddTask(txtTaskTitle.Text.Trim(), txtTaskDescription.Text.Trim(), txtReminder.Text.Trim());
            LoadTasksToUI();
            txtTaskTitle.Clear(); txtTaskDescription.Clear(); txtReminder.Clear();
        }

        private void btnMarkComplete_Click(object sender, RoutedEventArgs e)
        {
            if (lvTasks?.SelectedItem is CyberTask task)
            {
                _taskManager.MarkAsComplete(task.Id);
                LoadTasksToUI();
            }
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lvTasks?.SelectedItem is CyberTask task)
            {
                _taskManager.DeleteTask(task.Id);
                LoadTasksToUI();
            }
        }

        // QUIZ information 
        private void btnStartQuiz_Click(object sender, RoutedEventArgs e)
        {
            _quizManager.Reset();
            ShowCurrentQuestion();
            _activityLogger.Log("Quiz started");
        }

        private void ShowCurrentQuestion()
        {
            if (_quizManager.IsFinished())
            {
                txtQuestion.Text = "Quiz Completed!";
                txtFeedback.Text = _quizManager.GetFinalScore() + "\n" + _quizManager.GetFinalMessage();
                btnSubmitAnswer.IsEnabled = false;
                btnNextQuestion.Visibility = Visibility.Collapsed;
                return;
            }

            var q = _quizManager.GetCurrentQuestion();
            txtQuestion.Text = q?.Question ?? "No question available";
            txtFeedback.Text = "";
            selectedAnswer = "";

            OptionsPanel.Children.Clear();
            btnSubmitAnswer.IsEnabled = true;
            btnNextQuestion.Visibility = Visibility.Collapsed;

            foreach (var option in q?.Options ?? new List<string>())
            {
                Button btn = new Button
                {
                    Content = option,
                    Margin = new Thickness(5, 8, 5, 8),
                    Padding = new Thickness(12),
                    FontSize = 14,
                    Background = new SolidColorBrush(Colors.White),
                    Foreground = new SolidColorBrush(Colors.Black),
                    Tag = option,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                btn.Click += OptionButton_Click;
                OptionsPanel.Children.Add(btn);
            }
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                selectedAnswer = btn.Tag?.ToString();
            }
        }

        private void btnSubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedAnswer))
            {
                txtFeedback.Text = "❗ Please select an answer first.";
                return;
            }

            bool correct = _quizManager.SubmitAnswer(selectedAnswer);
            txtFeedback.Text = (correct ? "✅ Correct! " : "❌ Incorrect. ") + _quizManager.GetFeedback(correct);

            btnSubmitAnswer.IsEnabled = false;
            btnNextQuestion.Visibility = Visibility.Visible;
        }

        private void btnNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            ShowCurrentQuestion();
        }

        // activity log
        private void RefreshLog_Click(object sender, RoutedEventArgs e)
        {
            RefreshActivityLog();
        }

        private void RefreshActivityLog()
        {
            if (txtActivityLog != null)
                txtActivityLog.Text = _activityLogger.GetRecentLog(10);
        }
    }
}