namespace Engage.Application.Services.CommunicationTemplates.Commands;

public class CommunicationTemplateInsertCommand : IMapTo<CommunicationTemplate>, IRequest<CommunicationTemplate>
{
    //public string Name { get; set; }
    public string ExternalTemplateId { get; set; }
    public string FromName { get; set; }
    public string FromEmailAddress { get; set; }
    public string FromMobileNumber { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public int CommunicationTemplateTypeId { get; set; }
    public int CommunicationTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTemplateInsertCommand, CommunicationTemplate>();
    }
}

public record CommunicationTemplateInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateInsertCommand, CommunicationTemplate>
{
    public async Task<CommunicationTemplate> Handle(CommunicationTemplateInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CommunicationTemplateInsertCommand, CommunicationTemplate>(command);

        entity.Name = "";
        Context.CommunicationTemplates.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CommunicationTemplateInsertValidator : AbstractValidator<CommunicationTemplateInsertCommand>
{
    public CommunicationTemplateInsertValidator()
    {
        //RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.ExternalTemplateId).MaximumLength(200);
        RuleFor(e => e.FromName).NotEmpty().MaximumLength(200);
        RuleFor(e => e.FromEmailAddress).MaximumLength(200);
        RuleFor(e => e.FromMobileNumber).MaximumLength(15);
        RuleFor(e => e.Subject).MaximumLength(200);
        RuleFor(e => e.Body).NotEmpty().MaximumLength(1000);
        RuleFor(e => e.CommunicationTemplateTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CommunicationTypeId).NotEmpty().GreaterThan(0);
    }
}