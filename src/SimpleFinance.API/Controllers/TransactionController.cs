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

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _transactionService.GetAll();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _transactionService.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] CreateTransactionRequest request)
    {
        try
        {
            _transactionService.Update(id, request);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        
    }
}