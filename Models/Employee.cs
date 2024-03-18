using System;
using System.Collections.Generic;

namespace ManageLeaveAplication.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Unit { get; set; }

    public string? Postion { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? LeaveBalance { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<LeaveRequest> LeaveRequests { get; init; } = new List<LeaveRequest>();
}
