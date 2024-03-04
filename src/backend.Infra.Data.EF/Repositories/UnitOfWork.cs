using BackEnd.Domain.SeedWork;
using BackEnd.Infra.Data.EF.Context;

namespace BackEnd.Infra.Data.EF.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly PgDbContext _context;
    private IRepository? _repositorio; 

    public UnitOfWork(PgDbContext context)
        => _context = context;
    

    public IRepository Repository {
        get {
            return _repositorio = _repositorio ?? new Repository(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
