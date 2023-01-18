using MediatR;

namespace Application.Features.Configuration.DeleteConfiguration;

public class DeleteConfigurationCommand : IRequest<object>
{
    public long Id { get; set; }
}

public class DeleteConfigurationCommandHandler : IRequestHandler<DeleteConfigurationCommand, object>
{
    public Task<object> Handle(DeleteConfigurationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
