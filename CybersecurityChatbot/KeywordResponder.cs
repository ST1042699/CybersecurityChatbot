using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class KeywordResponder
    {
        private readonly Dictionary<string, List<string>> _responses;
        private readonly Random _random = new Random();

        public KeywordResponder()
        {
            _responses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", new List<string> {
                    "Use strong, unique passwords with uppercase, lowercase, numbers, and symbols.",
                    "Never reuse passwords across multiple accounts. Consider using a password manager." } },
                { "phishing", new List<string> {
                    "Be cautious of emails asking for personal information.",
                    "Always verify the sender's email address before clicking links." } },
                { "privacy", new List<string> {
                    "Review your privacy settings regularly.",
                    "Be mindful of what you share online." } },
                { "scam", new List<string> {
                    "Stay alert for scam calls or messages.",
                    "Never send money to unknown or unverified sources." } },
                { "malware", new List<string> {
                    "Keep your antivirus software up to date.",
                    "Avoid clicking on suspicious links or downloads." } },
                { "Browsing safety tips", new List<string>{
                  "Look for https and th padlock in the address bar.",
                  "Avoid public WI-FI for sensitive Transactions."} },
                { "social engineer", new List<string>{
                  "Social engineering involves the manupulation to obtain confidential information.",
                  "Be cautious when asked for passwords or personal info." } },
                { "Backup", new List<string>{
                  "Regular backups are essential. Keep at least 3 copies.",
                  "Including offline ones."} },
                { "Two factor", new List<string>{
                  "Two factor Authentication adds an extra layer of security.",
                  "Even if someone gets your password they still need your second factor code."} }
            };
        }

        public string GetResponse(string input)
        {
            foreach (var key in _responses.Keys)
            {
                if (input.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var responses = _responses[key];
                    return responses[_random.Next(responses.Count)];
                }
            }
            return null;
        }

        public List<string> GetAllKeywords()
        {
            return new List<string>(_responses.Keys);
        }
    }
}