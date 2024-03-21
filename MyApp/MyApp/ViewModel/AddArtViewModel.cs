using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class AddArtViewModel : BaseViewModel
    {
        [RelayCommand]
        private async Task OnSubmitClicked()
        {
            IsBusy = true;
            //on recupere les valerus du champs et on les rajoutent a notre listes d'arts
            Art newArt = new Art();
            IsBusy = false;

        }
    }
}
