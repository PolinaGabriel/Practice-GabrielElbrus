using System;
using System.Collections.Generic;

namespace GabrielElbrus.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int EmployeePosition { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeLogin { get; set; } = null!;

    public string EmployeePassword { get; set; } = null!;

    public string EmployeeCode { get; set; } = null!;

    public string EmployeeImage { get; set; } = null!;

    public virtual Position EmployeePositionNavigation { get; set; } = null!;

    public virtual ICollection<Enter> Enters { get; set; } = new List<Enter>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
