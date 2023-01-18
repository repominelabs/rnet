using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ConfigurationRepository : BaseRepository<Configuration>, IConfigurationRepository
{
    public ConfigurationRepository(string connStr) : base(connStr)
    {
    }
}
