using HospitalApp.Interfaces;
using HospitalApp.Models;
using HospitalApp.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;
        public AnnouncementsController(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        // GET: api/<Announcements>
        [HttpGet]
        public ActionResult<List<Announcement>> Get()
        {
            var announcements = _patientsRepository.GetAnnouncements();
            return Ok(announcements);

        }
        // POST api/<Announcements>
        [HttpPost]
        public void Post([FromBody] Announcement announcement)
        {
            _patientsRepository.UpdateAnnouncement(announcement);
        }



    }
}
