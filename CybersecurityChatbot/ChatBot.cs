using System;

namespace CybersecurityChatbot
{
    public class ChatBot
    {
        private readonly KeywordResponder _keywords;
        private readonly SentimentDetector _sentiment;
        private readonly MemoryStore _memory;

        // Part 3 information is added here 
        private readonly TaskManager _taskManager;
        private readonly ActivityLogger _activityLogger;
        private readonly QuizManager _quizManager;

        private bool _awaitingName = true;
        private string _lastTopic = "";

        
        public ChatBot(TaskManager taskManager, ActivityLogger logger, QuizManager quizManager)
        {
            _taskManager = taskManager;
            _activityLogger = logger;
            _quizManager = quizManager;

            _keywords = new KeywordResponder();
            _sentiment = new SentimentDetector();
            _memory = new MemoryStore();
        }

        public string GetGreeting()
        {
            return "Hello! What's your name?";
        }

        public string ProcessInput(string userInput)
        {
            string input = userInput.ToLower().Trim();

            if (_awaitingName)
            {
                _memory.UserName = userInput;
                _awaitingName = false;
                return $"Nice to meet you, {userInput}! How can I help you stay safe online?";
            }

           

            // Task 
            if (input.Contains("add task") || input.Contains("add a task") || input.Contains("create task") ||
                input.Contains("enable") || input.Contains("set up"))
            {
                string title = ExtractTaskTitle(userInput);
                _activityLogger.Log($"NLP recognised task intent from: '{userInput}'");
                return _taskManager.AddTask(title, "Added via chatbot");
            }

            // Reminder 
            if (input.Contains("remind me") || input.Contains("set a reminder"))
            {
                _activityLogger.Log($"Reminder set: {userInput}");
                return "Got it! I'll remind you.";
            }

            // Start Quiz
            if (input.Contains("start quiz") || input.Contains("take quiz") || input.Contains("quiz me") ||
                input.Contains("test my knowledge") || input.Contains("play the game"))
            {
                _quizManager.Reset();
                _activityLogger.Log("Quiz started");
                return "Great! Let's start the Cybersecurity Quiz.\nAnswer with A, B, C, D or True/False.";
            }

            // Activity Log
            if (input.Contains("show activity log") || input.Contains("what have you done") ||
                input.Contains("what did you do") || input.Contains("show log"))
            {
                _activityLogger.Log("User requested activity log");
                return _activityLogger.GetRecentLog(10);
            }

            // part 2 kept the same 
            // Follow-up
            if (input.Equals("tell me more", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(_lastTopic))
            {
                return $"Here's more information about {_lastTopic}...";
            }

            // Sentiment
            var sentiment = _sentiment.Detect(userInput);
            var sentimentResponse = _sentiment.GetSentimentResponse(sentiment);

            // Keyword responses
            var keywordResponse = _keywords.GetResponse(userInput);
            if (keywordResponse != null)
            {
                _lastTopic = userInput;
                return (sentimentResponse != string.Empty ? sentimentResponse + " " : "") + keywordResponse;
            }

            // Specific phrases
            string lowerInput = input.ToLower();
            if (lowerInput.Contains("how are you"))
                return "I'm just a virtual assistant, but I'm here to help you stay safe!";
            if (lowerInput.Contains("what can i do") || lowerInput.Contains("purpose"))
                return "You can ask me about passwords, phishing, malware, safe browsing, and more.";

            // Default
            return "I'm sorry, I didn't understand that. Could you rephrase?";
        }

        private string ExtractTaskTitle(string input)
        {
            return input.Replace("add task", "", StringComparison.OrdinalIgnoreCase)
                        .Replace("add a task", "", StringComparison.OrdinalIgnoreCase)
                        .Replace("create task", "", StringComparison.OrdinalIgnoreCase)
                        .Trim();
        }
    }
}