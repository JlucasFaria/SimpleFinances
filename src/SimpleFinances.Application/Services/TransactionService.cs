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

        return new TransactionResponse
        {
            Id = transaction.Id,
            Title = transaction.Title,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Type = transaction.Type.ToString(),
        };
    }
}
