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
    DeviceOrientationService myScanner;

    [ObservableProperty]
    string? codeBar;
    public MainViewModel()
    {
        this.myScanner = new DeviceOrientationService();
        myScanner.ConfigureScanner();
        myScanner.SerialBuffer.Changed += OnBarCodeScanned;
        LoadPossibleArtsCollection(); //On charge les elements qui peuvent etre scanné depuis le fichier Json 
    }

    private async Task LoadPossibleArtsCollection()
    {
        
        JSONServices MyService = new();

        await MyService.GetPossibleArtsCollection();//la methode qui recupere les element depuis le fichier json
        
        //debug
        foreach (var art in Globals.PossibleArtsCollection)
        {
            CodeBar += art.Id + ";";
        }

       
    }

    public void RefreshPage()
    {
        MyObservableArts.Clear();
        foreach (var art in Globals.MyArts)
        {
            MyObservableArts.Add(art);
        }
    }


    private async void OnBarCodeScanned(object sender, EventArgs args)
    {
        DeviceOrientationService.QueueBuffer MyLocalBuffer = (DeviceOrientationService.QueueBuffer)sender;

        
        CodeBar = MyLocalBuffer.Dequeue().ToString();
        JSONServices MyService = new();


        foreach (var Art in Globals.PossibleArtsCollection)
        {
            
            if (CodeBar.Equals(Art.Id.ToString()))
            {
                Globals.MyArts.Add(Art);
                await MyService.SetArts();
                MyObservableArts.Add(Art);
            }
        }
    }


    [RelayCommand]
    private async Task AddArt()
    {
        IsBusy = true;
        await Shell.Current.GoToAsync("AddArt", true);
        IsBusy = false;
    }


    [RelayCommand]
    private async Task GoToDetails(string Title)
    {
        IsBusy = true;
        await Shell.Current.GoToAsync("DetailsPage", true, new Dictionary<string, object>
        {
            {"SelectedArt",Title}
        });
        IsBusy = false;
    }


    [RelayCommand]
    private async Task OpenExportArt()
    {
        IsBusy = true;
        await Shell.Current.GoToAsync("ExportArt", true);
        IsBusy = false;

    }


    [RelayCommand]
    private async Task LoadJSON()
    {   
        IsBusy = true;
        JSONServices MyService = new();

        await MyService.GetArts();
        MyObservableArts.Clear();

        foreach(var art in Globals.MyArts) 
        {
            MyObservableArts.Add(art);
        }

        IsBusy = false;
    }

}
