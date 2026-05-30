using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public enum Sentiment
    {
        Neutral,
        Worried,
        Curious,
        Frustrated,
        Happy
    }

    public class SentimentDetector
    {
        private readonly Dictionary<Sentiment, List<string>> _triggerWords;

        public SentimentDetector()
        {
            _triggerWords = new Dictionary<Sentiment, List<string>>
            {
                { Sentiment.Worried, new List<string> { "worried", "scared", "afraid", "anxious", "nervous", "unsafe" } },
                { Sentiment.Curious, new List<string> { "curious", "wondering", "interested", "want to know", "how does" } },
                { Sentiment.Frustrated, new List<string> { "frustrated", "annoyed", "confused", "don't understand" } },
                { Sentiment.Happy, new List<string> { "great", "thanks", "helpful", "awesome", "love it" } }
            };
        }

        public Sentiment Detect(string input)
        {
            input = input.ToLower();
            foreach (var pair in _triggerWords)
            {
                foreach (var word in pair.Value)
                {
                    if (input.Contains(word))
                        return pair.Key;
                }
            }
            return Sentiment.Neutral;
        }

        public string GetSentimentResponse(Sentiment sentiment)
        {
            switch (sentiment)
            {
                case Sentiment.Worried:
                    return "It's understandable to feel worried. Remember, staying informed helps!";
                case Sentiment.Curious:
                    return "That's a good curiosity! Let me share some details.";
                case Sentiment.Frustrated:
                    return "I understand it can be frustrating. Let's go over some tips.";
                case Sentiment.Happy:
                    return "Glad you're feeling good! Keep practicing safe online habits.";
                default:
                    return string.Empty;
            }
        }
    }
}