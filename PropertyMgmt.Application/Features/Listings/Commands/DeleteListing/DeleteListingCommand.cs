using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Listings.Commands.DeleteListing;

public record DeleteListingCommand(Guid Id) : IRequest<bool>;
