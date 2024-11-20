namespace Engage.Application.Services.TrainingFiles.Commands;

public class TrainingFileDeleteCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class TrainingFileDeleteHandler : FileDeleteHandler, IRequestHandler<TrainingFileDeleteCommand, bool>
{
    public TrainingFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(TrainingFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingFiles.SingleOrDefaultAsync(e => e.TrainingFileId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null)
        {
            return false;
        }

        var fileCommand = new FileDeleteCommand
        {
            Id = command.Id,
            FileName = entity.Files[0].Name,
            FileType = entity.Files[0].Type,
        };

        await _file.DeleteAsync(fileCommand, nameof(TrainingFile), cancellationToken);

        _context.TrainingFiles.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class TrainingFileDeleteValidator : AbstractValidator<TrainingFileDeleteCommand>
{
    public TrainingFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}