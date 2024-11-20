using Engage.Application.Services.CategoryTargetAnswerHistories.Commands;

namespace Engage.Application.Services.CategoryTargetAnswers.Commands;

public class CategoryTargetAnswerUpdateCommand : IMapTo<CategoryTargetAnswer>, IRequest<CategoryTargetAnswer>
{
    public int Id { get; set; }
    //public int CategoryTargetAnswerId { get; init; }
    public int CategoryTargetId { get; init; }
    public float? Target { get; init; }
    public float? Available { get; init; }
    public float? Occupied { get; init; }
    public bool IsNotApplicable { get; set; }
    public string TextAnswer { get; set; }
    public int? CategoryTargetTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetAnswerUpdateCommand, CategoryTargetAnswer>();
    }
}

public record CategoryTargetAnswerUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<CategoryTargetAnswerUpdateCommand, CategoryTargetAnswer>
{
    public async Task<CategoryTargetAnswer> Handle(CategoryTargetAnswerUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryTargetAnswers.SingleOrDefaultAsync(e => e.CategoryTargetAnswerId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }
        await Mediator.Send(new CategoryTargetAnswerHistoryInsertCommand()
        {
            CategoryTargetAnswerId = entity.CategoryTargetAnswerId,
            CategoryTargetId = entity.CategoryTargetId,
            CategoryTargetStoreId = entity.CategoryTargetStoreId,
            EmployeeId = entity.EmployeeId.Value,
            Target = entity.Target,
            Available = entity.Available,
            Occupied = entity.Occupied,
            IsNotApplicable = entity.IsNotApplicable,
            TextAnswer = entity.TextAnswer,
            CategoryTargetTypeId = entity.CategoryTargetTypeId ?? command.CategoryTargetTypeId,
        });

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCategoryTargetAnswerValidator : AbstractValidator<CategoryTargetAnswerUpdateCommand>
{
    public UpdateCategoryTargetAnswerValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.CategoryTargetAnswerId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CategoryTargetId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.Target).NotEmpty();
        RuleFor(e => e.Available).GreaterThanOrEqualTo(0);
        RuleFor(e => e.Occupied).GreaterThanOrEqualTo(0);
    }
}