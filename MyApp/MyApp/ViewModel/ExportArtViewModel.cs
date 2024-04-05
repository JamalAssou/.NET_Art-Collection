using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyApp.ViewModel
{
    public partial class ExportArtViewModel : BaseViewModel
    {
        [ObservableProperty]
        public bool checkBox_Id; // On met en miniscule ici quand on l'observe et plus tard on le met en majuscule
        [ObservableProperty]
        public bool checkBox_Title;
        [ObservableProperty]
        public bool checkBox_Description;
        [ObservableProperty]
        public bool checkBox_Author;
        [ObservableProperty]
        public bool checkBox_Price;
        [ObservableProperty]
        public bool checkBox_Year;
        public ExportArtViewModel()
        { }

        [RelayCommand]
        private async Task ExportCSV()
        {
            IsBusy = true;

            String txt = "";

            foreach (var art in Globals.MyArts)
            {
                if (CheckBox_Id) txt += art.Id + ";"; // ici on commence par une majuscule
                if (CheckBox_Title) txt += art.Title + ";";
                if (CheckBox_Description) txt += art.Description + ";";
                if (CheckBox_Author) txt += art.Author + ";";
                if (CheckBox_Price) txt += art.Price + ";";
                if (CheckBox_Year) txt += art.Year + ";";
                txt += ";\r\n";

                txt.Replace(";;", "");

                IsBusy = false;
            }

            using var stream = new MemoryStream(Encoding.Unicode.GetBytes(txt));

            CancellationToken cancellationToken = CancellationToken.None; // Déclaration et initialisation du jeton d'annulation
            var fileSaverResult = await FileSaver.Default.SaveAsync("Export.csv", stream, cancellationToken);


            if (!fileSaverResult.IsSuccessful)
            {
                await Shell.Current.DisplayAlert("Export", "Export failed...", "OK");
            }
          
        }

    }

}
