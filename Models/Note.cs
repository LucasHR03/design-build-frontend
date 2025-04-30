namespace Notes.Models;

// representerer en enkeln note
internal class Note
{
    public string Filename { get; set; } // filnavn
    public string Text { get; set; }    // indhold
    public DateTime Date { get; set; }  // dato
}