namespace Engage.Application.Services.CategoryTargetAnswers.Commands;

public class CategoryTargetAnswerInsertCommand : IMapTo<CategoryTargetAnswer>, IRequest<CategoryTargetAnswer>
{
    //public int CategoryTargetAnswerId { get; init; }
    public int CategoryTargetId { get; init; }
    public int EmployeeId { get; init; }
    public int StoreId { get; init; }
    public int CategoryTargetStoreId { get; set; }
    public float? Target { get; init; }
    public float? Available { get; init; }
    public float? Occupied { get; init; }
    public bool IsNotApplicable { get; set; }
    public string TextAnswer { get; set; }
    public int? CategoryTargetTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetAnswerInsertCommand, CategoryTargetAnswer>();

    }
}

public record CategoryTargetAnswerInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetAnswerInsertCommand, CategoryTargetAnswer>
{
    public async Task<CategoryTargetAnswer> Handle(CategoryTargetAnswerInsertCommand command, CancellationToken cancellationToken)
    {
        var CategoryTargetStore = await Context.CategoryTargetStores.Where(e => e.CategoryTargetId == command.CategoryTargetId
             && e.StoreId == command.StoreId)
             .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("No category target answer found");

        var entity = Mapper.Map<CategoryTargetAnswerInsertCommand, CategoryTargetAnswer>(command);

        entity.CategoryTargetStoreId = CategoryTargetStore.CategoryTargetStoreId;

        Context.CategoryTargetAnswers.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CategoryTargetAnswerInsertValidator : AbstractValidator<CategoryTargetAnswerInsertCommand>
{
    public CategoryTargetAnswerInsertValidator()
    {
        //RuleFor(e => e.CategoryTargetAnswerId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CategoryTargetId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.Target).NotEmpty();
        RuleFor(e => e.Available).GreaterThanOrEqualTo(0);
        RuleFor(e => e.Occupied).GreaterThanOrEqualTo(0);
    }
}