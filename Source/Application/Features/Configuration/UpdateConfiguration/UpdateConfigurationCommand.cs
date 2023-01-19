using Application.Features.Configuration.CreateConfiguration;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Configuration.UpdateConfiguration;

public class UpdateConfigurationCommand : IRequest<object>
{
    public long Id { get; set; }
    public string Application { get; set; }
    public string ConfigType { get; set; }
    public string ConfigValue1 { get; set; }
    public string ConfigValue2 { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string IsActive { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public string LastModifiedBy { get; set; }
}

public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand, object>
{
    private readonly IConfigurationRepository _configurationRepository;
    private readonly IMapper _mapper;

    public UpdateConfigurationCommandHandler(IConfigurationRepository configurationRepository, IMapper mapper)
    {
        _configurationRepository = configurationRepository;
        _mapper = mapper;
    }

    public async Task<object> Handle(UpdateConfigurationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Domain.Entities.Configuration configuration = _mapper.Map<UpdateConfigurationCommand, Domain.Entities.Configuration>(request);
            var response = await _configurationRepository.UpdateAsync(configuration, whereClause: $"id = {configuration.Id}");
            return response;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
