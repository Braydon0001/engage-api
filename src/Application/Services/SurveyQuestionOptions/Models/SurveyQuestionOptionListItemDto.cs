namespace Engage.Application.Services.SurveyQuestionOptions.Models;

public class SurveyQuestionOptionListItemDto : IMapFrom<SurveyQuestionOption>
{
    public int Id { get; set; }
    public string Option { get; set; }
    public int DisplayOrder { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyQuestionOption, SurveyQuestionOptionListItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyQuestionOptionId));

    }
}
