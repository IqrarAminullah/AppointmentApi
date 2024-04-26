using AppointmentAPI.Models;

namespace AppointmentAPI.ViewModel
{
    public class AppointmentViewModel
    {

        public int AppointmentId { get; set; }

        public int CustomerId { get; set; }

        public DateTime? AppointmentTime { get; set; }

        public string? Token { get; set; }
        public string? CustomerName { get; set; }

        public AppointmentViewModel(Appointment a) {
            AppointmentId = a.AppointmentId;
            CustomerId = a.CustomerId;
            AppointmentTime = a.AppointmentTime;
            Token = a.Token;
            CustomerName = a.Customer.CustomerName;
        }
    }
}
