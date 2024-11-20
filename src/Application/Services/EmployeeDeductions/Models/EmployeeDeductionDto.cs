using System;
using AutoMapper;
using Engage.Application.Mappings;

namespace Engage.Application.Services.EmployeeDeductions.Models
{
    public class EmployeeDeductionDto : IMapFrom<Domain.Entities.EmployeeDeduction>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DeductionTypeId { get; set; }
        public string DeductionTypeName { get; set; }        
        public int DeductionCycleTypeId { get; set; }
        public string DeductionCycleTypeName { get; set; }
        public DateTime DeductionDate { get; set; }
        public float Amount { get; set; }
        public string Note { get; set; }
        public string Reference { get; set; }
        public bool Disabled { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.EmployeeDeduction, EmployeeDeductionDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EmployeeDeductionId))
                 .ForMember(e => e.DeductionTypeName, opt => opt.MapFrom(d => d.DeductionType.Name))
                .ForMember(e => e.DeductionCycleTypeName, opt => opt.MapFrom(d => d.DeductionCycleType.Name));
        }
    }
}
