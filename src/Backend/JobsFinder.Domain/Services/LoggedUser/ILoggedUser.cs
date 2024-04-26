using JobsFinder.Domain.Entities;

namespace JobsFinder.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    public Task<User> User();
}
