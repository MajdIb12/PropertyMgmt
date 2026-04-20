using MediatR;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Common.Behaviors;

public class TransactionBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ITransactionalRequest
{
    private readonly IApplicationDbContext _context;

    public TransactionBehavior(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var transaction = await _context.BeginTransactionAsync(cancellationToken);
        try
        {
            var response = await next();
            await _context.CommitTransactionAsync(cancellationToken);
            return response;
        }
        catch
        {
            await _context.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}