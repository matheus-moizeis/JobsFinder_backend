using JobsFinder.Domain.Entities;
using JobsFinder.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace JobsFinder.Infrastructure.DataAccess.Repositories;
public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly JobsFinderDbContext _dbContext;

    public UserRepository(JobsFinderDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email.Equals(email) && user.Active);
    }


}
