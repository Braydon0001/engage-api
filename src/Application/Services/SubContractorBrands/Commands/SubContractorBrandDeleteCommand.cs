namespace Engage.Application.Services.SubContractorBrands.Commands;

public class SubContractorBrandDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}
public record SubContractorBrandDeleteHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SubContractorBrandDeleteCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SubContractorBrandDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SubContractorBrands.FirstOrDefaultAsync(e => e.SubContractorBrandId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        Context.SubContractorBrands.Remove(entity);

        return await Context.SaveChangesAsync(cancellationToken);
    }
}

public class SubContractorBrandDeleteValidator : AbstractValidator<SubContractorBrandDeleteCommand>
{
    public SubContractorBrandDeleteValidator()
    {
        RuleFor(e => e.Id).NotNull().GreaterThan(0);
    }
}