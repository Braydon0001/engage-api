namespace Engage.Application.Services.CategoryFiles.Commands;

public class CategoryFileFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record CategoryFileFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<CategoryFileFileDeleteCommand, bool>
{
    public async Task<bool> Handle(CategoryFileFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(CategoryFile), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class CategoryFileFileDeleteValidator : FileDeleteValidator<CategoryFileFileDeleteCommand>
{
    public CategoryFileFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}