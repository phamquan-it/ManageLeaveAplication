﻿using System;
using System.Collections.Generic;

namespace ManageLeaveAplication.Models;

public partial class LeaveRequest
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Reason { get; set; }

    public string? Status { get; set; }

    public virtual Employee? Employee { get; init; }
}
