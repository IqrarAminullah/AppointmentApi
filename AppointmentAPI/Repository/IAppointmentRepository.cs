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
        Task<int> AddAppointment(AppointmentViewModel appointment);
        Task<int> UpdateAppointment(AppointmentViewModel appointment);
        Task<int> DeleteAppointment(int appointmentId);
        Task<int> AddCustomer(CustomerViewModel customer);
        Task<int> UpdateCustomer(CustomerViewModel customer);        
        Task<int> DeleteCustomer(int customerId);  
    }
}
