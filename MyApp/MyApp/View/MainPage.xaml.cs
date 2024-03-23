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

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = null;
        viewModel.RefreshPage();  //a developper
        BindingContext = viewModel;
    }
}