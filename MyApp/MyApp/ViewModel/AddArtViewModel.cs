using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyApp.ViewModel
{
    public partial class AddArtViewModel : BaseViewModel
    {

        [ObservableProperty]
        public string title = "";
        [ObservableProperty]
        public string author = "";
        [ObservableProperty]
        public string year = "";
        [ObservableProperty]
        public string description = "";
        [ObservableProperty]
        public string picture = "";
        [ObservableProperty]
        public string price = "";

        [RelayCommand]
        private async Task AddArtFinal()
        {
            IsBusy = true;
            Art myArt = new Art();
            myArt.Title = Title;
            myArt.Author = Author;
            myArt.Year = Year;
            myArt.Description = Description;
            myArt.Picture = Picture;
            myArt.Price = Price;


            Globals.MyArts.Add(myArt);
            IsBusy = false;

            //await Shell.Current.GoToAsync("ExportArt", true);//pour naviguer dans une autre fenetre

        }
    }
}
