namespace PropertyMgmt.Application.Features.ListingTypes.Query.GetListingTypeById;

public class ListingTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public static ListingTypeDto Create(Guid id, string name, string description)
    {
        return new ListingTypeDto
        {
            Id = id,
            Name = name,
            Description = description
        };
    }
}