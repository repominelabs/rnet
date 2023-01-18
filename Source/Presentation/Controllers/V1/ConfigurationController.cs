using Application.Features.Configuration.CreateConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V1;

[Route("Api/V{version:apiVersion}/[controller]/[action]")]
[ApiVersion("1.0", Deprecated = true)]
public class ConfigurationController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<object>> Create(CreateConfigurationCommand command)
    {
        return await Mediator.Send(command);
    }
}
