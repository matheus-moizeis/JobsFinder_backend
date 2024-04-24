using JobsFinder.Application.Services.Cryptography;
using JobsFinder.Communication.Requests;
using JobsFinder.Communication.Responses;
using JobsFinder.Domain.Repositories.User;
using JobsFinder.Exceptions.ExceptionsBase;

namespace JobsFinder.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly PasswordEncripter _passwordEncripter;

    public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncripter passwordEncripter)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
    }

    public async Task<ResponseRegistredUserJson> Execute(RequestLoginJson request)
    {
        var encriptedPassword = _passwordEncripter.Encript(request.Password);

        var user = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

        return new ResponseRegistredUserJson
        {
            Name = user.Name
        };
    }
}
