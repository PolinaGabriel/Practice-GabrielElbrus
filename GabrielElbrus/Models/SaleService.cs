using System;
using System.Collections.Generic;

namespace GabrielElbrus.Models;

public partial class SaleService
{
    public int SaleServiceId { get; set; }

    public int SaleServiceSale { get; set; }

    public int SaleServiceService { get; set; }

    public virtual Sale SaleServiceSaleNavigation { get; set; } = null!;

    public virtual Service SaleServiceServiceNavigation { get; set; } = null!;
}
