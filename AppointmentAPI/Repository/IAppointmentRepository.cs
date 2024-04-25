using AppointmentAPI.Models;
using AppointmentAPI.ViewModel;

namespace AppointmentAPI.Repository
{
    public interface IAppointmentRepository
    {
        Task<List<AppointmentViewModel>> GetAllAppointments();
        Task<AppointmentViewModel>? GetAppointmentById(int appointmentId);
        Task<List<CustomerViewModel>> GetAllCustomers();
        Task<CustomerViewModel>? GetCustomerById(int customerId);
        Task<List<AppointmentViewModel>>? GetCustomerAppointments(int customerId);
        Task<int> AddAppointment(Appointment appointment);
        Task UpdateAppointment(Appointment appointment);
        Task<int> DeleteAppointment(int appointmentId);
        Task<int> AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);        
        Task<int> DeleteCustomer(int customerId);  
    }
}
