namespace Engage.Application.Services.EmailTemplates.Commands;

public class UpdateEmailTemplateCommand : EmailTemplateCommand, IRequest<OperationStatus>, IMapTo<EmailTemplate>
{
    public int Id { get; set; }
}

public class UpdateEmailTemplateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmailTemplateCommand, OperationStatus>
{
    public UpdateEmailTemplateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmailTemplateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmailTemplates.SingleAsync(x => x.EmailTemplateId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmailTemplateId;
        return opStatus;
    }
}
