using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class MemoryStore
    {
        public string UserName { get; set; }
        public string FavouriteTopic { get; set; }

        private readonly Dictionary<string, string> _store = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public void Store(string key, string value)
        {
            _store[key] = value;
        }

        public string Recall(string key)
        {
            return _store.ContainsKey(key) ? _store[key] : null;
        }

        public string GetPersonalisedOpener()
        {
            string name = UserName ?? "there";
            string topic = FavouriteTopic != null ? $" about {FavouriteTopic}" : "";
            return $"Hi {name}! I'm here to help you stay safe online{topic}.";
        }
    }
}