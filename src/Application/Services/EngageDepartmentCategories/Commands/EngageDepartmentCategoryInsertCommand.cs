// auto-generated
namespace Engage.Application.Services.EngageDepartmentCategories.Commands;

public class EngageDepartmentCategoryInsertCommand : IMapTo<EngageDepartmentCategory>, IRequest<EngageDepartmentCategory>
{
    public int EngageDepartmentId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartmentCategoryInsertCommand, EngageDepartmentCategory>();
    }
}

public class EngageDepartmentCategoryInsertHandler : InsertHandler, IRequestHandler<EngageDepartmentCategoryInsertCommand, EngageDepartmentCategory>
{
    public EngageDepartmentCategoryInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageDepartmentCategory> Handle(EngageDepartmentCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EngageDepartmentCategoryInsertCommand, EngageDepartmentCategory>(command);
        _context.EngageDepartmentCategories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EngageDepartmentCategoryInsertValidator : AbstractValidator<EngageDepartmentCategoryInsertCommand>
{
    public EngageDepartmentCategoryInsertValidator()
    {
        RuleFor(e => e.EngageDepartmentId).GreaterThan(0);
        RuleFor(e => e.Name);
    }
}