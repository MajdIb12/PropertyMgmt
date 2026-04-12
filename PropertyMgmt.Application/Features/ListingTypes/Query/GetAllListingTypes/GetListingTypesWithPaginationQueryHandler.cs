using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.ListingTypes.Query.GetAllListingTypes
{
    public class GetListingTypesWithPaginationQueryHandler : IRequestHandler<GetListingTypesWithPaginationQuery, PaginatedList<ListingTypeLookupDto>>
    {
        private readonly IApplicationDbContext _Context;

        public GetListingTypesWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<PaginatedList<ListingTypeLookupDto>> Handle(GetListingTypesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            // 1. قم ببناء الاستعلام الأساسي فقط
            var query = _Context.ListingTypes
                .AsNoTracking()
                .Select(x => new ListingTypeLookupDto 
                { 
                    Id = x.Id, 
                    Name = x.Name 
                });
        
            // 2. استخدم ميثود احترافية داخل PaginatedList للقيام بكل شيء
            return await PaginatedList<ListingTypeLookupDto>.CreateAsync(
                query, 
                request.PageNumber, 
                request.PageSize, 
                cancellationToken);
        }
            }
        }