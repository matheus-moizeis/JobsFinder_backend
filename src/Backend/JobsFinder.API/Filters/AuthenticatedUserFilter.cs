using JobsFinder.Communication.Responses;
using JobsFinder.Domain.Repositories.User;
using JobsFinder.Domain.Security.Tokens;
using JobsFinder.Exceptions;
using JobsFinder.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace JobsFinder.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserReadOnlyRepository _repository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository)
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);
            var userIdentifire = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

            bool exist = await _repository.ExistActiveUserWithIdentifier(userIdentifire);
            if (!exist) throw new JobsFinderException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenIsExpired")
            {
                TokenIsExpired = true,
            });
        }
        catch (JobsFinderException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authentication))
            throw new JobsFinderException(ResourceMessagesException.NO_TOKEN);

        return authentication["Bearer ".Length..].Trim();
    }
}
