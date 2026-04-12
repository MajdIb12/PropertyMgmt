namespace PropertyMgmt.Application.Features.ListingTypes.Query.GetAllListingTypes
{
    public record ListingTypeLookupDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}