using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentAPI.Models;
using AppointmentAPI.Repository;
using AppointmentAPI.ViewModel;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IAppointmentRepository _repository;

        public CustomersController(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomers()
        {
            if (_repository == null)
            {
                return NotFound();
            }
            return await _repository.GetAllCustomers();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerViewModel>> GetCustomer(int id)
        {
            if (_repository == null)
            {
                return NotFound();
            }
            var Customer = await _repository.GetCustomerById(id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Customer;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> AddCustomer(CustomerViewModel customer)
        {
            if (_repository == null)
            {
                return Problem("Repository not found.");
            }
            int id = await _repository.AddCustomer(Customer);
            if (id == 0) return Problem("Problem adding Customer");
            customer.CustomerId = id;


            return CreatedAtAction("GetCustomer", new { id = id}, customer);
        }

        [HttpPost("update")]
        public async Task<ActionResult<CustomerViewModel>> UpdateCustomer(CustomerViewModel customer)
        {
            if (_repository == null)
            {
                return Problem("Repository not found.");
            }

            int status = await _repository.UpdateCustomer(customer);

            return NoContent();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_repository == null)
            {
                return NotFound();
            }
            var Customer = await _repository.DeleteCustomer(id);
            if (Customer == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
