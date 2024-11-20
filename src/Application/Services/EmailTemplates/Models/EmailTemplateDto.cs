namespace Engage.Application.Services.EmailTemplates.Models
{
    public class EmailTemplateDto : IMapFrom<EmailTemplate>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExternalTemplateId { get; set; }
        public string FromEmailName { get; set; }
        public string FromEmailAddress { get; set; }
        public int EmailTemplateTypeId { get; set; }
        public string EmailTemplateTypeName { get; set; }
        public int EmailTypeId { get; set; }
        public string EmailTypeName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmailTemplate, EmailTemplateDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EmailTemplateId))
                .ForMember(d => d.EmailTemplateTypeName, opt => opt.MapFrom(s => s.EmailTemplateType.Name))
                .ForMember(d => d.EmailTypeName, opt => opt.MapFrom(s => s.EmailType.Name));
        }
    }
}
