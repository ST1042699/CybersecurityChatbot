using System;

namespace CybersecurityChatbot
{
    public class ChatBot
    {
        private readonly KeywordResponder _keywords;
        private readonly SentimentDetector _sentiment;
        private readonly MemoryStore _memory;
        private bool _awaitingName = true;
        private string _lastTopic = "";

        public ChatBot()
        {
            _keywords = new KeywordResponder();
            _sentiment = new SentimentDetector();
            _memory = new MemoryStore();
        }

        public string GetGreeting()
        {
            return "Hello! What's your name?";
        }

        public string ProcessInput(string input)
        {
            if (_awaitingName)
            {
                _memory.UserName = input;
                _awaitingName = false;
                return $"Nice to meet you, {input}! How can I help you stay safe online?";
            }

            // follow up questions
            if (input.Equals("tell me more", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(_lastTopic))
            {
                return $"Here's more information about {_lastTopic}...";
            }

            // my sentiments dectections
            var sentiment = _sentiment.Detect(input);
            var sentimentResponse = _sentiment.GetSentimentResponse(sentiment);

            // my keyword responses
            var keywordResponse = _keywords.GetResponse(input);
            if (keywordResponse != null)
            {
                _lastTopic = input;
                return (sentimentResponse != string.Empty ? sentimentResponse + " " : "") + keywordResponse;
            }

            // any specific pharses to ask the chat bot
            string lowerInput = input.ToLower();
            if (lowerInput.Contains("how are you"))
                return "I'm just a virtual assistant, but I'm here to help you stay safe!";
            if (lowerInput.Contains("what can i do") || lowerInput.Contains("purpose"))
                return "You can ask me about passwords, phishing, malware, safe browsing, and more.";

            // my default response if the user does not ask anything cybersecurity related 
            return "I'm sorry, I didn't understand that. Could you rephrase?";
        }
    }
}