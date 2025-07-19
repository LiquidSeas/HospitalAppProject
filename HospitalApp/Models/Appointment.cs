namespace HospitalApp.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }

        public string Department { get; set; } = string.Empty;

        public string Status { get; set; } = "Scheduled";
        public string Email { get; set; }
    }

}