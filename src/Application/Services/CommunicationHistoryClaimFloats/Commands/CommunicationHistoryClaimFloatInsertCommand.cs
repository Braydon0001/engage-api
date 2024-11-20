namespace Engage.Application.Services.CommunicationHistoryClaimFloats.Commands;

public class CommunicationHistoryClaimFloatInsertCommand : IMapTo<CommunicationHistoryClaimFloat>, IRequest<CommunicationHistoryClaimFloat>
{
    public int ClaimFloatId { get; set; }
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
        profile.CreateMap<CommunicationHistoryClaimFloatInsertCommand, CommunicationHistoryClaimFloat>();
    }
}

public record CommunicationHistoryClaimFloatInsertHandler(IAppDbContext Context, IMapper Mapper, IHandlebarsService HandlebarsService) : IRequestHandler<CommunicationHistoryClaimFloatInsertCommand, CommunicationHistoryClaimFloat>
{
    public async Task<CommunicationHistoryClaimFloat> Handle(CommunicationHistoryClaimFloatInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CommunicationHistoryClaimFloatInsertCommand, CommunicationHistoryClaimFloat>(command);

        var template = HandlebarsService.RenderTemplate(command.Body, command.TemplateData);
        var subject = HandlebarsService.RenderTemplate(command.Subject, command.TemplateData);

        entity.Body = template;
        entity.Subject = subject;

        Context.CommunicationHistoryClaimFloats.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CommunicationHistoryClaimFloatInsertValidator : AbstractValidator<CommunicationHistoryClaimFloatInsertCommand>
{
    public CommunicationHistoryClaimFloatInsertValidator()
    {
        RuleFor(e => e.ClaimFloatId).NotEmpty().GreaterThan(0);
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