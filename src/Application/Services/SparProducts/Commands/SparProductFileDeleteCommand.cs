namespace Engage.Application.Services.SparProducts.Commands;

public class SparProductFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record SparProductFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SparProductFileDeleteCommand, bool>
{
    public async Task<bool> Handle(SparProductFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparProducts.SingleOrDefaultAsync(e => e.SparProductId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(SparProduct), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class SparProductFileDeleteValidator : FileDeleteValidator<SparProductFileDeleteCommand>
{
    public SparProductFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}