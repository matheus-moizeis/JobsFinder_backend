using JobsFinder.Domain.Repositories;

namespace JobsFinder.Infrastructure.DataAccess;
public class UnitOfWork : IUnitOfWork
{
    private readonly JobsFinderDbContext _dbContext;

    public UnitOfWork(JobsFinderDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
