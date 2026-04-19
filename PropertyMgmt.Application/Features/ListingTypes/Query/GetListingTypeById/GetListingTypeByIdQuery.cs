using MediatR;

namespace PropertyMgmt.Application.Features.ListingTypes.Query.GetListingTypeById;

public record GetListingTypeByIdQuery(Guid Id) : IRequest<ListingTypeDto>;
