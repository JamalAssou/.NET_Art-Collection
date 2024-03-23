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

    public void RefreshPage()
    {
        MyObservableArts.Clear();
        foreach (var art in Globals.MyArts)
        {
            MyObservableArts.Add(art);
        }
    }

    private void FillPossibleArtsCollection()
    {
        Art NewArt = new();
        NewArt.Id = 0;
        NewArt.Title = "La Joconde";
        NewArt.Description = "It depicts a half-body portrait, probably of the Florentine Lisa Gherardini";
        NewArt.Author = "Léonard de Vinci";
        NewArt.Price = "$260m";
        NewArt.Picture = "la_joconde.jpg";
        NewArt.Year = "1503-1506";

        Art NewArt1 = new();
        NewArt1.Id = 1;
        NewArt1.Title = "Les Tournesols";
        NewArt1.Description = "It depicts bouquets of sunflowers in vases";
        NewArt1.Author = "Vincent van Gogh";
        NewArt1.Price = "$14.9m";
        NewArt1.Picture = "sunflowers.jpg";
        NewArt1.Year = "1887-1889";

        Art NewArt2 = new();
        NewArt2.Id = 2;
        NewArt2.Title = "The Nightmare";
        NewArt2.Description = "It shows a woman in deep sleep with her arms thrown below her, and with a demonic and ape-like incubus crouched on her chest";
        NewArt2.Author = "Henry Fuseli";
        NewArt2.Price = "$75.9m";
        NewArt2.Picture = "the_nightmare.jpg";
        NewArt2.Year = "1781";

        Art NewArt3 = new();
        NewArt3.Id = 3;
        NewArt3.Title = "The Scream";
        NewArt3.Description = "Symbolizing the modern man swept away by a crisis of existential anguish";
        NewArt3.Author = "Edvard Munch";
        NewArt3.Price = "$119m";
        NewArt3.Picture = "the_scream.jpg";
        NewArt3.Year = "1893-1917";
        
        Art NewArt4 = new();
        NewArt4.Id = 4;
        NewArt4.Title = "Guernica";
        NewArt4.Description = "Powerful testimony to the horrors of the Spanish Civil War, expressing the ";
        NewArt4.Author = "Pablo Picasso";
        NewArt4.Price = "$200m";
        NewArt4.Picture = "guernica.jpg";
        NewArt4.Year = "1937";
        
        Art NewArt5 = new();
        NewArt5.Id = 5;
        NewArt5.Title = "La Nuit étoilée";
        NewArt5.Description = "Symbolizing the modern man swept away by a crisis of existential anguish";
        NewArt5.Author = "Vincent van Gogh";
        NewArt5.Price = "$119m";
        NewArt5.Picture = "la_nuit_etoilee.jpg";
        NewArt5.Year = "1893-1917";
        
        Art NewArt6 = new();
        NewArt6.Id = 6;
        NewArt6.Title = "Les Demoiselles d'Avignon";
        NewArt6.Description = "A revolutionary work that heralded Cubism, featuring distorted, angular figures in a brothel scene";
        NewArt6.Author = "Pablo Picasso";
        NewArt6.Price = "$200m";
        NewArt6.Picture = "les_demoiselles_davignon.jpg";
        NewArt6.Year = "1907";
        
        Art NewArt7 = new();
        NewArt7.Id = 7;
        NewArt7.Title = "La Persistance de la Mémoire";
        NewArt7.Description = "A surreal representation of soft watches in a desert landscape, exploring the concepts of time and reality";
        NewArt7.Author = "Salvador Dalí";
        NewArt7.Price = "$90m";
        NewArt7.Picture = "la_persistance_de_la_memoire.jpeg";
        NewArt7.Year = "1931";

        Art NewArt8 = new();
        NewArt8.Id = 8;
        NewArt8.Title = "Les Amoureux";
        NewArt8.Description = "An intimate and moving depiction of a couple embracing in an idyllic landscape, symbolizing love and human connection";
        NewArt8.Author = "Pierre-Auguste Renoir";
        NewArt8.Price = "$80m";
        NewArt8.Picture = "les_amoureux.jpg";
        NewArt8.Year = "1875";


        Globals.PossibleArtsCollection.Add(NewArt);
        Globals.PossibleArtsCollection.Add(NewArt1);
        Globals.PossibleArtsCollection.Add(NewArt2);
        Globals.PossibleArtsCollection.Add(NewArt3);
        Globals.PossibleArtsCollection.Add(NewArt4);
        Globals.PossibleArtsCollection.Add(NewArt5);
        Globals.PossibleArtsCollection.Add(NewArt6);
        Globals.PossibleArtsCollection.Add(NewArt7);
        Globals.PossibleArtsCollection.Add(NewArt8);
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
    }


    [RelayCommand]
    private async Task AddArt()
    {
        IsBusy = true;

        JSONServices MyService = new();

         Art NewArt = new ();
         NewArt.Title = "La Joconde";
         NewArt.Description = "It depicts a half-body portrait, probably of the Florentine Lisa Gherardini";
         NewArt.Author = "Leonardo Da Vinci";
         NewArt.Price = "$260m";
         NewArt.Picture = "la_joconde.jpg";
         NewArt.Year = "1503-1506";

         Globals.MyArts.Add(NewArt);
         MyObservableArts.Add(NewArt);

        await Shell.Current.GoToAsync("AddArt", true);

        await MyService.SetArts();

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
