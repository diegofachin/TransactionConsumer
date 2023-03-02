using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public class PurchaseAppDbContext : DbContext
{
    public PurchaseAppDbContext(DbContextOptions<PurchaseAppDbContext> options) : base(options)
    {

    }

    public DbSet<TransactionEntity> Transaction { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PurchaseAppDbContext).Assembly);
    }
}
