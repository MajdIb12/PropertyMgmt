using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.Bookings.Query.GetAllBookingByUserId;

public record GetAllBookingByUserIdQuery(Guid UserId, int PageNumber, int PageSize) : IRequest<PaginatedList<BookingListDto>>;
