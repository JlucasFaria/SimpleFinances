namespace SimpleFinances.Application.DTOs;
public class TransactionSummaryResponse
{
    public decimal TotalBalance { get; set; }
    public List<TransactionResponse> Transactions { get; set; } = new();
}
