using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SimpleFinances.Application.Services;
using SimpleFinances.Application.Validators;
using SimpleFinances.Infrastructure.Data;

namespace SimpleFinances;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddValidatorsFromAssemblyContaining<CreateTransactionRequestValidator>();
        builder.Services.AddFluentValidationAutoValidation();

        // Swagger (documentação e testes da API)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Banco de dados SQLite (usando EF Core)
        builder.Services.AddDbContext<FinanceDbContext>(options =>
            options.UseSqlite("Data Source=finances.db"));
        
        // Registro do serviço com EF Core
        builder.Services.AddScoped<ITransactionService, TransactionEfService>();

        var app = builder.Build();

        // Ativa Swagger apenas em ambiente de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
