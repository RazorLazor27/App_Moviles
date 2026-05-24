namespace E2_Maui;

public partial class AppShell : Shell
{
	public AppShell(MainPage mainPage)
	{
		InitializeComponent();

		Items.Add(new ShellContent
		{
			Title = "Calculadora",
			Route = "MainPage",
			Content = mainPage
		});
	}
}
