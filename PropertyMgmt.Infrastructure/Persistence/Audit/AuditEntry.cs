using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertyMgmt.Domain.Entities;
using System.Text.Json;

namespace PropertyMgmt.Infrastructure.Persistence.Audit;

public class AuditEntry
{
    public EntityEntry Entry { get; }
    public string? UserId { get; set; }
    public string? TableName { get; set; }
    public string? Type { get; set; }
    public Dictionary<string, object> KeyValues { get; } = new();
    public Dictionary<string, object> OldValues { get; } = new();
    public Dictionary<string, object> NewValues { get; } = new();
    public List<string> ChangedColumns { get; } = new();

    public AuditEntry(EntityEntry entry) => Entry = entry;

    public AuditLog ToAuditLog()
    {
        return new AuditLog
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            Type = Type,
            TableName = TableName,
            PrimaryKey = JsonSerializer.Serialize(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
            AffectedColumns = ChangedColumns.Count == 0 ? null : string.Join(", ", ChangedColumns)
        };
    }
}
