using System;
using System.Collections.Generic;

namespace EPMS.Domain.Entities;

public partial class ApplicationForm
{
    public int FormId { get; set; }

    public string? FormName { get; set; }

    public bool? IsActive { get; set; }
}
