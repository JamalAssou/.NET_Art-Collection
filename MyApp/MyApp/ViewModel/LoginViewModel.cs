using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        DataAccesServices DBService;

        public LoginViewModel(DataAccesServices dBService)
        {
            DBService = dBService;
        }

        [ObservableProperty]
        public string email = "";
        [ObservableProperty]
        public string password = "";


        public static int LoggedInUserId { get; private set; }

        [RelayCommand]
        private async Task Login()
        {
            IsBusy = true;

            var user = DBService.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user != null)
            {
                // Stockez l'ID de l'utilisateur connecté
                LoggedInUserId = user.Id_User;

                // Redirigez vers la page d'accueil
                await Shell.Current.GoToAsync("//MainPage", true);
            }
            else
            {
                // Affichez un message d'erreur
                await Shell.Current.DisplayAlert("Error!", "Invalid email or password", "OK");
            }

            IsBusy = false;
        }


        //RegisterPage Call
        [RelayCommand]
        private async Task RegisterCall()
        {
            IsBusy = true;

            await Shell.Current.GoToAsync("RegisterPage", true);

            IsBusy = false;
        }
    }
}
