using Microsoft.Extensions.Logging;
using PersonalExpenses.Services;
using PersonalExpenses.ViewModels;
using PersonalExpenses.Views;

namespace PersonalExpenses;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		#if DEBUG
		builder.Logging.AddDebug();
		#endif

		// Register Services
		builder.Services.AddSingleton<DatabaseService>();

		// Register ViewModels
		builder.Services.AddTransient<MainViewModel>();

		// Register Views
		builder.Services.AddTransient<MainPage>();

		return builder.Build();
	}
}
