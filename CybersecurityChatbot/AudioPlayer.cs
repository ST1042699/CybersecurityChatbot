using System.Media;

namespace CybersecurityChatbot
{
    // for my audio to player i created the audio class and took my logic from my audio class in part one 
    public static class AudioPlayer
    {
        public static void PlayGreeting()
        {
            var player = new SoundPlayer("greeting.wav");
            player.Load();
            player.Play();
        }
    }
}