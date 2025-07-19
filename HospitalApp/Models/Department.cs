using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
    }
}
