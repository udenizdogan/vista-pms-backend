namespace VistaPms.Application.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? TenantId { get; }
    string? BranchId { get; }
    bool HasFullAccess { get; } // Genel Müdür gibi roller için
}
