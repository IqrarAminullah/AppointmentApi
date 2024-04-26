using AppointmentAPI.Models;

namespace AppointmentAPI.ViewModel
{
    public class AppointmentViewModel
    {

        public int AppointmentId { get; set; }

        public int CustomerId { get; set; }

        public DateTime? AppointmentStart { get; set; }
        public DateTime? AppointmentEnd { get; set; }

        public string? Token { get; set; }
        public string? CustomerName { get; set; }
    }
}
