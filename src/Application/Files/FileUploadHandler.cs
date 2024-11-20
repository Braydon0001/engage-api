namespace Engage.Application.Files;

public class FileUploadHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IFileService _file;

    public FileUploadHandler(IAppDbContext context, IFileService file)
    {
        _context = context;
        _file = file;
    }
}
