namespace Engage.Application.Services.SurveyInstances.Models;

public class SurveyInstanceWebAllAnswersDto : IMapFrom<SurveyAnswer>
{
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
    public string Question { get; set; }
    public string QuestionType { get; set; }
    public string Answer { get; set; }
    public List<string> AnswerOptions { get; set; }
    public string QuestionFalseReason { get; set; }
    public List<JsonFile> FilePhotoAnswer { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyAnswer, SurveyInstanceWebAllAnswersDto>()
            .ForMember(d => d.QuestionId, opt => opt.MapFrom(s => s.SurveyQuestionId))
            .ForMember(d => d.AnswerId, opt => opt.MapFrom(s => s.SurveyAnswerId))
            .ForMember(d => d.Question, opt => opt.MapFrom(s => s.SurveyQuestion.Question))
            .ForMember(d => d.QuestionType, opt => opt.MapFrom(s => s.SurveyQuestion.QuestionType.Name))
            .ForMember(d => d.Answer, opt => opt.MapFrom(s => s.Answer))
            .ForMember(d => d.AnswerOptions, opt => opt.Ignore())
            .ForMember(d => d.FilePhotoAnswer, opt => opt.MapFrom(s => s.Files))
            .ForMember(d => d.QuestionFalseReason, opt => opt.MapFrom(s =>
                            s.QuestionFalseReasonId != null ? s.QuestionFalseReason.Name : ""));

    }
}
