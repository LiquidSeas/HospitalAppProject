using Dapper;
using HospitalApp.Interfaces;
using HospitalApp.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace HospitalApp.Repository
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly string _connectionString;

        public PatientsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(_connectionString);
        }

        public ICollection<Patient> GetPatients()
        {
            try
            {
                string sqlQuery = "SELECT * FROM dbo.Patients";

                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var patients = dbConnection.Query<Patient>(sqlQuery).ToList();
                    return patients;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching for patient", ex);
            }
        }
        public void RegisterPatient(Patient patient)
        {
            try
            {
                string sqlQuery = @"Insert into dbo.Patients (Name,Phone,Email,Room,Gender,AssignedDoctorId)
                                   values(@Name,@Phone,@Email,@Room,@Gender,@AssignedDoctorId)";
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {

                    dbConnection.Execute(sqlQuery, patient);

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Patient");

            }
        }
        public Patient SearchPatient(int id)
        {
            try
            {
                string sqlQuery = "SELECT * FROM dbo.Patients WHERE Id = @Id";

                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    return dbConnection.QueryFirstOrDefault<Patient>(sqlQuery, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching for patient", ex);
            }
        }

        public Patient SearchPatient(string name)
        {
            try
            {
                string sqlQuery = "SELECT * FROM dbo.Patients WHERE Name = @Name";

                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    return dbConnection.QueryFirstOrDefault<Patient>(sqlQuery, new { Name = name });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching for patient", ex);
            }
        }
        public void UpdatePatientsRoom(int id, int room)
        {
            try
            {
                string sqlQuery = @"UPDATE dbo.Patients 
                            SET Room = @Room 
                            WHERE Id = @Id";

                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Execute(sqlQuery, new { Id = id, Room = room });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating patient's room", ex);
            }

        }
        public ICollection<Appointment> GetPatientHistory(string name)
        {
            try
            {
                string sqlQuery = @"select * from Appointments where PatientName like name%";
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {

                    var history = dbConnection.Query<Appointment>(sqlQuery, name).ToList();
                    return history;
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Error No patient with name {name} room", ex);
            }
        }

        //appointments
        public void UpdateAppointment(int id,string status)
        {
            try
            {
                string query = @"UPDATE dbo.Appointments 
                     SET Status = @Status 
                     WHERE Id = @Id";

                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var parameters = new { Id = id, Status = status };
                    dbConnection.Execute(query, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not update appointment status", ex);
            }

        }
        public void AddAppointment(Appointment appointment)
        {
            try
            {
                string query = @"Insert into dbo.Appointments(PatientName,AppointmentDate,Department,Status,Email)
                            values(@PatientName,GETDATE(),@Department,@Status,@Email)";
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Execute(query, appointment);

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Appointment could not be added", ex);

            }


        }
        public void DeleteAppointment(string name)
        {
            try
            {
                string sqlQuery = @"UPDATE Appointments SET Status = 'Deleted' 
            WHERE PatientName=@Name";
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Execute(sqlQuery, new { Name = name });
                }

            }
            catch
            {
                throw new Exception($"Could not find the Appointment with the name {name}");
            }
        }
        public ICollection<Appointment> GetTodaysAppointment()
        {
            try
            {


                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string sqlQuery = @"SELECT * FROM Appointments WHERE Status like 'Sche%'";
                    var appoinments = dbConnection.Query<Appointment>(sqlQuery).ToList();
                    return appoinments;
                }


            }
            catch
            {
                throw new Exception("No appointments set for today");
            }
        }

        //medical notes
        public void DeleteMedicalNote(int id)
        {
            try
            {
                string query = "DELETE FROM MedicalNotes WHERE Id = @Id";

                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Execute(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting medical note with ID {id}");

            }

        }
        public MedicalNote GetMedicalNote(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM dbo.MedicalNotes WHERE Id = @Id";
                    return dbConnection.QuerySingleOrDefault<MedicalNote>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error fetching medical note with ID {id}: {ex.Message}");
                return null;
            }
        }

        public ICollection<MedicalNote> GetMedicalNote()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM dbo.MedicalNotes";
                    return dbConnection.Query<MedicalNote>(query).ToList();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error fetching medical notes  {ex.Message}");
                return null;
            }
        }
        public void UpdateMedicalNote(MedicalNote medicalNote)
        {
            try
            {
                string sqlQuery = @"Insert into dbo.MedicalNotes(Ailment,Temperature,BMI,CreatedAt,PatientId,DoctorId)Values(@Ailment,@Temperature,@BMI,GETDATE(),@PatientId,@DoctorId)";
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {

                    dbConnection.Execute(sqlQuery, medicalNote);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);


            }

        }
        //announcements
        public ICollection<Announcement> GetAnnouncements()
        {
            try
            {
                string sqlQuery = "select * from dbo.Announcements";
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var announcements = dbConnection.Query<Announcement>(sqlQuery).ToList();
                    return announcements;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching announcements");
            }
        }
        public void UpdateAnnouncement(Announcement announcement)
        {
            try
            {
                string sqlQuery = @"Insert into dbo.Announcements(Title,Content,CreatedAt,TargetGroup)
                                 Values(@Title,@Content,GETDATE(),@TargetGroup)";
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Execute(sqlQuery, announcement);


                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error updating announcements");

            }
        }
    }
}
