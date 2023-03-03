using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
namespace Infra.Repositories;

public class TransactionRepository : GenericRepository<TransactionEntity>, ITransactionRepository
{
    public TransactionRepository(IServiceProvider provider) : base(provider)
    {
    }
}
