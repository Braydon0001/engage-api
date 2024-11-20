namespace Engage.Application.Services.Shared.Commands;

public abstract class BaseDeleteHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IFileService _fileStorage;

    protected BaseDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    protected BaseDeleteHandler(IAppDbContext context, IFileService fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }
}
