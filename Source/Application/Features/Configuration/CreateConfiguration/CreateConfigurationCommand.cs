using MediatR;

namespace Application.Features.Configuration.CreateConfiguration;

public class CreateConfigurationCommand : IRequest<object>
{
}

public class CreateConfigurationCommandHandler : IRequestHandler<CreateConfigurationCommand, object>
{
    public Task<object> Handle(CreateConfigurationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
