namespace E2_Maui.Models;

public static class TipCalculator
{
	public static TipSplitCalculationResult Calculate(TipSplitCalculationInput input)
	{
		ArgumentNullException.ThrowIfNull(input);

		if (input.PeopleCount <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(input.PeopleCount), "PeopleCount must be greater than zero.");
		}

		var normalizedTipPercentage = ClampTipPercentage(input.TipPercentage);
		var subtotalPerPerson = RoundMoney(input.BillTotal / input.PeopleCount);
		var totalTipAmount = RoundMoney(input.BillTotal * normalizedTipPercentage / 100m);
		var tipPerPerson = RoundMoney(totalTipAmount / input.PeopleCount);
		var totalPerPerson = RoundMoney(subtotalPerPerson + tipPerPerson);
		var totalBillWithTip = RoundMoney(input.BillTotal + totalTipAmount);

		return new TipSplitCalculationResult
		{
			SubtotalPerPerson = subtotalPerPerson,
			TipPerPerson = tipPerPerson,
			TotalPerPerson = totalPerPerson,
			TotalTipAmount = totalTipAmount,
			TotalBillWithTip = totalBillWithTip
		};
	}

	public static decimal ClampTipPercentage(decimal tipPercentage)
	{
		if (tipPercentage < 0m)
		{
			return 0m;
		}

		if (tipPercentage > 50m)
		{
			return 50m;
		}

		return tipPercentage;
	}

	private static decimal RoundMoney(decimal amount)
	{
		return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
	}
}