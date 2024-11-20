using Engage.Application.Services.SurveyFormQuestionTypes.Queries;

namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarSurveySubmissionVm : IMapFrom<SurveyFormSubmission>
{
    public int Id { get; set; }
    public int SurveyFormSubmissionId { get; set; }
    public string Title { get; set; }
    public DateTime? StartDate { get; set; }
    public bool IsCompleted { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<EmployeeStoreCalendarSurveyFormSubmissionGroupVm> Groups { get; set; }
    public List<EmployeeStoreCalendarAnswerVm> Answers { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmission, EmployeeStoreCalendarSurveySubmissionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormId))
               .ForMember(d => d.Title, opt => opt.MapFrom(s => s.SurveyForm.Title))
               .ForMember(d => d.Groups, opt => opt.MapFrom(s => s.SurveyForm.SurveyFormQuestionGroups))
               .ForMember(d => d.Answers, opt => opt.MapFrom(s => s.SurveyFormAnswers))
               .ForMember(d => d.IsCompleted, opt => opt.MapFrom(s => s.IsComplete))
               .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.SurveyForm.StartDate))
               .ForMember(d => d.Files, opt => opt.MapFrom(s => s.SurveyForm.Files));

        profile.CreateMap<SurveyForm, EmployeeStoreCalendarSurveySubmissionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormId))
               .ForMember(d => d.Groups, opt => opt.MapFrom(s => s.SurveyFormQuestionGroups));
        //.ForMember(d => d.Answers, opt => opt.MapFrom(s => s.SurveyFormQuestionGroups.Select(x => x.SurveyFormQuestions).ToList()));
    }
}

public class EmployeeStoreCalendarSurveyFormSubmissionGroupVm : IMapFrom<SurveyFormQuestionGroup>
{
    public int Id { get; set; }
    public int DisplayOrder { get; set; }
    public int SurveyFormId { get; set; }
    public bool IsRequired { get; set; }
    public List<JsonRule> Rules { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonSetting> Settings { get; set; }
    public List<EmployeeStoreCalendarQuestionAnswerVm> Questions { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroup, EmployeeStoreCalendarSurveyFormSubmissionGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionGroupId))
               .ForMember(d => d.IsRequired, opt => opt.MapFrom(s => s.IsRequired))
               //.ForMember(d => d.Questions, opt => opt.MapFrom(s => s.SurveyFormQuestions))
               .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.SurveyFormQuestions));
    }
}

public class EmployeeStoreCalendarQuestionAnswerVm : IMapFrom<SurveyFormAnswer>, IMapFrom<SurveyFormQuestion>
{
    public int? Id { get; set; }
    public int? AnswerId { get; set; }
    public int DisplayOrder { get; set; }
    public int SurveyFormQuestionGroupId { get; set; }
    public SurveyFormQuestionTypeOption SurveyFormQuestionTypeId { get; set; }
    public string QuestionText { get; set; }
    public string AnswerText { get; set; }
    public bool IsRequired { get; set; }
    public bool IsReasonRequired { get; set; }
    public DateTime? MinDateTime { get; set; }
    public DateTime? MaxDateTime { get; set; }
    public List<EmployeeStoreCalendarQuestionOption> AnswerOptions { get; set; }
    public List<EmployeeStoreCalendarQuestionOption> Answers { get; set; }
    public List<EmployeeStoreCalendarQuestionOption> AnswerReasons { get; set; }
    public List<JsonRule> Rules { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonSetting> Metadata { get; set; }
    public List<JsonFile> AnswerFiles { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswer, EmployeeStoreCalendarQuestionAnswerVm>()
               .ForMember(d => d.Id, src => src.MapFrom(s => s.SurveyFormQuestionId))
               .ForMember(d => d.AnswerId, src => src.MapFrom(s => s.SurveyFormAnswerId))
               .ForMember(d => d.DisplayOrder, src => src.MapFrom(s => s.SurveyFormQuestion.DisplayOrder))
               .ForMember(d => d.SurveyFormQuestionGroupId, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionGroupId))
               .ForMember(d => d.SurveyFormQuestionTypeId, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionType))
               .ForMember(d => d.IsRequired, src => src.MapFrom(s => s.SurveyFormQuestion.IsRequired))
               .ForMember(d => d.IsReasonRequired, src => src.MapFrom(s => s.SurveyFormQuestion.IsReasonRequired))
               .ForMember(d => d.Answers, opt => opt.MapFrom(s => s.SurveyFormAnswerOptions))
               .ForMember(d => d.AnswerOptions, opt => opt.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionOptions))
               //.ForMember(d => d.SurveyFormQuestionTypeName, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionType.Name))
               .ForMember(d => d.QuestionText, src => src.MapFrom(s => s.SurveyFormQuestion.QuestionText))
               .ForMember(d => d.Rules, src => src.MapFrom(s => s.SurveyFormQuestion.Rules))
               .ForMember(d => d.AnswerReasons, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionReasons))
               .ForMember(d => d.AnswerFiles, src => src.MapFrom(s => s.Files))
               .ForMember(d => d.Disabled, src => src.MapFrom(s => s.SurveyFormQuestion.Disabled));

        profile.CreateMap<SurveyFormQuestion, EmployeeStoreCalendarQuestionAnswerVm>()
               .ForMember(d => d.Id, src => src.MapFrom(s => s.SurveyFormQuestionId))
               .ForMember(d => d.AnswerOptions, opt => opt.MapFrom(s => s.SurveyFormQuestionOptions))
               //.ForMember(d => d.AnswerOptions, opt => opt.Ignore())
               .ForMember(d => d.Answers, opt => opt.Ignore())
               .ForMember(d => d.AnswerReasons, opt => opt.MapFrom(s => s.SurveyFormQuestionReasons))
               .ForMember(d => d.SurveyFormQuestionTypeId, src => src.MapFrom(s => s.SurveyFormQuestionType));
        //.ForMember(d => d.)
    }
}

public class EmployeeStoreCalendarAnswerVm : IMapFrom<SurveyFormAnswer>
{
    public int Id { get; set; }
    public int DisplayOrder { get; set; }
    public int SurveyFormSubmissionId { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public SurveyFormQuestionTypeOption SurveyFormQuestionTypeId { get; set; }
    public List<EmployeeStoreCalendarQuestionOption> AnswerOptions { get; set; }
    public EmployeeStoreCalendarQuestionOption AnswerReason { get; set; }
    public string AnswerText { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonSetting> Metadata { get; set; }
    public DateTime? AnswerDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswer, EmployeeStoreCalendarAnswerVm>()
               .ForMember(d => d.Id, src => src.MapFrom(s => s.SurveyFormAnswerId))
               .ForMember(d => d.DisplayOrder, src => src.MapFrom(s => s.SurveyFormQuestion.DisplayOrder))
               .ForMember(d => d.SurveyFormQuestionTypeId, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionType))
               .ForMember(d => d.SurveyFormQuestionTypeId, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionType))
               .ForMember(d => d.AnswerOptions, src => src.MapFrom(s => s.SurveyFormAnswerOptions));
        //.ForMember(d => d.SurveyFormQuestionTypeName, src => src.MapFrom(s => s.SurveyFormQuestion.SurveyFormQuestionType.Name))
        //.ForMember(d => d.QuestionText, src => src.MapFrom(s => s.SurveyFormQuestion.QuestionText))

    }
}

public class EmployeeStoreCalendarQuestionOption : IMapFrom<SurveyFormQuestionOption>, IMapFrom<SurveyFormOption>, IMapFrom<SurveyFormQuestionReason>, IMapFrom<SurveyFormAnswerOption>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool CompleteSurvey { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionOption, EmployeeStoreCalendarQuestionOption>()
               .ForMember(e => e.Id, opt => opt.MapFrom(s => s.SurveyFormOptionId))
               .ForMember(e => e.Name, opt => opt.MapFrom(s => s.SurveyFormOption.Name))
               .ForMember(d => d.CompleteSurvey, opt => opt.MapFrom(s => s.SurveyFormOption.CompleteSurvey));

        profile.CreateMap<SurveyFormOption, EmployeeStoreCalendarQuestionOption>()
               .ForMember(e => e.Id, opt => opt.MapFrom(s => s.SurveyFormOptionId))
               .ForMember(e => e.Name, opt => opt.MapFrom(s => s.Name));

        profile.CreateMap<SurveyFormQuestionReason, EmployeeStoreCalendarQuestionOption>()
               .ForMember(e => e.Id, opt => opt.MapFrom(s => s.SurveyFormReasonId))
               .ForMember(e => e.Name, opt => opt.MapFrom(s => s.SurveyFormReason.Name));

        profile.CreateMap<SurveyFormAnswerOption, EmployeeStoreCalendarQuestionOption>()
               .ForMember(e => e.Id, opt => opt.MapFrom(s => s.SurveyFormOption.SurveyFormOptionId))
               .ForMember(e => e.Name, opt => opt.MapFrom(s => s.SurveyFormOption.Name))
               .ForMember(d => d.CompleteSurvey, opt => opt.MapFrom(s => s.SurveyFormOption.CompleteSurvey));
    }
}