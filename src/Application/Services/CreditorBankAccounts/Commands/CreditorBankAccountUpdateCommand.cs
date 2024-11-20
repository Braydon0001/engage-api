// auto-generated
namespace Engage.Application.Services.CreditorBankAccounts.Commands;

public class CreditorBankAccountUpdateCommand : IMapTo<CreditorBankAccount>, IRequest<CreditorBankAccount>
{
    public int Id { get; set; }
    public int BankNameId { get; set; }
    public int BankAccountTypeId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string BranchCode { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorBankAccountUpdateCommand, CreditorBankAccount>();
    }
}

public class CreditorBankAccountUpdateHandler : UpdateHandler, IRequestHandler<CreditorBankAccountUpdateCommand, CreditorBankAccount>
{
    public CreditorBankAccountUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<CreditorBankAccount> Handle(CreditorBankAccountUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.CreditorBankAccounts.SingleOrDefaultAsync(e => e.CreditorBankAccountId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCreditorBankAccountValidator : AbstractValidator<CreditorBankAccountUpdateCommand>
{
    public UpdateCreditorBankAccountValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.BankNameId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.BankAccountTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.AccountNumber).NotEmpty().MaximumLength(100);
        RuleFor(e => e.BranchCode).NotEmpty().MaximumLength(15);
    }
}