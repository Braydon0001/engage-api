namespace Engage.Application.Services.SurveyAnswerOptions.Models;

public class SurveyAnswerOptionListItemDto : IMapFrom<SurveyAnswerOption>
{
    public int Id { get; set; }
    public int SurveyAnswerId { get; set; }
    public int SurveyQuestionOptionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyAnswerOption, SurveyAnswerOptionListItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyAnswerOptionId));

    }
}
