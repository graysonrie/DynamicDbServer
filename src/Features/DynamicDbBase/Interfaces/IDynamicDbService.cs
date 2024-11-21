using DynamicDbServer.src.Features.DynamicDbBase.Models;

namespace DynamicDbServer.src.Features.DynamicDbBase.Interfaces;

public interface IDynamicDbService{
    public Task UpsertRecordAsync(DbObj obj);
    public Task UpdateJsonDataAsync(Guid objId, Dictionary<string,object> newProperties);
    public Task<DbObj?> GetByIdAsync(Guid objId);
    public Task<List<DbObj>> GetByOwnerAsync(Guid ownerId, string group);
}