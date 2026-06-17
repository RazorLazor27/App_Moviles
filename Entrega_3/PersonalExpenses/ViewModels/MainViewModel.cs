using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PersonalExpenses.Models;
using PersonalExpenses.Services;

namespace PersonalExpenses.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private string _descriptionInput = string.Empty;

    [ObservableProperty]
    private string _amountInput = string.Empty;

    [ObservableProperty]
    private bool _isExpense = true;

    [ObservableProperty]
    private decimal _totalBalance;

    [ObservableProperty]
    private decimal _totalIncome;

    [ObservableProperty]
    private decimal _totalExpense;

    public ObservableCollection<Transaction> Transactions { get; } = new();

    public MainViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [RelayCommand]
    public async Task LoadTransactionsAsync()
    {
        var list = await _databaseService.GetTransactionsAsync();
        
        Transactions.Clear();
        decimal income = 0;
        decimal expense = 0;

        foreach (var t in list)
        {
            Transactions.Add(t);
            if (t.Type == TransactionType.Income)
            {
                income += t.Amount;
            }
            else
            {
                expense += t.Amount;
            }
        }

        TotalIncome = income;
        TotalExpense = expense;
        TotalBalance = income - expense;
    }

    [RelayCommand]
    public async Task AddTransactionAsync()
    {
        if (string.IsNullOrWhiteSpace(DescriptionInput))
        {
            await ShowAlertAsync("Error", "Por favor, ingresa una descripción.", "OK");
            return;
        }

        if (!decimal.TryParse(AmountInput, out decimal amount) || amount <= 0)
        {
            await ShowAlertAsync("Error", "Por favor, ingresa un monto válido mayor a cero.", "OK");
            return;
        }

        var transaction = new Transaction
        {
            Description = DescriptionInput.Trim(),
            Amount = amount,
            Type = IsExpense ? TransactionType.Expense : TransactionType.Income,
            Date = DateTime.Now
        };

        await _databaseService.SaveTransactionAsync(transaction);

        // Limpiar campos
        DescriptionInput = string.Empty;
        AmountInput = string.Empty;

        // Recargar datos
        await LoadTransactionsAsync();
    }

    [RelayCommand]
    private void SetType(string type)
    {
        IsExpense = (type == "Expense");
    }

    [RelayCommand]
    public async Task DeleteTransactionAsync(Transaction transaction)
    {
        if (transaction == null) return;

        bool confirm = await ShowConfirmAsync("Confirmar", "¿Deseas eliminar esta transacción?", "Sí", "No");
        if (confirm)
        {
            await _databaseService.DeleteTransactionAsync(transaction);
            await LoadTransactionsAsync();
        }
    }

    private async Task ShowAlertAsync(string title, string message, string cancel)
    {
        if (App.Current?.Windows.Count > 0 && App.Current.Windows[0].Page is Page page)
        {
            await page.DisplayAlertAsync(title, message, cancel);
        }
    }

    private async Task<bool> ShowConfirmAsync(string title, string message, string accept, string cancel)
    {
        if (App.Current?.Windows.Count > 0 && App.Current.Windows[0].Page is Page page)
        {
            return await page.DisplayAlertAsync(title, message, accept, cancel);
        }
        return false;
    }
}
