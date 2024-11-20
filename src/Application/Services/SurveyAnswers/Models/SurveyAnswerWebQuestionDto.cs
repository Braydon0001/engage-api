namespace Engage.Application.Services.SurveyAnswers.Models;

public class SurveyAnswerWebQuestionDto : IMapFrom<SurveyAnswer>
{
    public int Id { get; set; }
    public int SurveyInstanceId { get; set; }
    public int SurveyQuestionId { get; set; }
    public string Answer { get; set; }
    public QuestionFalseReason QuestionFalseReason { get; set; }
    public bool? BooleanAnswer { get; set; }
    public List<OptionDto> SurveyOptions { get; set; }
    public List<JsonFile> FilePhotoAnswer { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyAnswer, SurveyAnswerWebQuestionDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyAnswerId))
            .ForMember(d => d.QuestionFalseReason, opt => opt.MapFrom(s => s.QuestionFalseReason))
            .ForMember(d => d.FilePhotoAnswer, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "photoanswer")))
            .ForMember(d => d.BooleanAnswer, opt => opt.Ignore())
            .ForMember(d => d.SurveyOptions, opt => opt.Ignore());
    }
}
