using Microsoft.Maui.Controls;

namespace Notes.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string cpr = CprEntry.Text;
        string password = PasswordEntry.Text;

        if (cpr == "1234567890" && password == "hemmelig")
        {
            await DisplayAlert("Velkommen", "Du er logget ind", "OK");

            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Fejl", "Forkert CPR-nummer eller adgangskode", "OK");
        }
    }
}