using System;
using System.Media;

namespace CybersecurityChatbot
{
    public static class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                Console.WriteLine("Playing voice greeting...");
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Could not play audio: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}