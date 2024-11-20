namespace Engage.Application.Services.DCDepartments.Models
{
    public class DCDepartmentDto : IMapFrom<DCDepartment>
    {
        public int Id { get; set; }        
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DCDepartment, DCDepartmentDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(d => d.DCDepartmentId));
        }
    }
}
