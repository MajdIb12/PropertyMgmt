using MediatR;

namespace PropertyMgmt.Application.Features.Listings.Query.GetListingById;

public record GetListingByIdQuery(Guid Id) : IRequest<ListingDto>;
