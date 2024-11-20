namespace Engage.Application.Services.TrainingFiles.Commands;

public class TrainingFileFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class TrainingFileFileDeleteHandler : FileDeleteHandler, IRequestHandler<TrainingFileFileDeleteCommand, bool>
{
    public TrainingFileFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(TrainingFileFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var fileType = await _context.TrainingFileTypes.SingleOrDefaultAsync(e => e.Name.ToLower() == command.FileType.ToLower(), cancellationToken);
        var entity = await _context.TrainingFiles.FirstOrDefaultAsync(e => e.TrainingId == command.Id && e.TrainingFileTypeId == fileType.TrainingFileTypeId, cancellationToken);
        //var entity = await _context.TrainingFiles.SingleOrDefaultAsync(e => e.TrainingFileId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(TrainingFile), cancellationToken);

        _context.TrainingFiles.Remove(entity);

        //entity.Files = entity.Files.RemoveFile(command);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class TrainingFileFileDeleteValidator : FileDeleteValidator<TrainingFileFileDeleteCommand>
{
    public TrainingFileFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}