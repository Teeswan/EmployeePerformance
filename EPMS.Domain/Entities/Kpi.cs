using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class Kpi
{
    public int KpiId { get; set; }

    public string Kpiname { get; set; } = null!;

    public string? Category { get; set; }

    public string? Unit { get; set; }

    public virtual ICollection<EmployeeKpi> EmployeeKpis { get; set; } = new List<EmployeeKpi>();

    public virtual ICollection<PositionKpi> PositionKpis { get; set; } = new List<PositionKpi>();
}
