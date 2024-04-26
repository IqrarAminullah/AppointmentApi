using AppointmentAPI.Models;

namespace AppointmentAPI.ViewModel
{
    public class CustomerViewModel
    {

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public CustomerViewModel(Customer c) {
            CustomerId = c.CustomerId;
            CustomerName = c.CustomerName;
        }
    }
}
