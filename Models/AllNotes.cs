using System.Collections.ObjectModel;

namespace Notes.Models;

// håndterer alle note filler
internal class AllNotes
{
    // ObservableCollection er en liste indeholdende de noter der kan observeres
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    // initialiserer klassen og kalder LoadNotes metoden
    public AllNotes() =>
        LoadNotes();

    // loader alle noter fra appDataDirectory
    public void LoadNotes()
    {
        Notes.Clear();

        // henter appDataDirectory
        string appDataPath = FileSystem.AppDataDirectory;

        // henter alle filer der ender på .notes.txt
        IEnumerable<Note> notes = Directory
                                    .EnumerateFiles(appDataPath, "*.notes.txt")
                                    .Select(filename => new Note()
                                    {
                                        Filename = filename,
                                        Text = File.ReadAllText(filename),
                                        Date = File.GetLastWriteTime(filename)
                                    })
                                    .OrderBy(note => note.Date); // gemmer noter i rækkefølge efter dato

        // tilføjer noterne til ObservableCollection
        foreach (Note note in notes)
            Notes.Add(note);
    }
}