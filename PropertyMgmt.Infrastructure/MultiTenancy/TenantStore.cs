using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Infrastructure.MultiTenancy;

public class TenantStore : ITenantStore
{
    private readonly string _connectionString;
    private readonly IMemoryCache _cache;

    public TenantStore(IConfiguration configuration, IMemoryCache cache)
    {
        // جلب نص الاتصال مباشرة من الإعدادات
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("Connection string is missing.");
        _cache = cache;
    }

    public async Task<string?> GetTenantBySubdomain(string subdomain)
    {
        var cacheKey = $"TenantDomain_{subdomain}";
        if (_cache.TryGetValue(cacheKey, out string? cachedTenantId))
        {
            return cachedTenantId;
        }

        using var connection = new SqlConnection(_connectionString);
        
        // استعلام مباشر وسريع جداً
        const string sql = "SELECT Id FROM Tenants WHERE Identifier = @subdomain";
        
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@subdomain", subdomain);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var tenantId = reader.GetString(0);
            _cache.Set(cacheKey, tenantId, TimeSpan.FromMinutes(10)); // تخزين في الذاكرة ل 10 دقائق
            return tenantId;
        }

        return null; // لم يتم العثور على المستأجر (عطالة منطقية)
    }
}