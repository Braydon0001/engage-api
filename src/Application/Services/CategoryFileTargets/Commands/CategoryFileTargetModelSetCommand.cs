namespace Engage.Application.Services.CategoryFileTargets.Commands;

public class CategoryFileTargetModelSetCommand : IMapTo<JsonRule>, IRequest<JsonRule>
{
    public JsonRule Model { get; set; }
    public int Id { get; set; }
}

public record CategoryFileTargetModelSetCommandHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFileTargetModelSetCommand, JsonRule>
{
    public async Task<JsonRule> Handle(CategoryFileTargetModelSetCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.Id, cancellationToken);

        if (entity == null)
        {
            throw new Exception("Entity not found");
        }

        entity.TargetRule = command.Model;

        await Context.SaveChangesAsync(cancellationToken);

        return entity.TargetRule;
    }
}

public class CategoryFileTargetModelSetValidator : AbstractValidator<CategoryFileTargetModelSetCommand>
{
    public CategoryFileTargetModelSetValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}