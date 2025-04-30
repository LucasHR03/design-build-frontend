using System.Collections.ObjectModel;

namespace Notes.Models;

// MenuViewModel klasse der håndterer menuen
public class MenuViewModel
{
    // obasaverbare liste af menu items
    public ObservableCollection<MenuItem> MenuItems { get; set; }

    // initialiserer klassen og tilføjer menu items
    public MenuViewModel()
    {
        MenuItems = new ObservableCollection<MenuItem>
        {
            new MenuItem { Title = "All Notes", Route = $"//AllNotesPage" }, // Naviger til AllNotesPage
            new MenuItem { Title = "New Note", Route = $"{nameof(Views.NotePage)}" }, // Naviger til NotePage
            new MenuItem { Title = "App Info", Route = "https://aka.ms/maui" }
        };
    }
}
