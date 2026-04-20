using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace PropertyMgmt.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        
        // تسجيل بداية العملية
        _logger.LogInformation("بدء معالجة الطلب: {Name} {@Request}", 
            requestName, request);

        var timer = Stopwatch.StartNew();

        try
        {
            var response = await next();
            
            timer.Stop();

            // تسجيل نجاح العملية مع الوقت المستغرق
            _logger.LogInformation("اكتمل الطلب: {Name} في {ElapsedMilliseconds}ms", 
                requestName, timer.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            timer.Stop();
            // تسجيل فشل العملية
            _logger.LogError(ex, "فشل الطلب: {Name} بعد {ElapsedMilliseconds}ms", 
                requestName, timer.ElapsedMilliseconds);
            throw;
        }
    }
}
