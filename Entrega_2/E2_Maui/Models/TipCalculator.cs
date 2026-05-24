namespace E2_Maui.Models;

public sealed class TipSplitCalculationInput
{
    public decimal TotalConsumption { get; set; }
    public int PeopleCount { get; set; }
    public decimal TipPercentage { get; set; }
}

public sealed class TipSplitCalculationResult
{
    public decimal TipAmountTotal { get; init; }
    public decimal TotalWithTip { get; init; }
    public decimal SubtotalPerPerson { get; init; }
    public decimal TipPerPerson { get; init; }
    public decimal TotalPerPerson { get; init; }
}

public static class TipCalculator
{
    public static TipSplitCalculationResult Calculate(TipSplitCalculationInput input)
    {
        var people = Math.Max(1, input.PeopleCount);
        var tipRate = Math.Clamp(input.TipPercentage, 0m, 50m) / 100m;
        var total = Math.Max(0m, input.TotalConsumption);

        var tipTotal = total * tipRate;
        var totalWithTip = total + tipTotal;

        var subtotalPerPerson = total / people;
        var tipPerPerson = tipTotal / people;
        var totalPerPerson = totalWithTip / people;

        return new TipSplitCalculationResult
        {
            TipAmountTotal = tipTotal,
            TotalWithTip = totalWithTip,
            SubtotalPerPerson = subtotalPerPerson,
            TipPerPerson = tipPerPerson,
            TotalPerPerson = totalPerPerson
        };
    }
}
