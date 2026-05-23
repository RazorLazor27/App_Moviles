using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using E2_Maui.Models;

namespace E2_Maui.ViewModels;

public partial class MainViewModel : ObservableObject
{
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(CanCalculate))]
	[NotifyCanExecuteChangedFor(nameof(RecalculateCommand))]
	private decimal billTotal;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(CanCalculate))]
	[NotifyCanExecuteChangedFor(nameof(RecalculateCommand))]
	private int peopleCount = 1;

	[ObservableProperty]
	private decimal selectedTipPercentage = 15m;

	[ObservableProperty]
	private decimal freeTipPercentage = 15m;

	[ObservableProperty]
	private decimal subtotalPerPerson;

	[ObservableProperty]
	private decimal tipPerPerson;

	[ObservableProperty]
	private decimal totalPerPerson;

	[ObservableProperty]
	private decimal totalTipAmount;

	[ObservableProperty]
	private decimal totalBillWithTip;

	public bool CanCalculate => BillTotal > 0m && PeopleCount > 0;

	partial void OnFreeTipPercentageChanged(decimal value)
	{
		SelectedTipPercentage = value;
	}

	[RelayCommand]
	private void UpdateTip(decimal tipPercentage)
	{
		SelectedTipPercentage = tipPercentage;
		FreeTipPercentage = tipPercentage;
	}

	[RelayCommand(CanExecute = nameof(CanCalculate))]
	private void Recalculate()
	{
		var result = TipCalculator.Calculate(new TipSplitCalculationInput
		{
			BillTotal = BillTotal,
			PeopleCount = PeopleCount,
			TipPercentage = SelectedTipPercentage
		});

		SubtotalPerPerson = result.SubtotalPerPerson;
		TipPerPerson = result.TipPerPerson;
		TotalPerPerson = result.TotalPerPerson;
		TotalTipAmount = result.TotalTipAmount;
		TotalBillWithTip = result.TotalBillWithTip;
	}

	[RelayCommand]
	private void Reset()
	{
		BillTotal = 0m;
		PeopleCount = 1;
		SelectedTipPercentage = 15m;
		FreeTipPercentage = 15m;
		SubtotalPerPerson = 0m;
		TipPerPerson = 0m;
		TotalPerPerson = 0m;
		TotalTipAmount = 0m;
		TotalBillWithTip = 0m;
	}
}