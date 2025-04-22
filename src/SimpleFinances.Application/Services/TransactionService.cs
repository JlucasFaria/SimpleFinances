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

    public TransactionSummaryResponse GetAll(
    string? type,
    DateTime? startDate,
    DateTime? endDate,
    decimal? min,
    decimal? max)
    {
        var query = _transactions.AsQueryable();

        // Filtro por tipo
        if (!string.IsNullOrWhiteSpace(type))
        {
            var typeNormalized = type.Trim().ToLower();
            query = typeNormalized switch
            {
                "income" => query.Where(t => t.Type == TransactionType.Income),
                "expense" => query.Where(t => t.Type == TransactionType.Expense),
                _ => Enumerable.Empty<Transaction>().AsQueryable()
            };
        }

        // Filtros adicionais
        if (startDate.HasValue)
            query = query.Where(t => t.Date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(t => t.Date <= endDate.Value);

        if (min.HasValue)
            query = query.Where(t => t.Amount >= min.Value);

        if (max.HasValue)
            query = query.Where(t => t.Amount <= max.Value);

        // Lista final após filtros
        var filteredList = query.ToList();

        var response = new TransactionSummaryResponse
        {
            Transactions = filteredList.Select(t => new TransactionResponse
            {
                Id = t.Id,
                Title = t.Title,
                Amount = t.Amount,
                Date = t.Date,
                Type = t.Type.ToString()
            }).ToList(),

            TotalBalance = filteredList
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount)
                -
                filteredList
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount)
        };

        return response;
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
    public void Update(Guid id, CreateTransactionRequest request)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);

        if (transaction is null)
        {
            throw new InvalidOperationException("Transaction not found.");
        }

        transaction.Title = request.Title;
        transaction.Amount = request.Amount;
        transaction.Date = request.Date;
        transaction.Type = (TransactionType)request.Type;
    }
}
