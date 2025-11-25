using System.Security.Claims;
using VistaPms.Application.Services;

namespace VistaPms.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? TenantId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("tenant_id"); 
    public string? BranchId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("branch_id");

    public bool HasFullAccess 
    {
        get 
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return false;

            // Örnek: "GeneralManager" rolü veya özel bir izin claim'i kontrol edilebilir.
            // Şimdilik "GeneralManager" rolü veya "AllBranches" izni varsa true döner.
            return user.IsInRole("GeneralManager") || user.HasClaim(c => c.Type == "permissions" && c.Value == "view:all_branches");
        }
    }
}
