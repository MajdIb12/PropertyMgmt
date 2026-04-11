using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Listings.Commands.DeleteListing;

public class DeleteListingCommand : IRequest<bool>
{    
    public Guid Id { get; set; }
}
