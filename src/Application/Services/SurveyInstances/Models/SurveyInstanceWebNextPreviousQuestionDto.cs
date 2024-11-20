namespace Engage.Application.Services.SurveyInstances.Models;

public class SurveyInstanceWebNextPreviousQuestionDto
{
    public int NextQuestionId { get; set; }
    public int PreviousQuestionId { get; set; }
    public int QuestionNumber { get; set; }
    public int TotalQuestions { get; set; }
    public string SurveyTitle { get; set; }
    public bool IsCompleted { get; set; }
    public string Date { get; set; }
}
