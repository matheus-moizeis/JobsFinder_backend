using JobsFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsFinder.Infrastructure.DataAccess;
public class JobsFinderDbContext : DbContext
{
    public JobsFinderDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobsFinderDbContext).Assembly);
    }

    public DbSet<User> Users { get; set; }
}
