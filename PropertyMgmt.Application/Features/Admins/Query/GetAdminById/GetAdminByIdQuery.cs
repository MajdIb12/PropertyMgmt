using MediatR;
using PropertyMgmt.Domain.Enums;

namespace PropertyMgmt.Application.Features.Admins.Query.GetAdminById;

public record GetAdminByIdQuery(Guid Id) : IRequest<GetAdminByIdResponseDto>;

public class GetAdminByIdResponseDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public AdminRole Role { get; set; }

    public DateTime CreatedAt { get; set; }
}
