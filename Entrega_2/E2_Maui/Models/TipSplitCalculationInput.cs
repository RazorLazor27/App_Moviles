namespace E2_Maui.Models;

public sealed record TipSplitCalculationInput
{
	public decimal BillTotal { get; init; }

	public int PeopleCount { get; init; } = 1;

	public decimal TipPercentage { get; init; }
}