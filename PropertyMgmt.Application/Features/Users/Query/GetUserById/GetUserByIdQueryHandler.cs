using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Application.Features.Users.Query;

public class GetUserByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Customers.AsNoTracking()
            .Where(u => u.Id == request.Id)
            .Select(u => new UserDto
            {
                Id = u.Id,
                FullName = $"{u.FirstName} {u.LastName}",
                Email = u.Email,
                ActiveSubscriptions = u.Subscriptions.Count(s => s.IsActive),
                TotalBookings = u.MyBookings.Count(u => u.Status == BookingStatus.Confirmed),
                TotalProperties = u.OwnedListings.Count()


            })
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(Users), request.Id);

        

        return user;
    }
}