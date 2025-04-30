namespace Notes.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))] //attribut der tillader at sende ItemId som parameter
public partial class NotePage : ContentPage
{

    public NotePage()
    {
        InitializeComponent();
        
        // når ny note laves, dannes et random filnavn
        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        //opretter en ny blank note
        LoadNote(Path.Combine(appDataPath, randomFileName));
    }

    // gem knap
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        // gemmer det fra editor feltet i en fil
        if (BindingContext is Models.Note note)
            File.WriteAllText(note.Filename, TextEditor.Text);

        // går til forrige side
        await Shell.Current.GoToAsync("..");
    }

    // slet knap
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            // sletter filen hvis den findes
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    // loader noten fra fil
    private void LoadNote(string fileName)
    {
        Models.Note noteModel = new Models.Note();
        noteModel.Filename = fileName;

        // hvis filen findes, så indlæses den
        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        // refresher editor feltet
        BindingContext = noteModel;
    }

    // sikre at navigering til denne side kan ske med en parameter
    public string ItemId
    {
        set { LoadNote(value); }
    }
}