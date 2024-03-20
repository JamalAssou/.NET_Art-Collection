using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace MyApp.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    public ObservableCollection<Spider> MyObservableSpiders { get; } = new();
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
    private async Task AddElement()
    {
        IsBusy = true;

        JSONServices MyService = new();

        Spider NewSpider = new ();
        NewSpider.Name = "je nveaui";
        NewSpider.Description = "hello";
        NewSpider.Origin = "Belgique";
        NewSpider.Picture = "scorpion.jpg";
       
        Globals.MySpiders.Add(NewSpider);
        MyObservableSpiders.Add(NewSpider);

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
    private async Task LoadJSON()
    {        IsBusy = true;
        JSONServices MyService = new();

        await MyService.GetSpiders();
        MyObservableSpiders.Clear();

        foreach(var spider in Globals.MySpiders) 
        {
            MyObservableSpiders.Add(spider);
        }

        IsBusy = false;
    }

}
