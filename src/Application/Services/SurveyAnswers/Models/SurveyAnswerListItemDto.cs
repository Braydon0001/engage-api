namespace Engage.Application.Services.SurveyAnswers.Models;

public class SurveyAnswerListItemDto : IMapFrom<SurveyAnswer>
{
    public int Id { get; set; }
    public string Answer { get; set; }
    public string Question { get; set; }
    public int DisplayOrder { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyAnswer, SurveyAnswerListItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyAnswerId))
            .ForMember(d => d.Question, opt => opt.MapFrom(s => s.SurveyQuestion.Question))
            .ForMember(d => d.DisplayOrder, opt => opt.MapFrom(s => s.SurveyQuestion.DisplayOrder));
    }
}
