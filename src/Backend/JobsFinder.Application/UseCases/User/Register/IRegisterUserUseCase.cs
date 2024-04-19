using JobsFinder.Communication.Requests;
using JobsFinder.Communication.Responses;

namespace JobsFinder.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    public Task<ResponseRegistredUserJson> Execute(RequestRegisterUserJson request);
}
