using System;
using System.Collections.Generic;

namespace GabrielElbrus.Models;

public partial class Enter
{
    public int EnterId { get; set; }

    public DateTime EnterTime { get; set; }

    public int EnterEmployee { get; set; }

    public bool EnterResult { get; set; }

    public virtual Employee EnterEmployeeNavigation { get; set; } = null!;
}
