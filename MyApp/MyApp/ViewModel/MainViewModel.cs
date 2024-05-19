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

    private readonly DataAccesServices DBService;
    [ObservableProperty]
    private int userId;

    [ObservableProperty]
    string? codeBar;

    public MainViewModel(DataAccesServices dBService)
    {
        this.myScanner = new DeviceOrientationService();
        myScanner.ConfigureScanner();
        myScanner.SerialBuffer.Changed += OnBarCodeScanned;
        LoadPossibleArtsCollection(); // On charge les elements qui peuvent etre scanné depuis le fichier Json 

        //on recuper l'id de l'user connecté
        DBService = dBService;
        LoadUserArts();
    }

    // méthode pour supprimer tous les enregistrements des arts du compte
     /* private void DeleteUserArts()
    {
        if (UserId != 0)
        {
            DBService.DeleteUserArts(UserId);
        }
    }*/

    private async Task LoadPossibleArtsCollection()
    {      
        JSONServices MyService = new();

        await MyService.GetPossibleArtsCollection(); // la méthode qui récupere les élements depuis le fichier json
        
        /*debug
        foreach (var art in Globals.PossibleArtsCollection)
        {
            CodeBar += art.Id + ";";
        }*/  
    }

    public void RefreshPage()
    {
        MyObservableArts.Clear();
        foreach (var art in Globals.MyArts)
        {
            MyObservableArts.Add(art);
        }

        // DeleteUserArts(); // -> pour supprimer tout les art de l'user connecter.

        UserId = LoginViewModel.LoggedInUserId; // Utilisez l'ID de l'utilisateur stocké;-> on le rajoute ici sinon l'id vaudra toujours 0 car il sera appaler uniquement dans le constructeur
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

                var newArt = new ArtCollectionModel
                {
                    Name = Art.Title,
                    Description = Art.Description,
                    Author = Art.Author,
                    Year = Art.Year,
                    Picture = Art.Picture,
                    Price = Art.Price,
                    UserId = UserId
                };

                DBService.artCollections.Add(newArt);
                DBService.SaveChanges();

                MyObservableArts.Add(Art);
            }
        }

    }


    [RelayCommand]
    private async Task AddArt()
    {
        IsBusy = true;
        UserId = LoginViewModel.LoggedInUserId; // Utilisez l'ID de l'utilisateur stocké

        if (UserId != 0)
        {
            await Shell.Current.GoToAsync("AddArt", true);
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "You must be logged in to add art", "OK");

        }
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

        if (MyObservableArts.Count != 0)
        {
            await Shell.Current.GoToAsync("ExportArt", true);
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "There is no art to export", "OK");

        }

        IsBusy = false;
    }


    [RelayCommand]
    private async Task GoToStatistics()
    {
        IsBusy = true;
        if (MyObservableArts.Count != 0)
        {
            await Shell.Current.GoToAsync("Statistics", true);
        }else
        {
            await Shell.Current.DisplayAlert("Error!", "No statistic to display because there's no art", "OK");

        }

        IsBusy = false;
    }

    
    [RelayCommand]
    private async Task LoginCall()
    {
        IsBusy = true;

        await Shell.Current.GoToAsync("LoginPage", true);

        IsBusy = false;
    }


    [RelayCommand]
    private async Task LoadJSON()
    {   
        IsBusy = true;
        // JSONServices MyService = new();

        // await MyService.GetArts();
        MyObservableArts.Clear();

        UserId = LoginViewModel.LoggedInUserId; // Utilisez l'ID de l'utilisateur stocké

        if (UserId != 0)
        {
            LoadUserArts(); //on appel la methode ici car on veut qu'a chaque retour sur la page, les nouvelle element apparaissent 
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "You must be logged in to view your art", "OK");

        }

        foreach (var art in Globals.MyArts) 
        {
            MyObservableArts.Add(art);
        }

        IsBusy = false;
    }

    private void LoadUserArts()
    {
        if (UserId == 0)
        {
            return;
        }

        List<ArtCollectionModel> userArts = DBService.artCollections
            .Where(p => p.UserId == UserId)
            .ToList();

        MyObservableArts.Clear();
        foreach (var item in userArts)
        {
            MyObservableArts.Add(new Art
            {
                Id = item.Id_Art,
                Title = item.Name,
                Description = item.Description,
                Picture = item.Picture,
                Price = item.Price,
                Author = item.Author,
                Year = item.Year
            });
        }
    }

}
