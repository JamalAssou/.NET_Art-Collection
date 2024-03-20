namespace MyApp.View;

public partial class DetailsPage : ContentPage
{
    DetailsViewModel viewModel;
    public DetailsPage(DetailsViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}