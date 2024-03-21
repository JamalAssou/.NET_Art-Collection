using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace MyApp.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    public ObservableCollection<Art> MyObservableArts { get; } = new();
    [ObservableProperty]
    string animalName;
    DeviceOrientationService myScanner;

    [ObservableProperty]
    string? codeBar;
    public MainViewModel()
    {
        this.myScanner = new DeviceOrientationService();
        myScanner.ConfigureScanner();
        myScanner.SerialBuffer.Changed += OnBarCodeScanned;
    }

    private void OnBarCodeScanned(object sender, EventArgs args)
    {
        DeviceOrientationService.QueueBuffer MyLocalBuffer = (DeviceOrientationService.QueueBuffer)sender;

        CodeBar = MyLocalBuffer.Dequeue().ToString();

    }

    [RelayCommand]
    private async Task AddArt()
    {
        IsBusy = true;

        JSONServices MyService = new();

        Art NewArt = new ();
        NewArt.Name = "Jokante";
        NewArt.Description = "The best work of art in the world";
        NewArt.Author = "Jamal";
        NewArt.Picture = "la_joconde.jpg";
       
        Globals.MyArts.Add(NewArt);
        MyObservableArts.Add(NewArt);

        await MyService.SetSpiders();

        IsBusy = false;
    }
    [RelayCommand]
    private async Task GoToDetails(string Name)
    {
        IsBusy = true;
        await Shell.Current.GoToAsync("DetailsPage", true, new Dictionary<string, object>
        {
            {"SelectedAnimal",Name}
        });
        IsBusy = false;
    }

    [RelayCommand]
    private async Task OpenExportArt()
    {
        IsBusy = true;
        //await Application.Current.MainPage.Navigation.PushAsync(new ExportArt());
        await Shell.Current.GoToAsync("ExportArt", true);
        IsBusy = false;

    }

    [RelayCommand]
    private async Task LoadJSON()
    {        IsBusy = true;
        JSONServices MyService = new();

        await MyService.GetArts();
        MyObservableArts.Clear();

        foreach(var spider in Globals.MyArts) 
        {
            MyObservableArts.Add(spider);
        }

        IsBusy = false;
    }

}
