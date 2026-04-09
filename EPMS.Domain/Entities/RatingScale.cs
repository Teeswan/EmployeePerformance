using System;
using System.Collections.Generic;

namespace EPMS.Infrastructure;

public partial class RatingScale
{
    public int RatingLevel { get; set; }

    public string Label { get; set; } = null!;

    public string? Description { get; set; }
}
