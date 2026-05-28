using System.Linq;

namespace E2_Maui;

public partial class MainPage : ContentPage
{
	public MainPage(ViewModels.MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private async void OnMoreOptionsClicked(object sender, EventArgs e)
	{
		if (BindingContext is not ViewModels.MainViewModel viewModel)
		{
			return;
		}

		var options = viewModel.CurrencyOptions;
		var labels = options.Select(option => option.Label).ToArray();

		var selection = await DisplayActionSheet("Moneda", "Cancelar", null, labels);

		if (string.IsNullOrWhiteSpace(selection) || selection == "Cancelar")
		{
			return;
		}

		var selectedOption = options.FirstOrDefault(option => option.Label == selection);

		if (selectedOption is null)
		{
			return;
		}

		viewModel.SelectedCurrencyCulture = selectedOption.CultureName;
	}
}
