using AutoMapper;
using HospitalApp.Dto;
using HospitalApp.Interfaces;
using HospitalApp.Models;
using HospitalApp.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        IPatientsRepository _patientsRepository;
        IMapper _mapper;
        public PatientsController(IPatientsRepository patientsRepository, IMapper imapper)
        {
            _patientsRepository = patientsRepository;
            _mapper = imapper;
        }
        // GET: api/<PatientsController>
        [HttpGet]
        public ActionResult<List<PatientDto>> Get()
        {
            var patients = _mapper.Map<List<PatientDto>>(_patientsRepository.GetPatients().ToList());
            return Ok(patients);
        }
        [HttpGet("by-name/{name}")]
        public ActionResult<Patient> Get(string name)
        {
            var patient = _patientsRepository.SearchPatient(name);
            
            return Ok(patient);

        }

        // GET api/<PatientsController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Patient> Get(int id)
        {
            var patient = _patientsRepository.SearchPatient(id);
            return Ok(patient);
        }

        //[HttpGet("history")]
        //public ActionResult<ICollection<Appointment>> Get([FromBody] string name)
        //{
        //    var history = _patientsRepository.GetPatientHistory(name).ToList();
        //    return Ok(history);


        //}
        // POST api/<PatientsController>
        [HttpPost]
        public void Post([FromBody] Patient patient)
        {
            _patientsRepository.RegisterPatient(patient);
            Ok(patient);
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] int room)
        {
            _patientsRepository.UpdatePatientsRoom(id, room);
            Ok();
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
