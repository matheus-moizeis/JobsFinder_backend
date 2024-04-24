using JobsFinder.Application.UseCases.Login.DoLogin;
using JobsFinder.Communication.Requests;
using JobsFinder.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JobsFinder.API.Controllers;

public class LoginController : JobsFinderController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegistredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] IDoLoginUseCase useCase,
        [FromBody] RequestLoginJson request
        )
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
