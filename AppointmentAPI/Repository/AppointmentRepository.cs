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
        private AppointmentDbContext _appointmentDbContext;

        public AppointmentRepository(AppointmentDbContext appointmentDbContext)
        {
            _appointmentDbContext = appointmentDbContext;
        }

        public async Task<int> AddAppointment(AppointmentViewModel appointment)
        {
            if(_appointmentDbContext != null) {
                Appointment a = new();
                a.AppointmentStart = appointment.AppointmentStart;
                a.AppointmentEnd = appointment.AppointmentEnd;
                a.CustomerId = appointment.CustomerId;
                a.Token = appointment.Token;
                await _appointmentDbContext.AddAsync(a);
                await _appointmentDbContext.SaveChangesAsync();

                return a.AppointmentId;
            }
            return 0;
        }

        public async Task<int> AddCustomer(CustomerViewModel customer)
        {
            if (_appointmentDbContext != null) {
                Customer c = new();
                c.CustomerName = customer.CustomerName;
                await _appointmentDbContext.AddAsync(c);
                await _appointmentDbContext.SaveChangesAsync();

                return c.CustomerId;
            }
            return 0;
        }

        public async Task<int> DeleteAppointment(int appointmentId)
        {
            int result = 0;
            if (_appointmentDbContext != null) {
                Appointment a = await _appointmentDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == appointmentId);

                if (a != null)
                {
                    _appointmentDbContext.Appointments.Remove(a);
                    result = await _appointmentDbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task<int> DeleteCustomer(int customerId)
        {
            int result = 0;
            if (_appointmentDbContext != null)
            {
                Customer c = await _appointmentDbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);

                if (c != null)
                {
                    _appointmentDbContext.Customers.Remove(c);
                    result = await _appointmentDbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task<List<AppointmentViewModel>> GetAllAppointments()
        {
            if (_appointmentDbContext != null)
            {
                return await (from a in _appointmentDbContext.Appointments
                              from c in _appointmentDbContext.Customers
                              where a.CustomerId == c.CustomerId
                              select new AppointmentViewModel
                              {
                                  AppointmentId = a.AppointmentId,
                                  AppointmentStart = a.AppointmentStart,
                                  AppointmentEnd = a.AppointmentEnd,
                                  Token = a.Token,
                                  CustomerId = a.CustomerId,
                                  CustomerName = c.CustomerName
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<List<CustomerViewModel>> GetAllCustomers()
        {
            if (_appointmentDbContext != null)
            {
                return await (from c in _appointmentDbContext.Customers
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
            if (_appointmentDbContext != null)
            {
                return await (from a in _appointmentDbContext.Appointments
                              from c in _appointmentDbContext.Customers
                              where a.AppointmentId == appointmentId
                              where a.AppointmentId == c.CustomerId
                              select new AppointmentViewModel
                              {
                                  AppointmentId = a.AppointmentId,
                                  AppointmentStart = a.AppointmentStart,
                                  AppointmentEnd = a.AppointmentEnd,
                                  Token = a.Token,
                                  CustomerId = a.CustomerId,
                                  CustomerName = c.CustomerName
                              }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<AppointmentViewModel>> GetCustomerAppointments(int customerId)
        {
            if (_appointmentDbContext != null)
            {
                return await (from a in _appointmentDbContext.Appointments
                              from c in _appointmentDbContext.Customers
                              where c.CustomerId == customerId
                              where a.AppointmentId == c.CustomerId
                              select new AppointmentViewModel
                              {
                                  AppointmentId = a.AppointmentId,
                                  AppointmentStart = a.AppointmentStart,
                                  AppointmentEnd = a.AppointmentEnd,
                                  Token = a.Token,
                                  CustomerId = a.CustomerId,
                                  CustomerName = c.CustomerName
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<CustomerViewModel> GetCustomerById(int customerId)
        {
            if (_appointmentDbContext != null)
            {
                return await (from c in _appointmentDbContext.Customers
                              where c.CustomerId == customerId
                              select new CustomerViewModel
                              {
                                  CustomerId = c.CustomerId,
                                  CustomerName = c.CustomerName
                              }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<int> UpdateAppointment(AppointmentViewModel appointment)
        {
            if (_appointmentDbContext != null && AppointmentExists(appointment.AppointmentId))
            {
                _appointmentDbContext.Update(appointment);
                await _appointmentDbContext.SaveChangesAsync();
                return appointment.AppointmentId;
            }
            return 0;
        }

        public async Task<int> UpdateCustomer(CustomerViewModel customer)
        {
            if (_appointmentDbContext != null && CustomerExists(customer.CustomerId))
            {
                _appointmentDbContext.Update(customer);
                await _appointmentDbContext.SaveChangesAsync();
                return customer.CustomerId;
            }
            return 0;
         }

        private bool AppointmentExists(int id)
        {
            return (_appointmentDbContext.Appointments?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
        private bool CustomerExists(int id)
        {
            return (_appointmentDbContext.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}