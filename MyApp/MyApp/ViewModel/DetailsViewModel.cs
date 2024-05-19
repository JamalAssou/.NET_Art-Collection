using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MyApp.ViewModel;

[QueryProperty(nameof(ArtTitle), "SelectedArtTitle")]
public partial class DetailsViewModel: BaseViewModel
{
    [ObservableProperty]
    string? artTitle;

}
