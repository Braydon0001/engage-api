namespace Engage.Application.Services.CommunicationTemplates.Commands;

public class CommunicationTemplateUpdateCommand : IMapTo<CommunicationTemplate>, IRequest<CommunicationTemplate>
{
    public int Id { get; set; }
    //public string Name { get; init; }
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
        profile.CreateMap<CommunicationTemplateUpdateCommand, CommunicationTemplate>();
    }
}

public record CommunicationTemplateUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateUpdateCommand, CommunicationTemplate>
{
    public async Task<CommunicationTemplate> Handle(CommunicationTemplateUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CommunicationTemplates.SingleOrDefaultAsync(e => e.CommunicationTemplateId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);
        mappedEntity.Name = "";
        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCommunicationTemplateValidator : AbstractValidator<CommunicationTemplateUpdateCommand>
{
    public UpdateCommunicationTemplateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
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