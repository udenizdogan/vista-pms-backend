using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using VistaPms.Application.Services;

namespace VistaPms.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../VistaPms.API"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        // Get connection string
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Create DbContextOptions
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(
            connectionString,
            npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });

        // Create mock services for design-time
        var currentUserService = new DesignTimeCurrentUserService();
        var dateTimeProvider = new DesignTimeDateTimeProvider();

        return new ApplicationDbContext(optionsBuilder.Options, currentUserService, dateTimeProvider);
    }

    // Design-time implementation of ICurrentUserService
    private class DesignTimeCurrentUserService : ICurrentUserService
    {
        public string? UserId => null;
        public string? TenantId => null;
        public string? BranchId => null;
        public bool HasFullAccess => true; // Assume full access for design time
    }

    // Design-time implementation of IDateTimeProvider
    private class DesignTimeDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
        public DateTime Today => DateTime.Today;
    }
}
