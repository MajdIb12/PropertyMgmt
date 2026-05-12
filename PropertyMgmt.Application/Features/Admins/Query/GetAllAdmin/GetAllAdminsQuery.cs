using System;
using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.Admins.Query.GetAllAdmin;

public record GetAllAdminsQuery(int PageNumber, int PageSize): IRequest<PaginatedList<AdminDto>>;

public class AdminDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
}
