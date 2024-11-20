namespace Engage.Application.Services.ExpenseTypes.Commands;

public class ExpenseTypeUpdateCommand : IMapTo<ExpenseType>, IRequest<ExpenseType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ExpenseTypeUpdateCommand, ExpenseType>();
    }
}

public record ExpenseTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExpenseTypeUpdateCommand, ExpenseType>
{
    public async Task<ExpenseType> Handle(ExpenseTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ExpenseTypes.SingleOrDefaultAsync(e => e.ExpenseTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateExpenseTypeValidator : AbstractValidator<ExpenseTypeUpdateCommand>
{
    public UpdateExpenseTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}