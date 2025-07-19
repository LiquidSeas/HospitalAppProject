namespace HospitalApp.Models
{
    public class MedicalNote
    {
        public int Id { get; set; }
        public string Ailment { get; set; }
        public double? Temperature { get; set; }
        public double? BMI { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PatientId { get; set; }
        //public Patient Patient { get; set; }

        public int? DoctorId { get; set; }
        // public Doctor? Doctor { get; set; }
    }
}