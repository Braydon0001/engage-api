// auto-generated
namespace Engage.Application.Services.EngageDepartmentCategories.Commands;

public class EngageDepartmentCategoryUpdateCommand : IMapTo<EngageDepartmentCategory>, IRequest<EngageDepartmentCategory>
{
    public int Id { get; set; }
    public int EngageDepartmentId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartmentCategoryUpdateCommand, EngageDepartmentCategory>();
    }
}

public class EngageDepartmentCategoryUpdateHandler : UpdateHandler, IRequestHandler<EngageDepartmentCategoryUpdateCommand, EngageDepartmentCategory>
{
    public EngageDepartmentCategoryUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageDepartmentCategory> Handle(EngageDepartmentCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageDepartmentCategories.SingleOrDefaultAsync(e => e.Id == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEngageDepartmentCategoryValidator : AbstractValidator<EngageDepartmentCategoryUpdateCommand>
{
    public UpdateEngageDepartmentCategoryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(e => e.EngageDepartmentId).GreaterThan(0);
        RuleFor(e => e.Name);
    }
}