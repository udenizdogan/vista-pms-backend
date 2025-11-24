namespace VistaPms.Application.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? TenantId { get; }
}
