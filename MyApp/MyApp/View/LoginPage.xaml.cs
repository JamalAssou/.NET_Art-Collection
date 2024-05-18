namespace MyApp.View;

public partial class LoginPage : ContentPage
{
    LoginViewModel viewModel;
    public LoginPage(LoginViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }
}