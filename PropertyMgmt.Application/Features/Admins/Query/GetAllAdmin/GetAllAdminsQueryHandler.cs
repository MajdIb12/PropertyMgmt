using MediatR;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Admins.Query.GetAllAdmin;

public class GetAllAdminsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllAdminsQuery, PaginatedList<AdminDto>>
{

    public async Task<PaginatedList<AdminDto>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
    {
        var query = from admin in context.Admins
                    select new AdminDto
                    {
                        Id = admin.Id,
                        Email = admin.Email
                    };
        return await PaginatedList<AdminDto>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
    }
}