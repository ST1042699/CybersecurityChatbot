using System;
using System.Media;

// this code is used to handle my voice greeting which allows the .wav file to be played //

namespace CybersecurityCahtbot
{
    public static class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                Console.WriteLine("Playing voice greeting...\n");
                Console.ResetColor();
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Could not play audio: {ex.Message}");
                Console.WriteLine(" Make sure 'greeting.wav' is in the project and set to 'Copy always' .");
                Console.WriteLine(" Also check that the file is a real .wav file.");
                Console.ResetColor();
            }
        }
    }
}