using Notes.Models;
using Notes.Views;

namespace Notes.Views
{
    public partial class TimerDetailPage : ContentPage
    {
        private TimerEntry _entry;  // timerEntry object der gemmer timerens data

        // constructor der modtager en timerEntry som parameter
        public TimerDetailPage(TimerEntry entry)
        {
            InitializeComponent();
            _entry = entry; // gemmer den indkommende timerEntry
            BindingContext = _entry; // setter BindingContext til den indkommende timerEntry
        }

        // slet knap
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            // sletter filen hvis den findes
            if (File.Exists(_entry.Filename))
            {
                File.Delete(_entry.Filename);
            }

            await Navigation.PopAsync();
        }
        // navigere til timer siden
        private async void OnNavigateToAboutPageClicked(object sender, EventArgs e)
        {
            var aboutPage = new AboutPage();    // opretter en ny instans af timer siden
            await Navigation.PushAsync(aboutPage);  // navigerer til timere siden
        }
    }
}
