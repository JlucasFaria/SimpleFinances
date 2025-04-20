namespace SimpleFinances.Application.DTOs;
public class CreateTransactionRequest
{
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int Type { get; set; }
}
