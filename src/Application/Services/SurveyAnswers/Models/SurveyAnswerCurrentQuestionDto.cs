namespace Engage.Application.Services.SurveyAnswers.Models;

public class SurveyAnswerCurrentQuestionDto : IMapFrom<SurveyAnswer>
{
    public int QuestionId { get; set; }
    public int? AnswerId { get; set; }
    public string QuestionText { get; set; }
    public string QuestionType { get; set; }
    public bool IsRequired { get; set; }
    public List<OptionDto> SurveyQuestionOptions { get; set; }
    public List<OptionDto> QuestionFalseReasons { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyAnswer, SurveyAnswerCurrentQuestionDto>()
            .ForMember(d => d.QuestionId, opt => opt.MapFrom(s => s.SurveyQuestionId))
            .ForMember(d => d.AnswerId, opt => opt.MapFrom(s => s.SurveyAnswerId))
            .ForMember(d => d.QuestionText, opt => opt.MapFrom(s => s.SurveyQuestion.Question))
            .ForMember(d => d.QuestionType, opt => opt.MapFrom(s => s.SurveyQuestion.QuestionType.Name))
            .ForMember(d => d.IsRequired, opt => opt.MapFrom(s => s.SurveyQuestion.IsRequired))
            .ForMember(d => d.SurveyQuestionOptions, opt => opt.MapFrom(s => s.SurveyQuestion.SurveyQuestionOptions.Select(o =>
                new OptionDto(o.SurveyQuestionOptionId, o.Option))))
            .ForMember(d => d.QuestionFalseReasons, opt => opt.MapFrom(s => s.SurveyQuestion.SurveyQuestionFalseReasons.Select(o =>
                new OptionDto(o.QuestionFalseReasonId, o.QuestionFalseReason.Name))));

    }
}
