using MediatR;

namespace Application.Features.Configuration.UpdateConfiguration;

public class UpdateConfigurationCommand : IRequest<object>
{
    public long Id { get; set; }
}

public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand, object>
{
    public Task<object> Handle(UpdateConfigurationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
