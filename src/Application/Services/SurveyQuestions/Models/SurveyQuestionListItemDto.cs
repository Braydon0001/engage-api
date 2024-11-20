using Engage.Application.Services.SurveyQuestionOptions.Models;

namespace Engage.Application.Services.SurveyQuestions.Models;

public class SurveyQuestionListItemDto : IMapFrom<SurveyQuestion>
{
    public int Id { get; set; }
    public int QuestionTypeId { get; set; }
    public string QuestionType { get; set; }
    public string Question { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsRequired { get; set; }
    public ICollection<OptionDto> QuestionFalseReasons { get; set; }
    public ICollection<SurveyQuestionOptionListItemDto> SurveyQuestionOptions { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyQuestion, SurveyQuestionListItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyQuestionId))
            .ForMember(d => d.QuestionType, opt => opt.MapFrom(s => s.QuestionType.Name))
            .ForMember(d => d.QuestionFalseReasons, opt => opt.MapFrom(d => d.SurveyQuestionFalseReasons
                                                                                .Select(s => s.QuestionFalseReason)
                                                                                .Select(s => new OptionDto()
                                                                                {
                                                                                    Id = s.Id,
                                                                                    Name = s.Name
                                                                                })))
            .ForMember(d => d.SurveyQuestionOptions, opt => opt.MapFrom(d => d.SurveyQuestionOptions
                                                                                .Select(s => new SurveyQuestionOptionListItemDto()
                                                                                {
                                                                                    Id = s.SurveyQuestionOptionId,
                                                                                    DisplayOrder = s.DisplayOrder,
                                                                                    Option = s.Option
                                                                                })));
    }
}
