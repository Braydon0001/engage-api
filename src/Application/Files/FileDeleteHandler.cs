namespace Engage.Application.Files;

public class FileDeleteHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IFileService _file;

    public FileDeleteHandler(IAppDbContext context, IFileService file)
    {
        _context = context;
        _file = file;
    }
}
