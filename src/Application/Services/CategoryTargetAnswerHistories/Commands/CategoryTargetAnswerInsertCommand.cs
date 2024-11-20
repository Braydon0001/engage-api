namespace Engage.Application.Services.CategoryTargetAnswerHistories.Commands;

public class CategoryTargetAnswerHistoryInsertCommand : IMapTo<CategoryTargetAnswerHistory>, IRequest<CategoryTargetAnswerHistory>
{
    public int CategoryTargetAnswerId { get; init; }
    public int CategoryTargetId { get; init; }
    public int EmployeeId { get; init; }
    public int CategoryTargetStoreId { get; set; }
    public float? Target { get; init; }
    public float? Available { get; init; }
    public float? Occupied { get; init; }
    public DateTime? LastUserVerifiedDate { get; set; }
    public bool IsNotApplicable { get; set; }
    public string TextAnswer { get; set; }
    public bool SaveChanges { get; init; } = true;
    public int? CategoryTargetTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetAnswerHistoryInsertCommand, CategoryTargetAnswerHistory>();

    }
}

public record CategoryTargetAnswerHistoryInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetAnswerHistoryInsertCommand, CategoryTargetAnswerHistory>
{
    public async Task<CategoryTargetAnswerHistory> Handle(CategoryTargetAnswerHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CategoryTargetAnswerHistoryInsertCommand, CategoryTargetAnswerHistory>(command);

        Context.CategoryTargetAnswerHistories.Add(entity);

        if (command.SaveChanges)
        {
            await Context.SaveChangesAsync(cancellationToken);

        }


        return entity;
    }
}

public class CategoryTargetAnswerHistoryInsertValidator : AbstractValidator<CategoryTargetAnswerHistoryInsertCommand>
{
    public CategoryTargetAnswerHistoryInsertValidator()
    {
        RuleFor(e => e.CategoryTargetAnswerId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CategoryTargetId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Target).NotEmpty();
        RuleFor(e => e.Available).GreaterThanOrEqualTo(0);
        RuleFor(e => e.Occupied).GreaterThanOrEqualTo(0);
    }
}