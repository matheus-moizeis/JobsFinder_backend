﻿namespace JobsFinder.Domain.Repositories;
public interface IUnitOfWork
{
    public Task Commit();
}
