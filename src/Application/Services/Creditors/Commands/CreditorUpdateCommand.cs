namespace Engage.Application.Services.Creditors.Commands;

public class CreditorUpdateCommand : IMapTo<Creditor>, IRequest<Creditor>
{
    public int Id { get; set; }
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
        profile.CreateMap<CreditorUpdateCommand, Creditor>();
    }
}

public record CreditorUpdateHandler(IAppDbContext Context, IMapper Mapper, IFileService File) : IRequestHandler<CreditorUpdateCommand, Creditor>
{
    public async Task<Creditor> Handle(CreditorUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Creditors.SingleOrDefaultAsync(e => e.CreditorId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

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

        return mappedEntity;
    }
}

public class UpdateCreditorValidator : AbstractValidator<CreditorUpdateCommand>
{
    public UpdateCreditorValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(300);
        RuleFor(e => e.TradingName).NotEmpty().MaximumLength(300);
        RuleFor(e => e.IsVatRegistered);
        RuleFor(e => e.VatNumber).MaximumLength(200);
        RuleFor(e => e.BankConfirmationDate).NotEmpty();
    }
}