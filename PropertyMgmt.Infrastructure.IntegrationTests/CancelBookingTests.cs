using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using PropertyMgmt.Application.Features.Bookings.Command.CancelBooking;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Common;
using PropertyMgmt.Domain.Entities;
using PropertyMgmt.Domain.Enums;
using PropertyMgmt.Domain.ValueObjects;
using PropertyMgmt.Infrastructure.Contexts;
namespace PropertyMgmt.Infrastructure.IntegrationTests;


public class CancelBookingTests : IAsyncLifetime
{

    private ApplicationDbContext _context;
    private ITenantService _mockTenantService;
    private const string TestTenantId = "A1B2C3D4-E5F6-47A8-9B0C-1D2E3F4G5H6I";

    // تشغيل الحاوية في Docker قبل بدء الاختبار
    public async Task InitializeAsync()
    {
        _mockTenantService = Substitute.For<ITenantService>();
        _mockTenantService.TenantId.Returns(TestTenantId);

        
        var localConnectionString = "Server=.;Database=PropertyMgmtDb_Tests;User Id=sa;Password=sa123456;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True";

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(localConnectionString)
            .Options;

        _context = new ApplicationDbContext(options, _mockTenantService);

        await _context.Database.EnsureDeletedAsync();

        await _context.Database.EnsureCreatedAsync();
    }

    [Fact]
    public async Task Handle_WhenBookingIsCancelled_ShouldMakeListingAvailableAndCancelPayment()
    {
       
        var address = new Address("Majd",  "Dubi", "UAE", null);
        var listingType = new ListingType { Id = Guid.NewGuid(), Name = "Apartment", Description = "bad" };
        var listing = new Listing { Id = Guid.NewGuid(), Name = "Luxury Apartment", Description = "good", PricePerNight = 1, MaxGuests = 3, Bedrooms = 2, Bathrooms = 2, ListingTypeId = listingType.Id, OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000003"), Address = address
            , Status = ListingStatus.Reserved };
        var booking = new Booking(listing.Id, Guid.Parse("00000000-0000-0000-0000-000000000002"), DateTime.UtcNow, DateTime.UtcNow.AddDays(3), 300);
        var payment = new Payment { BookingId = booking.Id, Amount = 300, Status = PaymentStatus.Pending, Method = PaymentMethod.BankTransfer };

        await _context.ListingTypes.AddAsync(listingType);
        await _context.Listings.AddAsync(listing);
        await _context.Bookings.AddAsync(booking);
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();

        var command = new CancelBookingCommand(booking.Id);
        var handler = new CancelBookingCommandHandler(_context);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().BeTrue();

        // إعادة جلب البيانات من القاعدة للتأكد من التغيير الحقيقي
        var updatedListing = await _context.Listings.FindAsync(listing.Id);
        var updatedBooking = await _context.Bookings.FindAsync(booking.Id);
        var updatedPayment = await _context.Payments.FindAsync(payment.Id);

        updatedListing.Status.Should().Be(ListingStatus.Available);
        updatedBooking.Status.Should().Be(BookingStatus.Cancelled);
        updatedPayment.Status.Should().Be(PaymentStatus.Failed);
    }

    // تدمير الحاوية وتنظيف الـ Docker تماماً بعد انتهاء الاختبار
    public async Task DisposeAsync()
    {
        await _context.DisposeAsync().AsTask();
    }
}
