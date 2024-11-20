namespace Engage.Application.Services.EmploymentTypes.Models
{
    public class EmploymentTypeDto : IMapFrom<EmploymentType>
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public int? EndDateReminderDays { get; set; }
        public bool Disabled { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmploymentType, EmploymentTypeDto>();
        }
    }
}
