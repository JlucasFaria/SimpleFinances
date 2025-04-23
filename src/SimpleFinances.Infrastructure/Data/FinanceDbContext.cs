using SimpleFinances.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleFinances.Infrastructure.Data;

public class FinanceDbContext : DbContext
{
    public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
    : base(options) { }

    public DbSet<Transaction> Transactions => Set<Transaction>();
}
