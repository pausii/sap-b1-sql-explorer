using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CacheTablesEntity> CacheTables => Set<CacheTablesEntity>();
        public DbSet<ExternalColumnTable> ExtColumnTable => Set<ExternalColumnTable>();
        public DbSet<ExternalIndexTableEntity> ExtIndexTable => Set<ExternalIndexTableEntity>();
        public DbSet<ExternalTableListEntity> ExternalTableListEntity => Set<ExternalTableListEntity>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CacheTablesEntity>()
                .HasIndex(c => new { c.SchemaName, c.TableName })
                .IsUnique();
            modelBuilder.Entity<CacheTablesEntity>()
                .HasIndex(c => c.TableName);
            modelBuilder.Entity<CacheTablesEntity>()
                .HasIndex(c => c.Description);

            modelBuilder.Entity<ExternalColumnTable>();
            modelBuilder.Entity<ExternalIndexTableEntity>();
            modelBuilder.Entity<ExternalTableListEntity>();
        }
    }
}
