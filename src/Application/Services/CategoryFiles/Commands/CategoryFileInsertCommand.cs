namespace Engage.Application.Services.CategoryFiles.Commands;

public class CategoryFileInsertCommand : IMapTo<CategoryFile>, IRequest<CategoryFile>
{
    public int CategoryFileTypeId { get; init; }
    public string Name { get; init; }
    public string Note { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int? StoreId { get; init; }
    public int? CategoryGroupId { get; init; }
    public int? CategorySubGroupId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryFileInsertCommand, CategoryFile>();
    }
}

public record CategoryFileInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFileInsertCommand, CategoryFile>
{
    public async Task<CategoryFile> Handle(CategoryFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CategoryFileInsertCommand, CategoryFile>(command);

        Context.CategoryFiles.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CategoryFileInsertValidator : AbstractValidator<CategoryFileInsertCommand>
{
    public CategoryFileInsertValidator()
    {
        RuleFor(e => e.CategoryFileTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(300);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
        RuleFor(e => e.StoreId);
        RuleFor(e => e.CategoryGroupId);
        RuleFor(e => e.CategorySubGroupId);
    }
}