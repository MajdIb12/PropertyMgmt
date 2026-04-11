namespace PropertyMgmt.Domain.ValueObjects;

    public record Address(
        string Street, 
        string City, 
        string Country,
        string? ZipCode);
