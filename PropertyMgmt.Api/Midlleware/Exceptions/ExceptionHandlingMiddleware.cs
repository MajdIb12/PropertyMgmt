using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions; // استدعاء استثناءاتك

namespace PropertyMgmt.Api.Middleware.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        // تحديد التفاصيل بناءً على نوع الاستثناء
        var problemDetails = exception switch
        {
            ValidationExceptions vEx => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                Detail = vEx.Message,
                Extensions = { ["errors"] = vEx.Errors } // إضافة قاموس الأخطاء
            },
            NotFoundException => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = exception.Message
            },
            DeleteFailureException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Delete Failure",
                Detail = exception.Message
            },
            DbUpdateException => new ProblemDetails
            {
              Status = StatusCodes.Status409Conflict,
              Title = "Database Conflict",
              Detail = "لا يمكن إتمام العملية بسبب وجود بيانات مرتبطة أو تكرار في السجلات."
           },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Detail = "An unexpected error occurred."
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true; // تعني أننا قمنا بمعالجة الخطأ ولا داعي لتمريره لمكان آخر
    }
}
