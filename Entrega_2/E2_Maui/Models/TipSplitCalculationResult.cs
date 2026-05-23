namespace E2_Maui.Models;

public sealed record TipSplitCalculationResult
{
	public decimal SubtotalPerPerson { get; init; }

	public decimal TipPerPerson { get; init; }

	public decimal TotalPerPerson { get; init; }

	public decimal TotalTipAmount { get; init; }

	public decimal TotalBillWithTip { get; init; }
}