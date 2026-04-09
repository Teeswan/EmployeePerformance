using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class OneOnOneMeeting
{
    public int MeetingId { get; set; }

    public int EmployeeId { get; set; }

    public int ManagerId { get; set; }

    public DateTime ScheduledDateTime { get; set; }

    public string? Location { get; set; }

    public string? MeetingStatus { get; set; }

    public string? RescheduleReason { get; set; }

    public int? ParentMeetingId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DiscussionPoints { get; set; }

    public string? ActionItems { get; set; }

    public string? MeetingSummary { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<OneOnOneMeeting> InverseParentMeeting { get; set; } = new List<OneOnOneMeeting>();

    public virtual Employee Manager { get; set; } = null!;

    public virtual ICollection<MeetingNote> MeetingNotes { get; set; } = new List<MeetingNote>();

    public virtual OneOnOneMeeting? ParentMeeting { get; set; }
}
