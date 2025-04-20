using Microsoft.AspNetCore.Mvc;
using SimpleFinances.Application.DTOs;
using SimpleFinances.Application.Services;

namespace SimpleFinances.API.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTransactionRequest request)
    {
        var result = _transactionService.Create(request);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}