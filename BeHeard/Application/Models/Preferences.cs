using System;

namespace BeHeard.Application.Models
{
    public class Preferences
    {
        public bool ColorBlindMode { get; set; }
        public bool DarkMode       { get; set; }
        public bool TextToSpeech   { get; set; }
        public Guid Id             { get; set; }
    }
}
