using Application.Interfaces.Repositories.Dapper;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICommentRepository : IBaseRepository<Comment>
{
}
