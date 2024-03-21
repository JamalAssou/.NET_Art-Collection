namespace MyApp.View;

public partial class ExportArt : ContentPage
{
	ExportArtViewModel viewModel;
	public ExportArt(ExportArtViewModel viewModel)
	{
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}