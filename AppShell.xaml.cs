namespace Notes
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // registrere ruter, såldes at der kan navigeres til dem    
            Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));// navigation til NotePage

            Routing.RegisterRoute(nameof(Views.AllNotesPage), typeof(Views.AllNotesPage));// navigation til AllNotesPage
        }
    }
}
