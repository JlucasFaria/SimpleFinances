using SimpleFinances.Domain.Enums;

namespace SimpleFinances.Domain.Entities;
public class Transaction
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date {  get; set; }
    public TransactionType Type { get; set; }
}
