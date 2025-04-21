using SimpleFinances.Application.DTOs;

namespace SimpleFinances.Application.Services;
public interface ITransactionService
{
    TransactionResponse Create(CreateTransactionRequest request);
    TransactionSummaryResponse GetAll();
    void Delete(Guid id);
    void Update(Guid id, CreateTransactionRequest request);
}
