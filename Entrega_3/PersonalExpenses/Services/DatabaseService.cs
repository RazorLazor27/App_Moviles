using SQLite;
using PersonalExpenses.Models;

namespace PersonalExpenses.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _database;

    private async Task InitAsync()
    {
        if (_database is not null)
            return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "PersonalExpenses.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        await _database.CreateTableAsync<Transaction>();
    }

    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        await InitAsync();
        return await _database!.Table<Transaction>()
                               .OrderByDescending(t => t.Date)
                               .ToListAsync();
    }

    public async Task<int> SaveTransactionAsync(Transaction transaction)
    {
        await InitAsync();
        if (transaction.Id != 0)
        {
            return await _database!.UpdateAsync(transaction);
        }
        else
        {
            return await _database!.InsertAsync(transaction);
        }
    }

    public async Task<int> DeleteTransactionAsync(Transaction transaction)
    {
        await InitAsync();
        return await _database!.DeleteAsync(transaction);
    }
}
