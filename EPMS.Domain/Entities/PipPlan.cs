using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class PipPlan
{
    public int Pipid { get; set; }

    public int EmployeeId { get; set; }

    public int ManagerId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Status { get; set; }

    public string? OverallGoal { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Employee Manager { get; set; } = null!;

    public virtual ICollection<PipMeeting> PipMeetings { get; set; } = new List<PipMeeting>();

    public virtual ICollection<PipObjective> PipObjectives { get; set; } = new List<PipObjective>();
}
