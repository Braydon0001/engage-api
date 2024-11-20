namespace Engage.Application.Services.ImportFiles
{
    // Commands
    public class ImportFileCommand
    {
        public string FileName { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
    }

    public class CreateImportFileCommand : ImportFileCommand, IMapTo<ImportFile>, IRequest<OperationStatus>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateImportFileCommand, ImportFile>();
        }
    }

    public class UpdateImportFileCommand : ImportFileCommand, IMapTo<ImportFile>, IRequest<OperationStatus>
    {
        public int Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateImportFileCommand, ImportFile>();
        }
    }

    // Handlers
    public class CreateImportFileCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateImportFileCommand, OperationStatus>
    {
        public CreateImportFileCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(CreateImportFileCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateImportFileCommand, ImportFile>(command);
            _context.ImportFiles.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.ImportFileId;
            opStatus.ReturnObject = entity;
            return opStatus;
        }
    }

    public class UpdateImportFileCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateImportFileCommand, OperationStatus>
    {
        public UpdateImportFileCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(UpdateImportFileCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.ImportFiles.FirstOrDefaultAsync(e => e.ImportFileId == command.Id, cancellationToken);
            return await SaveChangesAsync(command, entity, nameof(ImportFile), command.Id, cancellationToken);
        }
    }
}
