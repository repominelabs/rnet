using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts;

public interface IApplicationDbContext
{
    DbSet<Configuration> Configurations { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
