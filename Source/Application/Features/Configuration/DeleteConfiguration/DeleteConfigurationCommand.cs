using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Configuration.DeleteConfiguration;

public class DeleteConfigurationCommand : IRequest<object>
{
    public long Id { get; set; }
    public string WhereClause { get; set; }
}

public class DeleteConfigurationCommandHandler : IRequestHandler<DeleteConfigurationCommand, object>
{
    private readonly IConfigurationRepository _configurationRepository;
    private readonly IMapper _mapper;

    public DeleteConfigurationCommandHandler(IConfigurationRepository configurationRepository, IMapper mapper)
    {
        _configurationRepository = configurationRepository;
        _mapper = mapper;
    }

    public async Task<object> Handle(DeleteConfigurationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _configurationRepository.DeleteAsync(request.Id, request.WhereClause);
            return response;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
