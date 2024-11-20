namespace Engage.Application.Services.CommunicationHistoryProjects.Commands;

public class CommunicationHistoryProjectInsertCommand : IMapTo<CommunicationHistoryProject>, IRequest<CommunicationHistoryProject>
{
    public int ProjectId { get; set; }
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
        profile.CreateMap<CommunicationHistoryProjectInsertCommand, CommunicationHistoryProject>();
    }
}

public record CommunicationHistoryProjectInsertHandler(IAppDbContext Context, IMapper Mapper, IHandlebarsService HandlebarsService) : IRequestHandler<CommunicationHistoryProjectInsertCommand, CommunicationHistoryProject>
{
    public async Task<CommunicationHistoryProject> Handle(CommunicationHistoryProjectInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CommunicationHistoryProjectInsertCommand, CommunicationHistoryProject>(command);

        var template = HandlebarsService.RenderTemplate(command.Body, command.TemplateData);

        entity.Body = template;

        if (entity.Body.Length <= 10000)
        {
            Context.CommunicationHistoryProjects.Add(entity);

            await Context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        return new CommunicationHistoryProject();
    }
}

public class CommunicationHistoryProjectInsertValidator : AbstractValidator<CommunicationHistoryProjectInsertCommand>
{
    public CommunicationHistoryProjectInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CommunicationTemplateId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ToEmail).NotEmpty().MaximumLength(200);
        RuleFor(e => e.FromEmail).NotEmpty().MaximumLength(200);
        RuleFor(e => e.FromName).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Subject).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Body).NotEmpty().MaximumLength(1000);
        RuleFor(e => e.CcEmails).MaximumLength(1000);
        RuleFor(e => e.AttachmentUrls).MaximumLength(1000);
        RuleFor(e => e.Error).MaximumLength(1000);
    }
}