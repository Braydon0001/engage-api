using Engage.Application.Services.CategoryTargetAnswerHistories.Commands;

namespace Engage.Application.Services.CategoryTargetAnswers.Commands;

public class CategoryTargetAnswerUpdateHistoryCommand : IMapTo<CategoryTargetAnswer>, IRequest<OperationStatus>
{

    public int StoreId { get; init; }
    public DateTime ModifiedDate { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetAnswerUpdateHistoryCommand, CategoryTargetAnswer>();
    }
}

public record CategoryTargetAnswerUpdateHistoryHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<CategoryTargetAnswerUpdateHistoryCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(CategoryTargetAnswerUpdateHistoryCommand command, CancellationToken cancellationToken)
    {
        var categoryTargetStoreIds = await Context.CategoryTargetStores.Where(e => e.StoreId == command.StoreId).Select(d => d.CategoryTargetStoreId).ToListAsync(cancellationToken);

        var categoryTargetAnswers = await Context.CategoryTargetAnswers.Where(e => categoryTargetStoreIds.Contains(e.CategoryTargetStoreId)).ToListAsync(cancellationToken);

        if (categoryTargetAnswers.Any())
        {
            foreach (var categoryTargetAnswer in categoryTargetAnswers)
            {
                await Mediator.Send(new CategoryTargetAnswerHistoryInsertCommand()
                {
                    CategoryTargetAnswerId = categoryTargetAnswer.CategoryTargetAnswerId,
                    CategoryTargetId = categoryTargetAnswer.CategoryTargetId,
                    CategoryTargetStoreId = categoryTargetAnswer.CategoryTargetStoreId,
                    EmployeeId = categoryTargetAnswer.EmployeeId.Value,
                    Target = categoryTargetAnswer.Target,
                    Available = categoryTargetAnswer.Available,
                    Occupied = categoryTargetAnswer.Occupied,
                    LastUserVerifiedDate = command.ModifiedDate,
                    IsNotApplicable = categoryTargetAnswer.IsNotApplicable,
                    TextAnswer = categoryTargetAnswer.TextAnswer,
                    SaveChanges = false,
                    CategoryTargetTypeId = categoryTargetAnswer.CategoryTargetTypeId,
                });
                categoryTargetAnswer.LastUserVerifiedDate = command.ModifiedDate;

            }
        }

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status) return opStatus;

        return new OperationStatus { Status = false };
    }
}

public class UpdateCategoryTargetAnswerHistoryValidator : AbstractValidator<CategoryTargetAnswerUpdateHistoryCommand>
{
    public UpdateCategoryTargetAnswerHistoryValidator()
    {
        RuleFor(x => x.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ModifiedDate).NotEmpty();

    }
}