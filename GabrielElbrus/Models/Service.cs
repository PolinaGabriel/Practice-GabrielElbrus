using System;
using System.Collections.Generic;

namespace GabrielElbrus.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceCode { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public float ServicePrice { get; set; }

    public virtual ICollection<SaleService> SaleServices { get; set; } = new List<SaleService>();
}
