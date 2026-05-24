namespace PropertyMgmt.Application.Common.Behaviors;

using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

public class ChatAccessValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IChatRequest // 🎯 السحر هنا: قصر البايبلاين على عمليات الشات فقط
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITenantService _tenantService;

    public ChatAccessValidationBehavior(
        IApplicationDbContext context,
        ICurrentUserService currentUserService,
        ITenantService tenantService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _tenantService = tenantService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var currentUserId = Guid.TryParse(_currentUserService.UserId, out Guid result) ? result
            : Guid.Empty;
        var currentTenantId = _tenantService.TenantId;

        var hasAccess = await _context.Conversations
            .AsNoTracking()
            .AnyAsync(c => c.Id == request.ConversationId
                        && c.TenantId == currentTenantId
                        && (c.OwnerId == currentUserId || c.CustomerId == currentUserId),
                     cancellationToken);

        if (!hasAccess)
        {
            throw new UnauthorizedAccessException();
        }

        return await next();
    }
}