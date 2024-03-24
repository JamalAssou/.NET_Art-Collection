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

        public ImageSource _selectedImage;

        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set => SetProperty(ref _selectedImage, value);
        }

       /* private string _selectedImageFileName;
        * 
        public string SelectedImageFileName
        {
            get => _selectedImageFileName;
            set => SetProperty(ref _selectedImageFileName, value);
        }*/


        [RelayCommand]
        private async Task AddArtFinal()
        {
            IsBusy = true;

            JSONServices MyService = new();

            Art myArt = new Art();
            myArt.Title = Title;
            myArt.Author = Author;
            myArt.Year = Year;
            myArt.Description = Description;
            myArt.SelectedImage = SelectedImage;
            // myArt.ImageFileName = SelectedImageFileName;
            myArt.Price = Price;

            Globals.MyArts.Add(myArt);

            await MyService.SetArts();

            await Shell.Current.GoToAsync("//MainPage", true); //pour naviguer dans une autre fenetre

            IsBusy = false;

        }


        // OPEN FOLDER
        [RelayCommand]
        private async Task ImportImage()
        {
            try
            {
                var results = await FilePicker.PickMultipleAsync();

                if (results != null && results.Any())
                {
                    foreach (var result in results)
                    {
                        var stream = await result.OpenReadAsync();
                        using var memoryStream = new MemoryStream();
                        await stream.CopyToAsync(memoryStream);
                        SelectedImage = ImageSource.FromStream(() => memoryStream);
                        SelectedImage = result.FileName;


                        // DEBUG
                        string fileName = result.FileName;
                        await Shell.Current.DisplayAlert(":)", $"Selected image file name: {fileName}", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}
