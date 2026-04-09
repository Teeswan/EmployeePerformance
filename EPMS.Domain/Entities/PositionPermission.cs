using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class PositionPermission
{
    public int PositionPermissionId { get; set; }

    public int PositionId { get; set; }

    public int PermissionId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
