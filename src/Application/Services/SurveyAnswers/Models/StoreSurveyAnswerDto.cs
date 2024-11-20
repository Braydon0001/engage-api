namespace Engage.Application.Services.SurveyAnswers.Models;

public class StoreSurveyAnswerDto : IMapFrom<SurveyAnswer>
{
    public int Id { get; set; }
    public int EmployeeStoreSurveyId { get; set; }
    public int SurveyQuestionId { get; set; }
    public int? SurveyQuestionOptionId { get; set; }
    public int? QuestionFalseReasonId { get; set; }
    public string Answer { get; set; }
    public string PhotoLocation { get; set; }
    public int DisplayOrder { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyAnswer, StoreSurveyAnswerDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyAnswerId))
            .ForMember(d => d.DisplayOrder, opt => opt.MapFrom(s => s.SurveyQuestion.DisplayOrder));
    }
}
