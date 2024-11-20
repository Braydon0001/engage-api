namespace Engage.Application.Services.CommunicationTemplateTypes.Commands;

public class CommunicationTemplateTypeInsertCommand : IMapTo<CommunicationTemplateType>, IRequest<CommunicationTemplateType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTemplateTypeInsertCommand, CommunicationTemplateType>();
    }
}

public record CommunicationTemplateTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateTypeInsertCommand, CommunicationTemplateType>
{
    public async Task<CommunicationTemplateType> Handle(CommunicationTemplateTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CommunicationTemplateTypeInsertCommand, CommunicationTemplateType>(command);

        Context.CommunicationTemplateTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CommunicationTemplateTypeInsertValidator : AbstractValidator<CommunicationTemplateTypeInsertCommand>
{
    public CommunicationTemplateTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}