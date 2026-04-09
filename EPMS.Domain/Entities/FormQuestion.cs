using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class FormQuestion
{
    public int? FormId { get; set; }

    public int? QuestionId { get; set; }

    public int? SortOrder { get; set; }

    public virtual ApplicationForm? Form { get; set; }

    public virtual AppraisalQuestion? Question { get; set; }
}
