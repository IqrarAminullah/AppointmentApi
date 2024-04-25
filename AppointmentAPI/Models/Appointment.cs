using System;
using System.Collections.Generic;

namespace AppointmentAPI.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int CustomerId { get; set; }

    public DateTime? AppointmentTime { get; set; }

    public string? Token { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
