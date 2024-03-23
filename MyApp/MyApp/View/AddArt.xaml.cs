namespace MyApp.View;

public partial class AddArt : ContentPage
{
	AddArtViewModel viewModel;
	public AddArt(AddArtViewModel viewModel)
	{
		this.viewModel = viewModel;
		InitializeComponent();
		BindingContext = viewModel;
	}
}