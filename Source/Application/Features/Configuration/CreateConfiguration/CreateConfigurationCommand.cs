using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Configuration.CreateConfiguration;

public class CreateConfigurationCommand : IRequest<object>
{
    public string Application { get; set; }
    public string ConfigType { get; set; }
    public string ConfigValue1 { get; set; }
    public string ConfigValue2 { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string IsActive { get; set; }
    public string Description { get; set; }
}

public class CreateConfigurationCommandHandler : IRequestHandler<CreateConfigurationCommand, object>
{
    private readonly IConfigurationRepository _configurationRepository;
    private readonly IMapper _mapper;

    public CreateConfigurationCommandHandler(IConfigurationRepository configurationRepository, IMapper mapper)
    {
        _configurationRepository = configurationRepository;
        _mapper = mapper;
    }

    public Task<object> Handle(CreateConfigurationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Domain.Entities.Configuration configuration = _mapper.Map<CreateConfigurationCommand, Domain.Entities.Configuration>(request);
            var response = _configurationRepository.Create(configuration);
            return response;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
