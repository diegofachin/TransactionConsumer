using Domain.Interfaces;
using Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PurchaseAppDbContext _context;
    public ITransactionRepository TransactionRepository { get; }

    public UnitOfWork(PurchaseAppDbContext context, ITransactionRepository transactionRepository)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        TransactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    }

    public int Commit()
    {
        return _context.SaveChanges();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
