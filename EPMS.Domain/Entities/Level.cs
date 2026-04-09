using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class Level
{
    public string LevelId { get; set; } = null!;

    public string LevelName { get; set; } = null!;

    public string? LevelDescription { get; set; }

    public virtual ICollection<PerformanceOutcome> PerformanceOutcomeNewLevels { get; set; } = new List<PerformanceOutcome>();

    public virtual ICollection<PerformanceOutcome> PerformanceOutcomeOldLevels { get; set; } = new List<PerformanceOutcome>();

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
}
