using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models
{
    public class Doctor
    {


        public int Id { get; set; }
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        [Required]
        public required string Department { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<Patient>? AssignedPatients { get; set; }
    }

}
