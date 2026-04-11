using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;

public class AuditLog : BaseEntity
{
    
    public string? UserId { get; set; } // من قام بالعملية (الأدمن)
    public string? Type { get; set; }   // (Create, Update, Delete)
    public string? TableName { get; set; } 
    public string? OldValues { get; set; } // القيم قبل التغيير (JSON)
    public string? NewValues { get; set; } // القيم بعد التغيير (JSON)
    public string? AffectedColumns { get; set; }
    public string? PrimaryKey { get; set; }
}