using System.Text.Json;
using BackEnd.Domain.SeedWork;
using BackEnd.Infra.Data.EF.Context;

namespace BackEnd.Infra.Data.EF.Repositories;

public class Repository : IRepository
{
    private readonly PgDbContext _context;

    public Repository(PgDbContext context)
        => _context = context;

    public async Task<string> AddObject<T>(T objectInsert)
    {

        try
        {
            if (objectInsert is null)
                throw new Exception("null is Exception");

            var createdObject = await _context.AddAsync(objectInsert);
            var returnCreated = JsonSerializer.Serialize(createdObject);

            return returnCreated;
        }
        catch (Exception ex)
        {
            return await Task.FromResult(string.Concat(false, $"- {ex.Message}"));
        }
    }

    public async Task<string> UpdateObject<T>(T objectUpdate)
    {
        try
        {
            if (objectUpdate is null)
                throw new Exception("null is Exception");

            _context.Update(objectUpdate);

            return await Task.FromResult(true.ToString());
        }
        catch (Exception ex)
        {
            return await Task.FromResult(string.Concat(false, $"- {ex.Message}"));
        }
    }

    public async Task<string> DeleteObject<T>(T objectRemove)
    {
        try
        {
            if (objectRemove is null)
                throw new Exception("null is Exception");

            _context.Remove(objectRemove);

            return await Task.FromResult(true.ToString());
        }
        catch (Exception ex)
        {
            return await Task.FromResult(string.Concat(false, $"- {ex.Message}"));
        }
    }
}