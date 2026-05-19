using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyMgmt.Application.Features.Bookings.Query;

public class BookingListDto
{
    public Guid BookingId { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid ListingId { get; set; }
    public Guid OwnerId { get; set; }
}
