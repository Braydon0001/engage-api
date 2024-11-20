using Engage.Application.Services.CommunicationHistoryEmployeeStoreCalendars.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarManagerEmailCommand : IRequest<OperationStatus>
{
    public int EmployeeStoreCalendarId { get; set; }
    public string ManagerName { get; set; }
}
public class EmployeeStoreCalendarManagerEmailHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeStoreCalendarManagerEmailCommand, OperationStatus>
{
    private readonly IEmailService _email;
    public EmployeeStoreCalendarManagerEmailHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IEmailService emailService) : base(context, mapper, mediator)
    {
        _email = emailService;
    }

    public async Task<OperationStatus> Handle(EmployeeStoreCalendarManagerEmailCommand request, CancellationToken cancellationToken)
    {
        var storeVisit = await _context.EmployeeStoreCalendars.Where(e => e.EmployeeStoreCalendarId == request.EmployeeStoreCalendarId)
                                                              .Include(e => e.Store)
                                                              .FirstOrDefaultAsync(cancellationToken);

        var employeeAddress = await _context.Employees.Where(e => e.EmployeeId == storeVisit.EmployeeId)
                                                      .Select(e => e.EmailAddress1)
                                                      .FirstOrDefaultAsync(cancellationToken);

        //var emailVariables = new EmployeeStoreCalendarManagerEmailTemplate
        //{
        //    EmployeeStoreCalendarId = request.EmployeeStoreCalendarId,
        //    Name = request.ManagerName,
        //    CalendarDate = storeVisit.CalendarDate.ToShortDateString(),
        //    StoreName = storeVisit.Store.Name
        //};

        //return await _email.SendEmailCalendarManagerCreateVisitAsync(new EmailModel<EmployeeStoreCalendarManagerEmailTemplate>
        //{
        //    EmailTypeId = EmailTypeId.StoreVisitEventCreated,
        //    ToEmail = employeeAddress,
        //    Subject = $"New Store Visit at {emailVariables.StoreName}",
        //    TemplateVariables = emailVariables,
        //    CCEmails = new List<string>(),
        //    IsSmtp = false,
        //    EmailBody = EmailBody.GetEmployeeStoreCalendarManagerStoreVisit(request.ManagerName, emailVariables.StoreName, emailVariables.CalendarDate)
        //}, cancellationToken);

        var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.EmployeeStoreCalendarManagerCreateStoreVisit &&
                                                                            c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                                .FirstOrDefaultAsync(cancellationToken);
        if (template != null)
        {
            var templateData = new
            {
                EmployeeStoreCalendarId = request.EmployeeStoreCalendarId,
                Name = request.ManagerName,
                CalendarDate = storeVisit.CalendarDate.ToShortDateString(),
                StoreName = storeVisit.Store.Name
            };
            //Save History
            await _mediator.Send(new CommunicationHistoryEmployeeStoreCalendarInsertCommand
            {
                EmployeeStoreCalendarId = request.EmployeeStoreCalendarId,
                CommunicationTemplateId = template.CommunicationTemplateId,
                ToEmail = employeeAddress,
                FromEmail = template.FromEmailAddress,
                FromName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                TemplateData = templateData,
            }, cancellationToken);

            //Send Email
            await _mediator.Send(new SendEmailCommand
            {
                ToEmailAddress = employeeAddress,
                FromEmailAddress = template.FromEmailAddress,
                FromEmailName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                TemplateData = templateData,
            }, cancellationToken);
        }

        return new OperationStatus { Status = true };
    }
}
