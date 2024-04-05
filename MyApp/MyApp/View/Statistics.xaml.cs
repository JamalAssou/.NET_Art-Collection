namespace MyApp.View;

public partial class Statistics : ContentPage
{
    StatisticsViewModel viewModel;

    public Statistics(StatisticsViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}