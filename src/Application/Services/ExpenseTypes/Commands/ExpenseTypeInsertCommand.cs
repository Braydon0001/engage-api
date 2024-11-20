namespace Engage.Application.Services.ExpenseTypes.Commands;

public class ExpenseTypeInsertCommand : IMapTo<ExpenseType>, IRequest<ExpenseType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ExpenseTypeInsertCommand, ExpenseType>();
    }
}

public record ExpenseTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExpenseTypeInsertCommand, ExpenseType>
{
    public async Task<ExpenseType> Handle(ExpenseTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ExpenseTypeInsertCommand, ExpenseType>(command);
        
        Context.ExpenseTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ExpenseTypeInsertValidator : AbstractValidator<ExpenseTypeInsertCommand>
{
    public ExpenseTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}