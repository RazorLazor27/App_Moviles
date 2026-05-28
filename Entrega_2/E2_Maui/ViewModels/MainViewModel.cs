using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using E2_Maui.Models;

namespace E2_Maui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public sealed record CurrencyOption(string Label, string CultureName);

    public IReadOnlyList<CurrencyOption> CurrencyOptions { get; } = new List<CurrencyOption>
    {
        new("USD - Estados Unidos", "en-US"),
        new("EUR - Europa", "es-ES"),
        new("CLP - Chile", "es-CL"),
        new("ARS - Argentina", "es-AR"),
        new("PEN - Peru", "es-PE"),
        new("MXN - Mexico", "es-MX")
    };

    [RelayCommand]
    private void IncreasePeople()
    {
        int people = ParseInt(PeopleCountText);

        people++;

        PeopleCountText = people.ToString();
    }

    [RelayCommand]
    private void DecreasePeople()
    {
        int people = ParseInt(PeopleCountText);

        if (people > 1)
        {
            people--;

            PeopleCountText = people.ToString();
        }
    }

    [ObservableProperty]
    private string totalConsumptionText = "0";

    [ObservableProperty]
    private string peopleCountText = "1";

    [ObservableProperty]
    private double tipPercent = 10;

    [ObservableProperty]
    private decimal subtotalPerPerson;

    [ObservableProperty]
    private decimal tipPerPerson;

    [ObservableProperty]
    private decimal totalPerPerson;

    [ObservableProperty]
    private decimal tipAmountTotal;

    [ObservableProperty]
    private decimal totalWithTip;

    [ObservableProperty]
    private string selectedCurrencyCulture = "en-US";

    public MainViewModel()
    {
        Recalculate();
    }

    partial void OnTotalConsumptionTextChanged(string value)
    {
        Recalculate();
    }

    partial void OnPeopleCountTextChanged(string value)
    {
        Recalculate();
    }

    partial void OnTipPercentChanged(double value)
    {
        Recalculate();
    }

    [RelayCommand]
    private void ApplyPresetTip(int percent)
    {
        TipPercent = percent;
    }

    private void Recalculate()
    {
        var input = new TipSplitCalculationInput
        {
            TotalConsumption = ParseDecimal(TotalConsumptionText),
            PeopleCount = ParseInt(PeopleCountText),
            TipPercentage = (decimal)Math.Clamp(TipPercent, 0, 50)
        };

        var result = TipCalculator.Calculate(input);

        SubtotalPerPerson = result.SubtotalPerPerson;
        TipPerPerson = result.TipPerPerson;
        TotalPerPerson = result.TotalPerPerson;
        TipAmountTotal = result.TipAmountTotal;
        TotalWithTip = result.TotalWithTip;
    }

    private static decimal ParseDecimal(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return 0m;
        }

        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var result))
        {
            return Math.Max(0m, result);
        }

        return 0m;
    }

    private static int ParseInt(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return 1;
        }

        if (int.TryParse(value, NumberStyles.Integer, CultureInfo.CurrentCulture, out var result))
        {
            return Math.Max(1, result);
        }

        return 1;
    }
}
