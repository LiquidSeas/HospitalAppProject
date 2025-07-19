using HospitalApp.Dto;
using HospitalApp.Interfaces;
using HospitalApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;

        public AppointmentsController(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }

        [HttpGet]
        public ActionResult<List<Appointment>> Get()
        {
            var appointments = _patientsRepository.GetTodaysAppointment().ToList();
            return Ok(appointments);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AppointmentUpdateDto appointmentDetail) 
        {
        _patientsRepository.UpdateAppointment(id, appointmentDetail.Status);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Appointment appointment)
        {
            _patientsRepository.AddAppointment(appointment);
            return Ok("Appointment created");
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] string name)
        {
            _patientsRepository.DeleteAppointment(name);
            return Ok("Appointment deleted");
        }
    }
}
