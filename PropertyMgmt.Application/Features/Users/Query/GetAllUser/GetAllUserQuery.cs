using System;
using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.Users.Query.GetAllUser;

public record GetAllUserQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<AllUserDto>>;

public class AllUserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
