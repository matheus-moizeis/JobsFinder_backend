﻿using JobsFinder.Application.UseCases.User.Register;
using JobsFinder.Communication.Requests;
using JobsFinder.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JobsFinder.API.Controllers;

public class UserController : JobsFinderController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegistredUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
