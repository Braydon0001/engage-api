namespace Engage.Application.Services.EmailTemplates.Commands
{
    public class CreateEmailTemplateCommand : EmailTemplateCommand, IRequest<OperationStatus>
    {
        
    }

    public class CreateEmailTemplateCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmailTemplateCommand, OperationStatus>
    {
        public CreateEmailTemplateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(CreateEmailTemplateCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateEmailTemplateCommand, EmailTemplate>(command);
            _context.EmailTemplates.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.EmailTemplateId;
            return opStatus;
        }
    }
}
