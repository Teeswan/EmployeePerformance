using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class AppraisalQuestion
{
    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string? Category { get; set; }

    public bool? IsRequired { get; set; }

    public virtual ICollection<AppraisalResponse> AppraisalResponses { get; set; } = new List<AppraisalResponse>();
}
