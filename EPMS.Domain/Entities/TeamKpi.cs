using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class TeamKpi
{
    public int TeamKpiid { get; set; }

    public int TeamId { get; set; }

    public int DeptKpiid { get; set; }

    public decimal TeamTarget { get; set; }

    public decimal Weight { get; set; }

    public bool? IsActive { get; set; }

    public virtual DepartmentKpi DeptKpi { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
