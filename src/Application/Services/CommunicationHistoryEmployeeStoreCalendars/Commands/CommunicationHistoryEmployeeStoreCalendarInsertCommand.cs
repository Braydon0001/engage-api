namespace Engage.Application.Services.CommunicationHistoryEmployeeStoreCalendars.Commands;

public class CommunicationHistoryEmployeeStoreCalendarInsertCommand : IMapTo<CommunicationHistoryEmployeeStoreCalendar>, IRequest<CommunicationHistoryEmployeeStoreCalendar>
{
    public int EmployeeStoreCalendarId { get; set; }
    public int CommunicationTemplateId { get; set; }
    public string ToEmail { get; set; }
    public string FromEmail { get; set; }
    public string FromName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string CcEmails { get; set; }
    public object TemplateData { get; set; }
    public string AttachmentUrls { get; set; }
    public bool HasMemoryStreamAttachment { get; set; }
    public string Error { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationHistoryEmployeeStoreCalendarInsertCommand, CommunicationHistoryEmployeeStoreCalendar>();
    }
}

public record CommunicationHistoryEmployeeStoreCalendarInsertHandler(IAppDbContext Context, IMapper Mapper, IHandlebarsService HandlebarsService) : IRequestHandler<CommunicationHistoryEmployeeStoreCalendarInsertCommand, CommunicationHistoryEmployeeStoreCalendar>
{
    public async Task<CommunicationHistoryEmployeeStoreCalendar> Handle(CommunicationHistoryEmployeeStoreCalendarInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CommunicationHistoryEmployeeStoreCalendarInsertCommand, CommunicationHistoryEmployeeStoreCalendar>(command);

        var template = HandlebarsService.RenderTemplate(command.Body, command.TemplateData);
        var subject = HandlebarsService.RenderTemplate(command.Subject, command.TemplateData);

        entity.Body = template;
        entity.Subject = subject;

        if (entity.Body.Length <= 10000)
        {
            Context.CommunicationHistoryEmployeeStoreCalendars.Add(entity);

            await Context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        return new CommunicationHistoryEmployeeStoreCalendar();
    }
}

public class CommunicationHistoryEmployeeStoreCalendarInsertValidator : AbstractValidator<CommunicationHistoryEmployeeStoreCalendarInsertCommand>
{
    public CommunicationHistoryEmployeeStoreCalendarInsertValidator()
    {
        RuleFor(e => e.EmployeeStoreCalendarId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CommunicationTemplateId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ToEmail).NotEmpty().MaximumLength(200);
        RuleFor(e => e.FromEmail).NotEmpty().MaximumLength(200);
        RuleFor(e => e.FromName).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Subject).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Body).NotEmpty().MaximumLength(10000);
        RuleFor(e => e.CcEmails).MaximumLength(1000);
        RuleFor(e => e.AttachmentUrls).MaximumLength(1000);
        RuleFor(e => e.Error).MaximumLength(1000);
    }
}