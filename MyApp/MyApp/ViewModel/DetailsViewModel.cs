using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel;

[QueryProperty(nameof(ArtTitle), "SelectedArtTitle")]
// [QueryProperty(nameof(ArtPrice), "SelectedArtPrice")]
public partial class DetailsViewModel: BaseViewModel
{
    [ObservableProperty]
    string? artTitle;
    /*[ObservableProperty]
    string? artPrice;
    [ObservableProperty]
    string? artDescription;
    [ObservableProperty]
    string? artAuthor;
    [ObservableProperty]
    string? artYear;*/

}
