using Application.Features.Configuration.CreateConfiguration;
using Application.Features.Configuration.DeleteConfiguration;
using Application.Features.Configuration.UpdateConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V2;

[Route("Api/V{version:apiVersion}/[controller]/[action]")]
[ApiVersion("2.0")]
public class ConfigurationController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<object>> Create(CreateConfigurationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<object>> Delete(DeleteConfigurationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<object>> Update(UpdateConfigurationCommand command)
    {
        return await Mediator.Send(command);
    }
}
