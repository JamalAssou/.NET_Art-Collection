namespace MyApp.View;

public partial class DbPage : ContentPage
{
    DBViewModel viewModel;
    public DbPage(DBViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}