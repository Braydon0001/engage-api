namespace Engage.Application.Services.SparSubProducts.Commands;

public class SparSubProductFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record SparSubProductFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SparSubProductFileDeleteCommand, bool>
{
    public async Task<bool> Handle(SparSubProductFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparSubProducts.SingleOrDefaultAsync(e => e.SparSubProductId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(SparSubProduct), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class SparSubProductFileDeleteValidator : FileDeleteValidator<SparSubProductFileDeleteCommand>
{
    public SparSubProductFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}