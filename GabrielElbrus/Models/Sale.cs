using System;
using System.Collections.Generic;

namespace GabrielElbrus.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public string SaleCode { get; set; } = null!;

    public int SaleCustomer { get; set; }

    public float SaleHours { get; set; }

    public int SaleStatus { get; set; }

    public DateTime SaleStart { get; set; }

    public DateTime? SaleEnd { get; set; }

    public int SaleEmployee { get; set; }

    public virtual Customer SaleCustomerNavigation { get; set; } = null!;

    public virtual Employee SaleEmployeeNavigation { get; set; } = null!;

    public virtual ICollection<SaleService> SaleServices { get; set; } = new List<SaleService>();

    public virtual Status SaleStatusNavigation { get; set; } = null!;
}
