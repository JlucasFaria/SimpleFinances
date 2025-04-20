using SimpleFinances.Application.DTOs;

namespace SimpleFinances.Application.Services;
public interface ITransactionService
{
    TransactionResponse Create(CreateTransactionRequest request);
    TransactionSummaryResponse GetAll();
}
