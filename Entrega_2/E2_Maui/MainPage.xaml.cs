namespace E2_Maui;

public partial class MainPage : ContentPage
{
	public MainPage(ViewModels.MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
