namespace BeHeard.Application.Models
{
    public class BeHeardSession
    {
        public Gender       Gender       { get; set; }
        public int          Age          { get; set; }
        public int          MasterVolume { get; set; } // what is this even used for
        public Preferences  Preferences  { get; set; }
        public string       Address      { get; set; }
        public string       Email        { get; set; }
        public string       FirstName    { get; set; }
        public string       LastName     { get; set; }
        public string       PhoneNumber  { get; set; }
        public string       Username     { get; set; }
        public Subscription Subscription { get; set; }
    }
}
