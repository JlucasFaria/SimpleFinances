using SimpleFinances.Application.Services;

namespace SimpleFinances;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Swagger (documentação e testes da API)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Serviço de transações (em memória por enquanto)
        builder.Services.AddSingleton<ITransactionService, TransactionService>();

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
