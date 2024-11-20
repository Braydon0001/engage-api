namespace Engage.Application.Services.EntityContacts.Commands;

public class StoreContactFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class StoreContactFileDeleteHandler : FileDeleteHandler, IRequestHandler<StoreContactFileDeleteCommand, bool>
{
    public StoreContactFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(StoreContactFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreContacts.SingleOrDefaultAsync(e => e.EntityContactId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(StoreContact), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class StoreContactFileDeleteValidator : FileDeleteValidator<StoreContactFileDeleteCommand>
{
    public StoreContactFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}