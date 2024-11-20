namespace Engage.Application.Services.CommunicationTemplateTypes.Commands;

public class CommunicationTemplateTypeUpdateCommand : IMapTo<CommunicationTemplateType>, IRequest<CommunicationTemplateType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTemplateTypeUpdateCommand, CommunicationTemplateType>();
    }
}

public record CommunicationTemplateTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateTypeUpdateCommand, CommunicationTemplateType>
{
    public async Task<CommunicationTemplateType> Handle(CommunicationTemplateTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CommunicationTemplateTypes.SingleOrDefaultAsync(e => e.CommunicationTemplateTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCommunicationTemplateTypeValidator : AbstractValidator<CommunicationTemplateTypeUpdateCommand>
{
    public UpdateCommunicationTemplateTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}