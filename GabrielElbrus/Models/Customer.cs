using System;
using System.Collections.Generic;

namespace GabrielElbrus.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerCode { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string CustomerPassport { get; set; } = null!;

    public DateTime CustomerBirth { get; set; }

    public string CustomerAddress { get; set; } = null!;

    public string CustomerLogin { get; set; } = null!;

    public string CustomerPassword { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
