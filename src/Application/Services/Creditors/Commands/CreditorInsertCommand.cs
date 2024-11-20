namespace Engage.Application.Services.Creditors.Commands;

public class CreditorInsertCommand : IMapTo<Creditor>, IRequest<Creditor>
{
    public int? CreditorStatusId { get; init; }
    public string Name { get; init; }
    public string TradingName { get; set; }
    public bool IsVatRegistered { get; init; }
    public string VatNumber { get; set; }
    public DateTime BankConfirmationDate { get; set; }
    public IFormFile[] FilesVendorApplication { get; set; }
    public IFormFile[] FilesCipcDocument { get; set; }
    public IFormFile[] FilesVatDocument { get; set; }
    public IFormFile[] FilesBankConfirmation { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorInsertCommand, Creditor>();
    }
}

public record CreditorInsertHandler(IAppDbContext Context, IMapper Mapper, IFileService File) : IRequestHandler<CreditorInsertCommand, Creditor>
{
    public async Task<Creditor> Handle(CreditorInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CreditorInsertCommand, Creditor>(command);
        entity.CreditorStatusId = (int)CreditorStatusId.New;

        Context.Creditors.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status)
        {
            var vendorApplicationFileUpdateCommand = new FileUpdateCommand
            {
                ContainerName = nameof(EmployeePension),
                EntityFiles = entity.Files,
                MaxFiles = 5,
                Files = command.FilesVendorApplication,
                Id = entity.CreditorId,
                FileType = "vendorapplication"
            };

            entity.Files = await File.UpdateAsync(vendorApplicationFileUpdateCommand, cancellationToken);

            var cipcFileUpdateCommand = new FileUpdateCommand
            {
                ContainerName = nameof(EmployeePension),
                EntityFiles = entity.Files,
                MaxFiles = 5,
                Files = command.FilesCipcDocument,
                Id = entity.CreditorId,
                FileType = "cipc"
            };

            entity.Files = await File.UpdateAsync(cipcFileUpdateCommand, cancellationToken);

            var vatDocumentFileUpdateCommand = new FileUpdateCommand
            {
                ContainerName = nameof(EmployeePension),
                EntityFiles = entity.Files,
                MaxFiles = 5,
                Files = command.FilesVatDocument,
                Id = entity.CreditorId,
                FileType = "vat"
            };

            entity.Files = await File.UpdateAsync(vatDocumentFileUpdateCommand, cancellationToken);

            var bankConfirmationFileUpdateCommand = new FileUpdateCommand
            {
                ContainerName = nameof(EmployeePension),
                EntityFiles = entity.Files,
                MaxFiles = 5,
                Files = command.FilesBankConfirmation,
                Id = entity.CreditorId,
                FileType = "bankconfirmation"
            };

            entity.Files = await File.UpdateAsync(bankConfirmationFileUpdateCommand, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);
        }

        return entity;
    }
}

public class CreditorInsertValidator : AbstractValidator<CreditorInsertCommand>
{
    public CreditorInsertValidator()
    {
        RuleFor(e => e.CreditorStatusId);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(300);
        RuleFor(e => e.TradingName).NotEmpty().MaximumLength(300);
        RuleFor(e => e.IsVatRegistered);
        RuleFor(e => e.VatNumber).MaximumLength(200);
        RuleFor(e => e.BankConfirmationDate).NotEmpty();
    }
}