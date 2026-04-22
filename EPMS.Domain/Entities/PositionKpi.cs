using System;
using System.Collections.Generic;

namespace EPMS.Domain.Entities;

public partial class PositionKpi
{
    public int PositionKpiid { get; set; }

    public int PositionId { get; set; }

    public int KpiId { get; set; }

    public decimal? DefaultWeightPercent { get; set; }

    public bool? IsRequired { get; set; }

    public virtual Kpi Kpi { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
