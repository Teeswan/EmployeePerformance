using System;
using System.Collections.Generic;

namespace EPMS.Domain.Entities;

public partial class EmployeeKpi
{
    public int EmployeeKpiid { get; set; }

    public int? EmployeeId { get; set; }

    public int? CycleId { get; set; }

    public int? KpiId { get; set; }

    public decimal WeightPercent { get; set; }

    public decimal TargetValue { get; set; }

    public decimal? ActualValue { get; set; }

    public decimal? WeightedScore { get; set; }

    public int? VersionNo { get; set; }

    public string? Status { get; set; }

    public decimal? SnapshotTarget { get; set; }

    public decimal? SnapshotWeight { get; set; }

    public virtual AppraisalCycle? Cycle { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Kpi? Kpi { get; set; }
}
