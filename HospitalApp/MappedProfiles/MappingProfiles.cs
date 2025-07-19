using HospitalApp.Models;
using AutoMapper;
using HospitalApp.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalApp.MappedProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Patient, PatientDto>();
        }
    }
}
