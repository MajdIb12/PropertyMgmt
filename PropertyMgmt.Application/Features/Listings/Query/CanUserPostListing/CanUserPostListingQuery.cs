using MediatR;

namespace PropertyMgmt.Application.Features.Listings.Query.CanUserPostListing;

public record CanUserPostListingQuery(Guid UserId) : IRequest<bool>;
