﻿using JobsFinder.Domain.Entities;
using JobsFinder.Domain.Security.Tokens;
using JobsFinder.Domain.Services.LoggedUser;
using JobsFinder.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JobsFinder.Infrastructure.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly JobsFinderDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(JobsFinderDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _dbContext.Users
            .AsNoTracking()
            .FirstAsync(
                user => user.Active 
                && user.UserIdentifier == userIdentifier
                );
    }
}
