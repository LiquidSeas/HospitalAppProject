namespace HospitalApp.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int Room { get; set; }
        public string Gender { get; set; }
        public int AssignedDoctorId { get; set; }
        public string Status { get; set; }


    }
}
