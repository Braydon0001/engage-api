namespace Engage.Application.Services.CategoryFiles.Commands;

public class CategoryFileUpdateCommand : IMapTo<CategoryFile>, IRequest<CategoryFile>
{
    public int Id { get; set; }
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
        profile.CreateMap<CategoryFileUpdateCommand, CategoryFile>();
    }
}

public record CategoryFileUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFileUpdateCommand, CategoryFile>
{
    public async Task<CategoryFile> Handle(CategoryFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCategoryFileValidator : AbstractValidator<CategoryFileUpdateCommand>
{
    public UpdateCategoryFileValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
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