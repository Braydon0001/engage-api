namespace Engage.Application.Services.Shared.Commands;

public class DeleteCommand : IRequest<OperationStatus>
{
    public string EntityName { get; set; }
    public int Id { get; set; }
    public bool Undo { get; set; } = false;
    public bool Toggle { get; set; } = false;
}

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;
    private readonly IDateTimeService _dateTime;

    public DeleteCommandHandler(IAppDbContext context, IDateTimeService dateTime, IUserService user)
    {
        _context = context;
        _dateTime = dateTime;
        _user = user;
    }

    public async Task<OperationStatus> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await DeleteCommandUtils.FindAsync(request.Id, request.EntityName, _context, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(request.EntityName, request.Id);
        }

        if (request.Undo)
        {
            entity.Deleted = false;
            entity.DeletedDate = null;
            entity.DeletedBy = null;
        }
        else if (request.Toggle)
        {
            entity.Disabled = !entity.Disabled;
        }
        else
        {
            entity.Deleted = true;
            entity.DeletedDate = _dateTime.Now;
            if (!string.IsNullOrWhiteSpace(_user.UserName))
            {
                entity.DeletedBy = _user.UserName;
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        opStatus.ReturnObject = entity;
        return opStatus;
    }
}
