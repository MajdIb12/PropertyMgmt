using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Users.Query.GetAllUser;

public class GetAllUserQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllUserQuery, PaginatedList<AllUserDto>>
{

    public async Task<PaginatedList<AllUserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var query = context.Customers.AsNoTracking()
            .Select(x => new AllUserDto
            {
                Id = x.Id,
                FullName = $"{x.FirstName} {x.LastName}",
                Email = x.Email
            });
        return await PaginatedList<AllUserDto>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
    }
}
