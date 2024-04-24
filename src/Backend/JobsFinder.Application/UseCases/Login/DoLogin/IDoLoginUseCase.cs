using JobsFinder.Communication.Requests;
using JobsFinder.Communication.Responses;

namespace JobsFinder.Application.UseCases.Login.DoLogin;
public interface IDoLoginUseCase 
{
    public Task<ResponseRegistredUserJson> Execute(RequestLoginJson request);
}
