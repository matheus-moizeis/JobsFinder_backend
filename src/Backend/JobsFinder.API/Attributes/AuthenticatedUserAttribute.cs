using JobsFinder.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JobsFinder.API.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
