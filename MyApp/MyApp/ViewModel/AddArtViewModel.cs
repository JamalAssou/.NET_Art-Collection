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
   
        [RelayCommand]
        private async Task OnSubmitClicked()
        {
            IsBusy = true;
            IsBusy = false;
        }
    }
}
