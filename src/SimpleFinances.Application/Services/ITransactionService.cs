using SimpleFinances.Application.DTOs;

namespace SimpleFinances.Application.Services;
public interface ITransactionService
{
    TransactionResponse Create(CreateTransactionRequest request);
    TransactionSummaryResponse GetAll(
        string? type = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? min = null,
        decimal? max = null
      );

   
    void Delete(Guid id);
    void Update(Guid id, CreateTransactionRequest request);
}
