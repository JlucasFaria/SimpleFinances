using SimpleFinances.Application.DTOs;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Enums;


namespace SimpleFinances.Application.Services;
public class TransactionService : ITransactionService
{
    private readonly List<Transaction> _transactions = new();

    public TransactionResponse Create(CreateTransactionRequest request)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Amount = request.Amount,
            Date = request.Date,
            Type = (TransactionType)request.Type,
        };

        _transactions.Add(transaction);

        return new TransactionResponse
        {
            Id = transaction.Id,
            Title = transaction.Title,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Type = transaction.Type.ToString(),
        };
    }
    public TransactionSummaryResponse GetAll()
    {
        var transactionResponse = _transactions.Select(t => new TransactionResponse
        {
            Id = t.Id,
            Title = t.Title,
            Amount = t.Amount,
            Date = t.Date,
            Type = t.Type.ToString(),
        }).ToList();

        var income = _transactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        var expense = _transactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);

        return new TransactionSummaryResponse
        {
            Transactions = transactionResponse,
            TotalBalance = income - expense
        };
    }
    public void Delete(Guid id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);

        if (transaction is null)
        {
            throw new InvalidOperationException("Transaction not found.");
        }

        _transactions.Remove(transaction);
    }
}
