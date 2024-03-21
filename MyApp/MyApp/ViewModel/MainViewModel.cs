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
        FillPossibleArtsCollection();
    }

    private void FillPossibleArtsCollection()
    {
        Art NewArt = new();
        NewArt.Id = 0;
        NewArt.Name = "Joconte";
        NewArt.Description = "The best work of art in the world";
        NewArt.Author = "Isma";
        NewArt.Price = "1000";
        NewArt.Picture = "la_joconde.jpg";
        NewArt.Date = "2003";

        Art NewArt1 = new();
        NewArt1.Id = 1;
        NewArt1.Name = "Tournesol";
        NewArt1.Description = "The best work of art in the world";
        NewArt1.Author = "Van Gogh";
        NewArt1.Price = "1000";
        NewArt1.Picture = "tournesol.jpg";
        NewArt1.Date = "2003";

        Art NewArt2 = new();
        NewArt2.Id = 2;
        NewArt2.Name = "The Nightmare";
        NewArt2.Description = "The best work of art in the world";
        NewArt2.Author = "Jamal";
        NewArt2.Price = "1000";
        NewArt2.Picture = "the_nightmare.jpg";
        NewArt2.Date = "2003";

        Art NewArt3 = new();
        NewArt3.Id = 3;
        NewArt3.Name = "The Scream";
        NewArt3.Description = "The best work of art in the world";
        NewArt3.Author = "Farouk";
        NewArt3.Price = "10504";
        NewArt3.Picture = "the_scream.jpg";
        NewArt3.Date = "2003";

        Globals.PossibleArtsCollection.Add(NewArt);
        Globals.PossibleArtsCollection.Add(NewArt1);
        Globals.PossibleArtsCollection.Add(NewArt2);
        Globals.PossibleArtsCollection.Add(NewArt3);
    }

    private void OnBarCodeScanned(object sender, EventArgs args)
    {
        DeviceOrientationService.QueueBuffer MyLocalBuffer = (DeviceOrientationService.QueueBuffer)sender;

        
        CodeBar = MyLocalBuffer.Dequeue().ToString();


        foreach (var Art in Globals.PossibleArtsCollection)
        {
            
            if (CodeBar.Equals(Art.Id.ToString()))
            {
                Globals.MyArts.Add(Art);
                MyObservableArts.Add(Art);
            }
        }

        /*if (codeBar.Equals("1"))
        {
            Art NewArt = new();
            NewArt.Name = "Jokante";
            NewArt.Description = "The best work of art in the world";
            NewArt.Author = "Jamal";
            NewArt.Picture = "la_joconde.jpg";

            Globals.MyArts.Add(NewArt);
            MyObservableArts.Add(NewArt);
        }*/

    }


    [RelayCommand]
    private async Task AddArt()
    {
        IsBusy = true;

        JSONServices MyService = new();

        /* Art NewArt = new ();
         NewArt.Name = "Jokante";
         NewArt.Description = "The best work of art in the world";
         NewArt.Author = "Jamal";
         NewArt.Price = "4507";
         NewArt.Picture = "la_joconde.jpg";
         NewArt.Date = "2003";

         Globals.MyArts.Add(NewArt);
         MyObservableArts.Add(NewArt);*/

        await Shell.Current.GoToAsync("AddArt", true);

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

        foreach(var art in Globals.MyArts) 
        {
            MyObservableArts.Add(art);
        }

        IsBusy = false;
    }

}
