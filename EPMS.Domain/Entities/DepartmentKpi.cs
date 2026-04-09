using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class DepartmentKpi
{
    public int DeptKpiid { get; set; }

    public int DepartmentId { get; set; }

    public int CycleId { get; set; }

    public int KpiMasterId { get; set; }

    public decimal DepartmentTarget { get; set; }

    public decimal Weight { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual AppraisalCycle Cycle { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<TeamKpi> TeamKpis { get; set; } = new List<TeamKpi>();
}
