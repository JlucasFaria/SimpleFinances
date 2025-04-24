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
        var query = _context.Transactions.AsQueryable();

        if (!string.IsNullOrWhiteSpace(type) && Enum.TryParse<TransactionType>(type, true, out var parsedType))
            query = query.Where(t => t.Type == parsedType);

        if (startDate.HasValue)
            query = query.Where(t => t.Date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(t => t.Date <= endDate.Value);

        if (min.HasValue)
            query = query.Where(t => t.Amount >= min.Value);

        if (max.HasValue)
            query = query.Where(t => t.Amount <= max.Value);

        var list = query.ToList();

        var income = list.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
        var expense = list.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

        return new TransactionSummaryResponse
        {
            Transactions = list.Select(t => new TransactionResponse
            {
                Id = t.Id,
                Title = t.Title,
                Amount = t.Amount,
                Date = t.Date,
                Type = t.Type.ToString()
            }).ToList(),
            TotalBalance = income - expense
        };
    }

    public void Delete(Guid id)
    {
        var transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);
        if (transaction is null) return;

        _context.Transactions.Remove(transaction);
        _context.SaveChanges();
    }

    void ITransactionService.Update(Guid id, CreateTransactionRequest request)
    {
        var transaction = _context.Transactions.FirstOrDefault(t =>t.Id == id);
        if (transaction is null) return;

        transaction.Title = request.Title;
        transaction.Amount = request.Amount;
        transaction.Date = request.Date;
        transaction.Type = (TransactionType)request.Type;

        _context.SaveChanges();
    }
}

