using AutoMapper;
using BeneficiarioAPI.DTOs;
using BeneficiarioAPI.Models;

namespace BeneficiarioAPI.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<BeneficiarioAddDTO, Beneficiario>();
            CreateMap<BeneficiarioUpdateDTO, Beneficiario>();
        }
        
    }
}
