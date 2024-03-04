using BackEnd.Domain.SeedWork;
using BackEnd.Infra.Data.EF.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infra.Data.EF.Repositories;

public class RepositoryDapper : IRepositoryDapper
{
    private readonly PgDbContext _context;

    public RepositoryDapper(PgDbContext context)
        => _context = context;

    public async Task<IEnumerable<T>> GetAll<T>(string query)
    {
        return await _context.Database.GetDbConnection().QueryAsync<T>(query);
    }

    public async Task<T> GetById<T>(Guid id, string query, object parameters)
    {
        var command = new CommandDefinition(query, parameters);
        var objectResult = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<T>(command);
        if(objectResult is null)
            return default!;

        return objectResult;
    }

    public async Task<IEnumerable<T>> GetByIdList<T>(Guid id, string query, object parameters)
    {
        var command = new CommandDefinition(query, parameters);
        var objectResult = await _context.Database.GetDbConnection().QueryAsync<T>(command);
        if (objectResult is null)
            return default!;

        return objectResult;
    }
}