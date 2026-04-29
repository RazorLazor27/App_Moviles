using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
namespace MyMauiApp;

public partial class MainPage : ContentPage
{
	private readonly Random _random = new();
	
	/// Copia el código hexadecimal al portapapeles
	/// <param name="sender">El objeto que envió el evento.</param>
	/// <param name="e">Los argumentos del evento.</param>
	private async void OnCopyHexClicked(object? sender, EventArgs e)
	{
		await Clipboard.SetTextAsync(HexLabel.Text);
		var button = sender as Button;
		if (button != null)
		{
			button.Text = "Copiado ✓";
			await Task.Delay(1500);
			button.Text = "Copiar Color";
		}
	}
	
	public MainPage()
	{
		InitializeComponent();
		ApplyColorFromSliders();
	}

	/// <summary>
	/// ACTUALMENTE NO HACE NADA
	/// </summary> <param name="sender">El objeto que envió el evento.</param> <param name="e">Los argumentos del evento.</param>
	private void OnApplyRgbClicked(object? sender, EventArgs e)
	{
		// TODO: Implementar funcionalidad especifica para el boton "Aplicar RGB".
		ApplyColorFromSliders();
	}

	/// <summary>
	/// Maneja el evento de clic del botón "Aleatorio" para generar un color aleatorio.
	/// </summary> <param name="sender">El objeto que envió el evento.</param> <param name="e">Los argumentos del evento.</param>
	private void OnRandomColorClicked(object? sender, EventArgs e)
	{
		int r = _random.Next(256);
		int g = _random.Next(256);
		int b = _random.Next(256);

		// Rslider, GSlider y Bslider son los controles de barra deslizante para Rojo, Verde y Azul.
		// Se definieron en el archivo "MainPage.xaml" y si marcan error en Visual, ignorar.
		RSlider.Value = r;
		GSlider.Value = g;
		BSlider.Value = b;

		ApplyColorFromSliders();
	}

	/// <summary>
	/// Maneja el evento de cambio de valor de los sliders para actualizar el color en tiempo real.
	/// </summary>
	/// <param name="sender">El objeto que envió el evento.</param>
	/// <param name="e">Los argumentos del evento.</param>
	private void OnSliderValueChanged(object? sender, ValueChangedEventArgs e)
	{
		ApplyColorFromSliders();
	}

	/// <summary>
	/// Aplica el color seleccionado por los sliders y actualiza la vista previa y el código hexadecimal.
	/// (Tiene que aproximar el valor de los sliders a un número entero)
	/// </summary>
	private void ApplyColorFromSliders()
	{
		int r = (int)Math.Round(RSlider.Value);
		int g = (int)Math.Round(GSlider.Value);
		int b = (int)Math.Round(BSlider.Value);

		RValueLabel.Text = r.ToString();
		GValueLabel.Text = g.ToString();
		BValueLabel.Text = b.ToString();

		SetColor(r, g, b);
	}

	/// <summary>
	/// Establece el color de fondo de la vista previa y actualiza el código hexadecimal y el color del texto según el brillo del color.
	/// 
	/// ColorPreviewGrid y Hexlabel son los controles de vista previa y etiqueta para mostrar el color y su código hexadecimal, respectivamente.
	/// Se definieron en el archivo "MainPage.xaml" y si marcan error en Visual, ignorar.
	/// </summary>
	/// <param name="r"></param>
	/// <param name="g"></param>
	/// <param name="b"></param>
	/// 
/*	private void SetColor(int r, int g, int b)
	{
		Color color = Color.FromRgb(r, g, b);
		ColorPreviewGrid.BackgroundColor = color;
		// Para cambiar el color de fondo, pero caché que si lo colocamos no se ve lindo
		this.BackgroundColor = color;
		HexLabel.Text = $"#{r:X2}{g:X2}{b:X2}";

		int brightness = (r + g + b) / 3;
		HexLabel.TextColor = brightness >= 128 ? Colors.Black : Colors.White;
	}
	*/	

	private void SetColor(int r, int g, int b)
	{
		Color color = Color.FromRgb(r, g, b);
		ColorPreviewGrid.BackgroundColor = color;
		this.BackgroundColor = color;
		HexLabel.Text = $"#{r:X2}{g:X2}{b:X2}";

		// --- AQUÍ LA MAGIA DEL STATUS BAR ---
		if (StatusBar != null)
		{
			// Aplicamos el color generado a la barra de estado
			StatusBar.StatusBarColor = color;

			// Calculamos el brillo para que los iconos (hora, batería) sean legibles
			// Si el color es claro, ponemos iconos oscuros. Si es oscuro, iconos blancos.
			double luminance = (0.299 * r + 0.587 * g + 0.114 * b) / 255;
			StatusBar.StatusBarStyle = luminance > 0.5 ? StatusBarStyle.DarkContent : StatusBarStyle.LightContent;
		}

		if (Shell.Current != null)
		{
			double luminance = (0.299 * r + 0.587 * g + 0.114 * b) / 255;

			Shell.SetBackgroundColor(Shell.Current, color);

			Shell.SetTitleColor(
				Shell.Current,
				luminance > 0.5 ? Colors.Black : Colors.White
			);
		}

	int brightness = (r + g + b) / 3;
	HexLabel.TextColor = brightness >= 128 ? Colors.Black : Colors.White;

		
	}	
}
