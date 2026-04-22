using System;
using System.Collections.Generic;

namespace EPMS.Domain.Entities;

public partial class RatingScale
{
    public int RatingLevel { get; set; }

    public string Label { get; set; } = null!;

    public string? Description { get; set; }
}
