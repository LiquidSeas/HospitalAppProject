using HospitalApp.Interfaces;
using HospitalApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalNotesController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;

        public MedicalNotesController(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        [HttpGet]
        public ActionResult<MedicalNote> Get()
        {
            var note = _patientsRepository.GetMedicalNote();
            return note == null
                ? NotFound($"No medical note found")
                : Ok(note);
        }

        [HttpGet("{id}")]
        public ActionResult<MedicalNote> Get(int id)
        {
            var note = _patientsRepository.GetMedicalNote(id);
            return note == null
                ? NotFound($"No medical note found with ID {id}")
                : Ok(note);
        }

        [HttpPost]
        public IActionResult Post([FromBody] MedicalNote medicalNote)
        {
            if (medicalNote == null) return BadRequest("Invalid input");

            _patientsRepository.UpdateMedicalNote(medicalNote);
            return Ok("Medical note updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _patientsRepository.DeleteMedicalNote(id);
            return Ok($"Medical note with ID {id} deleted");
        }
    }
}
