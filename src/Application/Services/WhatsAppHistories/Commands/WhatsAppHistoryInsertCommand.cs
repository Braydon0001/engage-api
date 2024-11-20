namespace Engage.Application.Services.WhatsAppHistories.Commands;

public class WhatsAppHistoryInsertCommand : IMapTo<WhatsAppHistory>, IRequest<WhatsAppHistory>
{
    public string ToMobileNumber { get; set; }
    public string FromMobileNumber { get; set; }
    public string FromName { get; set; }
    public string Message { get; set; }
    public string ContentVariables { get; set; }
    public string ExternalTemplateId { get; set; }
    public string AttachmentUrls { get; set; }
    public string Error { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WhatsAppHistoryInsertCommand, WhatsAppHistory>();
    }
}

public record WhatsAppHistoryInsertHandler(IAppDbContext Context, IMapper Mapper, IHandlebarsService HandlebarsService) : IRequestHandler<WhatsAppHistoryInsertCommand, WhatsAppHistory>
{
    public async Task<WhatsAppHistory> Handle(WhatsAppHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<WhatsAppHistoryInsertCommand, WhatsAppHistory>(command);

        Context.WhatsAppHistories.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class WhatsAppHistoryInsertValidator : AbstractValidator<WhatsAppHistoryInsertCommand>
{
    public WhatsAppHistoryInsertValidator()
    {
        RuleFor(e => e.ToMobileNumber).NotEmpty().MaximumLength(20);
        RuleFor(e => e.FromMobileNumber).NotEmpty().MaximumLength(20);
        RuleFor(e => e.FromName).MaximumLength(200);
        RuleFor(e => e.ExternalTemplateId).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Message).MaximumLength(1000);
        RuleFor(e => e.ContentVariables).MaximumLength(1000);
        RuleFor(e => e.AttachmentUrls).MaximumLength(1000);
        RuleFor(e => e.Error).MaximumLength(1000);
    }
}