namespace HospitalApp.Dto
{
    public class PatientDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Room { get; set; }
        public string Gender { get; set; }
        public int? AssignedDoctorId { get; set; }
        public string Status { get; set; }
    }
}
