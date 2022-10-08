using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Presentation.Filters;

public class BaseExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
{
    private readonly ILogger _logger;

	public BaseExceptionFilter(ILogger<BaseExceptionFilter> logManager)
	{
		_logger = logManager;
	}

	public override void OnException(ExceptionContext context)
	{
        int statusCode;

        if (context.Exception is ArgumentNullException) statusCode = (int)HttpStatusCode.BadRequest;
        else if (context.Exception is ArgumentException) statusCode = (int)HttpStatusCode.BadRequest;
        else if (context.Exception is UnauthorizedAccessException) statusCode = (int)HttpStatusCode.Unauthorized;
        // else if (context.Exception is SecurityAccessDeniedException) statusCode = (int)HttpStatusCode.Forbidden;
        else // On special errors
        {
            statusCode = (int)HttpStatusCode.InternalServerError;
        }

        // Customize this object to fit your needs
        var result = new ObjectResult(new
        {
            context.Exception.Message, // Or a different generic message
            context.Exception.Source,
            ExceptionType = context.Exception.GetType().FullName,
            StatusCode = statusCode
        });

        // Log the exception
        _logger.LogError("Unhandled exception occurred while executing request: {ex}", context.Exception);

        // Set the result
        context.Result = result;
	}
}
