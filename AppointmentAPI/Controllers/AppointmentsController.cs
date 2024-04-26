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
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentsController(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentViewModel>>> GetAppointments()
        {
          if (_repository == null)
          {
              return NotFound();
          }
          return await _repository.GetAllAppointments();
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentViewModel>> GetAppointment(int id)
        {
          if (_repository == null)
          {
              return NotFound();
          }
            var appointment = await _repository.GetAppointmentById(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppointmentViewModel>> AddAppointment(AppointmentViewModel appointment)
        {
          if (_repository == null)
          {
              return Problem("Repository not found.");
          }
            int id = await _repository.AddAppointment(appointment);
            appointment.AppointmentId = id;
        
            return CreatedAtAction("GetAppointment", new { id = id }, appointment);
        }

        [HttpPost("update")]
        public async Task<ActionResult<AppointmentViewModel>> UpdateAppointment(AppointmentViewModel appointment) { 
            if(_repository == null)
            {
                return Problem("Repository not found.");
            }

            int status = await _repository.UpdateAppointment(appointment);

            return NoContent();
        }
        
        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (_repository == null)
            {
                return NotFound();
            }
            var appointment = await _repository.DeleteAppointment(id);
            if (appointment == 0)
            {
                return NotFound();
            }
        
            return NoContent();
        }
    }
}
