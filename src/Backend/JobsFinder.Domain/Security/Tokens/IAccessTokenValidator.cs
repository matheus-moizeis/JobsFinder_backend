﻿namespace JobsFinder.Domain.Security.Tokens;
public interface IAccessTokenValidator
{
    public Guid ValidateAndGetUserIdentifier(string token);
}
