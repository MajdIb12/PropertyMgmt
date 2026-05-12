using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Admins.Query.GetAdminById;

public class GetAdminByIdQueryHandler(IApplicationDbContext context) 
    : IRequestHandler<GetAdminByIdQuery, GetAdminByIdResponseDto>
{
    public async Task<GetAdminByIdResponseDto> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
    {
        var admin = await context.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
                    ?? throw new NotFoundException(nameof(Admins), request.Id);

        return new GetAdminByIdResponseDto
        {
            Id = admin.Id,
            FullName = admin.FullName,
            Email = admin.Email,
            Role = admin.Role,
            CreatedAt = admin.CreatedAt
        };
    }
}