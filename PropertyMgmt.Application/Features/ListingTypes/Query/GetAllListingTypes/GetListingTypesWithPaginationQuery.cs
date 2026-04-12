using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.ListingTypes.Query.GetAllListingTypes
{
    public class GetListingTypesWithPaginationQuery : IRequest<PaginatedList<ListingTypeLookupDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}