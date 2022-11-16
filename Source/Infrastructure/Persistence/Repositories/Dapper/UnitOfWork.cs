﻿using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Dapper;

namespace Infrastructure.Persistence.Repositories.Dapper;

public class UnitOfWork : IUnitOfWork
{
    public IPostRepository Posts { get; }
    public ICommentRepository Comments { get; }

    public UnitOfWork(IPostRepository Posts, ICommentRepository Comments)
    {
        this.Posts = Posts;
        this.Comments = Comments;
    }
}
