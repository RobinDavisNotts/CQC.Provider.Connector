using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CQC.Provider.Connector.Data.Contexts;

public class ProviderContext(DbContextOptions<ProviderContext> options) : DbContext(options: options)
{
    public DbSet<Core.Entities.Provider> Providers { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Core.Entities.Provider>().ToCollection("Providers");
    }
}