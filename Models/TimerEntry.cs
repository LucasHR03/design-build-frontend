namespace Notes.Models
{
    // representere en enkelt timer entry, med filnavn, varighed og tidsstempel
    public class TimerEntry
    {
        public string Filename { get; set; }
        public string Duration { get; set; }
        public string Timestamp { get; set; }
    }
}
