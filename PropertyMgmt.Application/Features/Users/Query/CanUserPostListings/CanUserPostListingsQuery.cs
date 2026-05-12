using System;
using MediatR;
using PropertyMgmt.Application.Common.Exceptions;

namespace PropertyMgmt.Application.Features.Users.Query.CanUserPostListings;

public record CanUserPostListingsQuery(Guid UserId): IRequest<bool>;
