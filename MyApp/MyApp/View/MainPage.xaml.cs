namespace MyApp.View;

public partial class MainPage : ContentPage
{
    MainViewModel viewModel;
    public MainPage(MainViewModel viewModel)
	{
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext= viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args) // m�thode apell�e lorsque on revient en arri�re
    {
        BindingContext = null;
        viewModel.RefreshPage();  
        BindingContext = viewModel;
    }
}