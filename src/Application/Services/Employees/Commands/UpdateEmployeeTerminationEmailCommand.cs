using Engage.Application.Services.CommunicationHistoryEmployees.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeTerminationEmailCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public string ManagerName { get; set; }
    public string ManagerEmail { get; set; }
    public string LeaveManagerEmail { get; set; }
    public string TerminationReason { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public DateTime EndDate { get; set; }
    public string TerminatedBy { get; set; }

}
public class UpdateEmployeeTerminationEmailCommandHandler : BaseCreateCommandHandler, IRequestHandler<UpdateEmployeeTerminationEmailCommand, OperationStatus>
{
    private readonly IEmailService _email;
    public UpdateEmployeeTerminationEmailCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IEmailService email) : base(context, mapper, mediator)
    {
        _email = email;
    }
    public async Task<OperationStatus> Handle(UpdateEmployeeTerminationEmailCommand command, CancellationToken cancellationToken)
    {
        //send email
        var reciverName = command.ManagerName;

        string endDate = command.EndDate.ToShortDateString();

        //var emailVariables = new TerminationEmailTemplate
        //{
        //    Name = reciverName,
        //    EmployeeName = $"{command.EmployeeName} {command.EmployeeCode}",
        //    TerminationReason = command.TerminationReason,
        //    TerminationDate = endDate,
        //    TerminatedBy = command.TerminatedBy,
        //};

        //await _email.SendEmailTerminationAsync(new EmailModel<TerminationEmailTemplate>
        //{
        //    EmailTypeId = EmailTypeId.EmployeeTermination,
        //    ToEmail = command.ManagerEmail,
        //    CCEmails = new List<string>() { command.LeaveManagerEmail },
        //    Subject = $"Engage Employee Termination of {emailVariables.EmployeeName}",
        //    TemplateVariables = emailVariables,
        //    IsSmtp = false,
        //    EmailBody = EmailBody.GetEmployeeTerminationBody
        //    (reciverName, emailVariables.EmployeeName, emailVariables.TerminationDate,
        //    emailVariables.TerminationReason, emailVariables.TerminatedBy),
        //}, cancellationToken);
        var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.EmployeeTermination &&
                                                                        c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                            .FirstOrDefaultAsync(cancellationToken);
        if (template != null)
        {
            var templateData = new
            {
                Name = reciverName,
                EmployeeName = $"{command.EmployeeName} {command.EmployeeCode}",
                TerminationReason = command.TerminationReason,
                TerminationDate = endDate,
                TerminatedBy = command.TerminatedBy,
            };
            //Save History
            await _mediator.Send(new CommunicationHistoryEmployeeInsertCommand
            {
                EmployeeId = command.EmployeeId,
                CommunicationTemplateId = template.CommunicationTemplateId,
                ToEmail = command.ManagerEmail,
                FromEmail = template.FromEmailAddress,
                FromName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                CcEmails = command.LeaveManagerEmail,
                TemplateData = templateData,
            }, cancellationToken);

            //Send Email
            await _mediator.Send(new SendEmailCommand
            {
                ToEmailAddress = command.ManagerEmail,
                FromEmailAddress = template.FromEmailAddress,
                FromEmailName = template.FromName,
                CcEmailAddresses = new List<string>() { command.LeaveManagerEmail },
                Subject = template.Subject,
                Body = template.Body,
                TemplateData = templateData,
            }, cancellationToken);
        }

        return new OperationStatus
        {
            Status = true
        };
    }
}
