using MediatR;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Features.Bookings.Query.GetBookingById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyMgmt.Application.Features.Bookings.Query.GetAllbookingByOwnerId;

public record GetAllBookingByOwnerIdQuery(Guid OwnerId, int PageNumber, int PageSize) : IRequest<PaginatedList<BookingListDto>>;
