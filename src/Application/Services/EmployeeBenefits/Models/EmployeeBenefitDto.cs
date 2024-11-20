using System;
using AutoMapper;
using Engage.Application.Mappings;

namespace Engage.Application.Services.EmployeeBenefits.Models
{
    public class EmployeeBenefitDto : IMapFrom<Domain.Entities.EmployeeBenefit>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int BenefitTypeId { get; set; }
        public string BenefitTypeName { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public bool Disabled { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.EmployeeBenefit, EmployeeBenefitDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EmployeeBenefitId))
                .ForMember(e => e.BenefitTypeName, opt => opt.MapFrom(d => d.BenefitType.Name));
        }
    }
}
