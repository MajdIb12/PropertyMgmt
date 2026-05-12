using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Users.Query;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int ActiveSubscriptions { get; set; }
    public int TotalBookings { get; set; }
    public int TotalProperties { get; set; }

}
