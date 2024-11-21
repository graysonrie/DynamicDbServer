using DynamicDbServer.src.Features.DynamicDbBase.Interfaces;
using DynamicDbServer.src.Features.DynamicDbBase.Models;
using DynamicDbServer.src.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DynamicDbServer.src.Features.DynamicDbBase.Services;

public class DynamicDbService(AppDbContext db) : IDynamicDbService
{
    readonly AppDbContext _db = db;
    DbSet<DbObj> Table => _db.DbObjs;

    public async Task<DbObj?> GetByIdAsync(Guid objId)
    {
        return await Table.FindAsync(objId);
    }

    public async Task<List<DbObj>> GetByOwnerAsync(Guid ownerId, string group)
    {
        return await Table.Where(x => x.OwnerId == ownerId && x.Group == group).ToListAsync();
    }

    public async Task UpdateJsonDataAsync(Guid objId, Dictionary<string, object> newProperties)
    {
        DbObj? obj = await Table.FindAsync(objId);
        if (obj != null)
        {
            obj.Data = newProperties;
            await _db.SaveChangesAsync();
            return;
        }
        Log.Warning($"UpdateJsonDataAsync: The following ID does not exist in the DB: {objId}");
    }

    public async Task UpsertRecordAsync(DbObj obj)
    {
        await Table.AddAsync(obj);
        await _db.SaveChangesAsync();
    }
}