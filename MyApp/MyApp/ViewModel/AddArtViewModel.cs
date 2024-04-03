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

namespace MyApp.ViewModel
{
    public partial class AddArtViewModel : BaseViewModel
    {
        int cptId = 9;

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
        private async Task AddArtFinal()
        {
            IsBusy = true;

            JSONServices MyService = new();

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
            await MyService.SetPossibleArtsCollection();

            await Shell.Current.GoToAsync("//MainPage", true); //pour naviguer dans une autre fenetre

            IsBusy = false;

        }

    }
}
