namespace MyApp.View;

public partial class AddArt : ContentPage
{
	AddArtViewModel viewModel;
	public AddArt()
	{
		this.viewModel = viewModel;
		InitializeComponent();
		BindingContext = viewModel;
	}
}