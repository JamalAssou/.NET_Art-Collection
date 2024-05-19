using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class RegisterViewModel : BaseViewModel
    {
        public ObservableCollection<UserModel> MyObservableUsers { get; } = new();

        public ObservableCollection<ArtCollectionModel> MyObservableArt { get; } = new();

        DataAccesServices DBService;
        public RegisterViewModel(DataAccesServices dBService) {

            DBService = dBService;

        }

        [ObservableProperty]
        public string email = "";
        [ObservableProperty]
        public string password = "";
        [ObservableProperty]
        public string temp = "";


        [RelayCommand]
        private async Task BackToLogin()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("LoginPage", true); // pour retourner sur la page d'accueil
            IsBusy = false;
        }


        [RelayCommand]
        private async Task Register()
        {

            // Vérifiez si les champs Email et Password sont vides
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                await Shell.Current.DisplayAlert("Error!", "Email and Password cannot be empty !", "OK");
                return;
            } else
            {
                await Shell.Current.GoToAsync("LoginPage", true); // pour retourner sur la page d'accueil
            }

            IsBusy = true;

            JSONServices MyService = new();

            var user = new UserModel {
                Email = Email,
                Password = Password
            };
            

            DBService.Users.Add(user);

            try
            {
                DBService.SaveChanges();
            }catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error!", "Error saving to database: " + ex, "OK");
            }

            // debug
            // Temp = ""+Email; 
            // Console.WriteLine(user.Email);

            IsBusy = false;

        }
    }
}
