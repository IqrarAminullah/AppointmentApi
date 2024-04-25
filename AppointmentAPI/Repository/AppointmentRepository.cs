using AppointmentAPI.Models;
using AppointmentAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppointmentAPI.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private AppointmentDbContext _AppointmentDbContext;

        public AppointmentRepository(AppointmentDbContext appointmentDbContext)
        {
            _AppointmentDbContext = appointmentDbContext;
        }

        public async Task<int> AddAppointment(Appointment appointment)
        {
            if(_AppointmentDbContext != null) {
                Appointment a = new();
                a.AppointmentId = appointment.AppointmentId;
                a.AppointmentTime = appointment.AppointmentTime;
                a.CustomerId = appointment.CustomerId;
                a.Token = appointment.Token;
                await _AppointmentDbContext.AddAsync(a);
                await _AppointmentDbContext.SaveChangesAsync();

                return a.AppointmentId;
            }
            return 0;
        }

        public async Task<int> AddCustomer(Customer customer)
        {
            if (_AppointmentDbContext != null) {
                Customer c = new();
                c.CustomerId = customer.CustomerId;
                c.CustomerName = customer.CustomerName;
                await _AppointmentDbContext.AddAsync(c);
                await _AppointmentDbContext.SaveChangesAsync();

                return c.CustomerId;
            }
            return 0;
        }

        public async Task<int> DeleteAppointment(int appointmentId)
        {
            int result = 0;
            if (_AppointmentDbContext != null) {
                Appointment a = await _AppointmentDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == appointmentId);

                if (a != null)
                {
                    _AppointmentDbContext.Appointments.Remove(a);
                    result = await _AppointmentDbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task<int> DeleteCustomer(int customerId)
        {
            int result = 0;
            if (_AppointmentDbContext != null)
            {
                Customer c = await _AppointmentDbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);

                if (c != null)
                {
                    _AppointmentDbContext.Customers.Remove(c);
                    result = await _AppointmentDbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task<List<AppointmentViewModel>> GetAllAppointments()
        {
            if (_AppointmentDbContext != null)
            {
                return await (from a in _AppointmentDbContext.Appointments
                              from c in _AppointmentDbContext.Customers
                              where a.CustomerId == c.CustomerId
                              select new AppointmentViewModel
                              {
                                  AppointmentId = a.AppointmentId,
                                  AppointmentTime = a.AppointmentTime,
                                  Token = a.Token,
                                  CustomerId = a.CustomerId,
                                  CustomerName = c.CustomerName
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<List<CustomerViewModel>> GetAllCustomers()
        {
            if (_AppointmentDbContext != null)
            {
                return await (from c in _AppointmentDbContext.Customers
                              select new CustomerViewModel
                              {
                                  CustomerId = c.CustomerId,
                                  CustomerName = c.CustomerName
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<AppointmentViewModel> GetAppointmentById(int appointmentId)
        {
            if (_AppointmentDbContext != null)
            {
                return await (from a in _AppointmentDbContext.Appointments
                              from c in _AppointmentDbContext.Customers
                              where a.AppointmentId == appointmentId
                              where a.AppointmentId == c.CustomerId
                              select new AppointmentViewModel
                              {
                                  AppointmentId = a.AppointmentId,
                                  AppointmentTime = a.AppointmentTime,
                                  Token = a.Token,
                                  CustomerId = a.CustomerId,
                                  CustomerName = c.CustomerName
                              }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<AppointmentViewModel>> GetCustomerAppointments(int customerId)
        {
            if (_AppointmentDbContext != null)
            {
                return await (from a in _AppointmentDbContext.Appointments
                              from c in _AppointmentDbContext.Customers
                              where c.CustomerId == customerId
                              where a.AppointmentId == c.CustomerId
                              select new AppointmentViewModel
                              {
                                  AppointmentId = a.AppointmentId,
                                  AppointmentTime = a.AppointmentTime,
                                  Token = a.Token,
                                  CustomerId = a.CustomerId,
                                  CustomerName = c.CustomerName
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<CustomerViewModel> GetCustomerById(int customerId)
        {
            if (_AppointmentDbContext != null)
            {
                return await (from c in _AppointmentDbContext.Customers
                              where c.CustomerId == customerId
                              select new CustomerViewModel
                              {
                                  CustomerId = c.CustomerId,
                                  CustomerName = c.CustomerName
                              }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            if (_AppointmentDbContext != null)
            {
                _AppointmentDbContext.Update(appointment);
                await _AppointmentDbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            if (_AppointmentDbContext != null)
            {
                _AppointmentDbContext.Update(customer);
                await _AppointmentDbContext.SaveChangesAsync();
            }
         }
    }
}