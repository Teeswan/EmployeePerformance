using System;
using System.Collections.Generic;

namespace EPMS.Domain.Entities;

public partial class AuditLog
{
    public long AuditId { get; set; }

    public string TableName { get; set; } = null!;

    public int RecordId { get; set; }

    public string ActionType { get; set; } = null!;

    public string? OldData { get; set; }

    public string? NewData { get; set; }

    public int ChangedByUserId { get; set; }

    public DateTime? ChangedAt { get; set; }
}
