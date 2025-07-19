using HospitalApp.Models;

namespace HospitalApp.Interfaces
{
    public interface IPatientsRepository
    {
        public void AddAppointment(Appointment appointment);

        public void UpdateAppointment(int id, string status);
        public void DeleteAppointment(string name);
        public ICollection<Appointment> GetTodaysAppointment();
        public void RegisterPatient(Patient patient);
        public void UpdatePatientsRoom(int id, int room);
        public ICollection<Patient> GetPatients();
        public Patient SearchPatient(int id);
        public Patient SearchPatient(string name);
        public ICollection<Appointment> GetPatientHistory(string name);





        public ICollection<Announcement> GetAnnouncements();

        public void UpdateAnnouncement(Announcement announcement);

        public MedicalNote GetMedicalNote(int id);
        public ICollection<MedicalNote> GetMedicalNote();
        public void UpdateMedicalNote(MedicalNote medicalNote);
        public void DeleteMedicalNote(int id);

    }
}
