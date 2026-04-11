using PropertyMgmt.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace PropertyMgmt.Application.Features.Listings.Query.GetAllListings;

public class GetListingsWithPaginationQueryHandler 
    : IRequestHandler<GetListingsWithPaginationQuery, PaginatedList<ListingLookupDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly GetListingsWithPaginationMapper _mapper; // Mapperly 

    public GetListingsWithPaginationQueryHandler(IApplicationDbContext context, GetListingsWithPaginationMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ListingLookupDto>> Handle(GetListingsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        // 1. نبدأ بالاستعلام الأساسي (الفلترة التلقائية للـ IsDeleted تعمل هنا)
        var query = _context.Listings
            .AsNoTracking()
            .Include(x => x.ListingType)
            .Include(x => x.Images)
            .OrderBy(x => x.Name); // الترتيب ضروري للـ Pagination

        // 2. حساب العدد الإجمالي
        var count = await query.CountAsync(cancellationToken);

        // 3. تطبيق الـ Pagination (Skip & Take)
        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        // 4. تحويل النتائج إلى DTOs باستخدام الماپر
        var dtos = items.Select(_mapper.MapToListingLookupDto).ToList();

        return new PaginatedList<ListingLookupDto>(dtos, count, request.PageNumber, request.PageSize);
    }
}
