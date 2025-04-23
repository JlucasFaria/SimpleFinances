using SimpleFinances.Application.DTOs;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Enums;
using SimpleFinances.Infrastructure.Data;

namespace SimpleFinances.Application.Services;


public class TransactionEfService : ITransactionService
{
    private readonly FinanceDbContext _context;

    public TransactionEfService(FinanceDbContext context)
    {
        _context = context;
    }

    public TransactionResponse Create(CreateTransactionRequest request)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Amount = request.Amount,
            Date = request.Date,
            Type = (TransactionType)request.Type
        };

        _context.Transactions.Add(transaction);
        _context.SaveChanges();

        return new TransactionResponse
        {
            Id = transaction.Id,
            Title = transaction.Title,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Type = transaction.Type.ToString()
        };
    }

    public TransactionSummaryResponse GetAll(string? type, DateTime? startDate, DateTime? endDate, decimal? min, decimal? max)
    {
        throw new NotImplementedException(); // você pode implementar depois
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    void ITransactionService.Update(Guid id, CreateTransactionRequest request)
    {
        throw new NotImplementedException();
    }
}

