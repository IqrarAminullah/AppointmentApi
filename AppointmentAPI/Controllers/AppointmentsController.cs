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

        // PUT: api/Appointments/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        //{
        //    if (id != appointment.AppointmentId)
        //    {
        //        return BadRequest();
        //    }
        //
        //    _context.Entry(appointment).State = EntityState.Modified;
        //
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AppointmentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //
        //    return NoContent();
        //}

        // POST: api/Appointments
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        //{
        //  if (_context.Appointments == null)
        //  {
        //      return Problem("Entity set 'AppointmentDbContext.Appointments'  is null.");
        //  }
        //    _context.Appointments.Add(appointment);
        //    await _context.SaveChangesAsync();
        //
        //    return CreatedAtAction("GetAppointment", new { id = appointment.AppointmentId }, appointment);
        //}
        //
        //// DELETE: api/Appointments/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAppointment(int id)
        //{
        //    if (_context.Appointments == null)
        //    {
        //        return NotFound();
        //    }
        //    var appointment = await _context.Appointments.FindAsync(id);
        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    _context.Appointments.Remove(appointment);
        //    await _context.SaveChangesAsync();
        //
        //    return NoContent();
        //}
        //
        //private bool AppointmentExists(int id)
        //{
        //    return (_context.Appointments?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        //}
    }
}
