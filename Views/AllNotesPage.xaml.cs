namespace Notes.Views;

public partial class AllNotesPage : ContentPage
{
    public AllNotesPage()
    {
        InitializeComponent();

        BindingContext = new Models.AllNotes();
    }

    // kaldet når siden åbnes fra en anden side
    protected override void OnAppearing()
    {
        ((Models.AllNotes)BindingContext).LoadNotes();  // reloader noterne 
    }

    // kaldet når der trykkes på add knappen
    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NotePage));
    }

    // kaldet når der trykkes på en note
    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // finder den valgte note
            var note = (Models.Note)e.CurrentSelection[0];

            // navigerer til NotePage og sender note filnavnet
            await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.Filename}");

            // resetter valget i CollectionView
            notesCollection.SelectedItem = null;
        }
    }
}