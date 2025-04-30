using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Storage;
using Notes.Models;
using Notes.Views;

namespace Notes.Views;

public partial class AboutPage : ContentPage
{
    private DateTime startTime; // gemmer når timeren startes
    private bool isTimerRunning;    // gemmer om timeren kører
    private IDispatcherTimer timer; // timer objekt

    public ObservableCollection<TimerEntry> Entries { get; set; } = new(); // gemmer timer entries

    public AboutPage()
    {
        InitializeComponent();
        BindingContext = this;
        LoadSavedTimers();
    }
    // starter timer
    private void StartButton_Clicked(object sender, EventArgs e)
    {
        startTime = DateTime.Now;
        isTimerRunning = true;

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(10); // opdaterer hvert 10 ms, kan ændres efter behov
        timer.Tick += (_, _) =>
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            TimerLabel.Text = elapsed.TotalSeconds.ToString("0.00");    // opdaterer label med tid der er gået
        };
        timer.Start();
    }
    // stopper timer og gemmer den
    private void StopButton_Clicked(object sender, EventArgs e)
    {
        if (isTimerRunning)
        {
            timer.Stop();
            isTimerRunning = false;

            TimeSpan elapsed = DateTime.Now - startTime;
            string duration = elapsed.TotalSeconds.ToString("0.00");

            // Gemmer timeren i en tekstfil
            string filename = Path.Combine(FileSystem.AppDataDirectory, $"{Path.GetRandomFileName()}.timer.txt");
            File.WriteAllText(filename, duration);

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Debug.WriteLine($"Timer saved at: {filename}");

            // tilføjer timeren til listen
            Entries.Add(new TimerEntry
            {
                Filename = filename,
                Duration = duration,
                Timestamp = timestamp
            });

            TimerLabel.Text = "0.00";   // resetter timeren på displayet 
        }
    }

    // loader gemte timere 
    private void LoadSavedTimers()
    {
        Entries.Clear();
        var files = Directory.EnumerateFiles(FileSystem.AppDataDirectory, "*.timer.txt");

        foreach (var file in files)
        {
            string duration = File.ReadAllText(file);
            string timestamp = File.GetLastWriteTime(file).ToString("yyyy-MM-dd HH:mm:ss");

            Entries.Add(new TimerEntry
            {
                Filename = file,
                Duration = duration,
                Timestamp = timestamp
            });
        }
    }

    // sletter timer fillen og  fjerner den fra ObservableCollection listen
    private void OnDeleteTimer(object sender, EventArgs e)
    {
        if ((sender as SwipeItem)?.CommandParameter is TimerEntry entry)
        {
            if (File.Exists(entry.Filename))
                File.Delete(entry.Filename); 

            Entries.Remove(entry); 
        }
    }
    // navigerer til TimerDetailPage når der trykkes på en timer
    private async void OnTimerTapped(object sender, EventArgs e)
    {
        if ((sender as VisualElement)?.BindingContext is TimerEntry selectedTimer)
        {
            await Navigation.PushAsync(new TimerDetailPage(selectedTimer));
        }
    }
}