using KSFramework.Exceptions;
using KSFramework.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Website.Common.WebFrameworks.FilterAttributes;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(KSNotFoundException), HandleNotFoundException },
                { typeof(KSBadRequestException), HandleBadRequestException },
                { typeof(KSValidationException), HandleValidationException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (KSValidationException)context.Exception;

        List<FormValidationExceptionModel> errors = new();

        foreach (var item in exception.Errors)
        {

            errors.Add(new FormValidationExceptionModel(item.PropertyName, item.ErrorMessage, item.ErrorCode));
        }

        var res = new FormValidationViewModelResponse<FormValidationExceptionModel>(400, exception.Message, errors);

        context.Result = new BadRequestObjectResult(res);
        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState);

        FormValidationResponse<IDictionary<string, string[]>> res = new("InvalidModelState", details.Errors);

        context.Result = new BadRequestObjectResult(res)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (KSNotFoundException)context.Exception;

        var res = new NotFoundResponse<string>(exception.Message);

        context.Result = new NotFoundObjectResult(res)
        {
            StatusCode = StatusCodes.Status404NotFound,
        };
        context.ExceptionHandled = true;
    }

    private void HandleBadRequestException(ExceptionContext context)
    {
        var exception = (KSBadRequestException)context.Exception;

        var res = new BadRequestResponse<string>(exception.Message);

        context.Result = new NotFoundObjectResult(res)
        {
            StatusCode = StatusCodes.Status400BadRequest,
        };
        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var msg = context.Exception.Message;
        if (context.Exception.InnerException != null)
            msg += $" {context.Exception.InnerException.Message}";

        var res = new ServerErrorResponse<string>(msg);

        context.Result = new ObjectResult(res)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var res = new UnauthorizedAccessResponse<string>("Unauthorized");

        context.Result = new ObjectResult(res)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };

        context.ExceptionHandled = true;
    }

}

public record FormValidationExceptionModel(string PropertyName, string ErrorMessage, string ErrorCode);
public record FormValidationViewModelResponse<T>(int Code, string Msg, IEnumerable<T> Errors);