namespace Engage.Application.Models;

public class EmailModel<T> where T : class
{
    public EmailTypeId EmailTypeId { get; set; }
    public string ToEmail { get; set; }
    public List<string> CCEmails { get; set; }
    public string Subject { get; set; }
    public T TemplateVariables { get; set; }
    public bool IsSmtp { get; set; }
    public string EmailBody { get; set; }
}
