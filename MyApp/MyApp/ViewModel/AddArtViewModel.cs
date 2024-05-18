using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using System.Security.Claims;


namespace MyApp.ViewModel
{
    public partial class AddArtViewModel : BaseViewModel
    {
       // public ObservableCollection<ArtCollectionModel> MyPersonalObservableArt { get; } = new();

        private DataAccesServices DBService;
        private int userId;

        public AddArtViewModel(DataAccesServices dBService)
        {
            DBService = dBService;
            this.userId = LoginViewModel.LoggedInUserId; // Utilisez l'ID de l'utilisateur stocké;

            //LoadUserArts();

        }


        public int cptId = 10;

        [ObservableProperty]
        public string title = "";
        [ObservableProperty]
        public string author = "";
        [ObservableProperty]
        public string year = "";
        [ObservableProperty]
        public string description = "";
        [ObservableProperty]
        public string price = "";
        [ObservableProperty]
        public string picture = "";

        [RelayCommand]
        private async Task AddArt() 
        {
            IsBusy = true;

            //ancienne methode qui permettait de rajouter les art dans un fichier Json
            /*JSONServices MyService = new();

            Art myArt = new Art();
            myArt.Id = cptId;
            myArt.Title = Title;
            myArt.Author = Author;
            myArt.Year = Year;
            myArt.Description = Description;
            myArt.Picture = Picture;
            myArt.Price = Price;
                
            cptId++;

            Globals.MyArts.Add(myArt);
            Globals.PossibleArtsCollection.Add(myArt);

            
            await MyService.SetArts();
            await MyService.SetPossibleArtsCollection();*/

            var newArt = new ArtCollectionModel
            {
                Name = Title,
                Description = Description,
                Author = Author,
                Year = Year,
                Picture = Picture,
                Price = Price,
                UserId = userId
            };

            DBService.artCollections.Add(newArt);
            DBService.SaveChanges();
            await Shell.Current.GoToAsync("//MainPage", true); // pour retourner sur la page d'accueil

            IsBusy = false;

        }

        /*private void LoadUserArts()
        {
            if (userId == 0)
            {
                // Gérer le cas où l'utilisateur n'est pas connecté ou l'ID de l'utilisateur est introuvable
                return;
            }

            List<ArtCollectionModel> userArts = DBService.artCollections
                .Where(p => p.UserId == userId)
                .ToList();

            MyPersonalObservableArt.Clear();
            foreach (var item in userArts)
            {
                MyPersonalObservableArt.Add(item);
            }
        }*/

    }
}
