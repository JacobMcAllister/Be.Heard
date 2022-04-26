namespace BeHeard.Application.Models
{
    public class BeHeardSession
    {
        public Address      Address      { get; set; }
        public Gender       Gender       { get; set; }
        public int          Age          { get; set; }
        public int          MasterVolume { get; set; } // what is this even used for
        public Preferences  Preferences  { get; set; }
        public Settings     Settings     { get; set; }
        public string       Email        { get; set; }
        public string       FirstName    { get; set; }
        public string       LastName     { get; set; }
        public string       PhoneNumber  { get; set; }
        public string       Username     { get; set; }
        public string icon { get; set; } // Better name is 'Avatar'
        public Subscription Subscription { get; set; }
        // public BeHeardRole Role { get; }
    }
}
