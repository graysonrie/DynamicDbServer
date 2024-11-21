using DynamicDbServer.src.Features.DynamicDbBase.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace DynamicDbServer.src.Shared.Data;
public class AppDbContext : DbContext
{
    public DbSet<DbObj> DbObjs { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbObj>()
        .Property(e => e.Data)
        .HasConversion(v => JsonConvert.SerializeObject(v),
        v => JsonConvert.DeserializeObject<Dictionary<string, object>>(v));

        modelBuilder.Entity<DbObj>().HasIndex(e => e.Group).HasDatabaseName("DbObjGroup");

        base.OnModelCreating(modelBuilder);

    }
}