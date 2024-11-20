// auto-generated
namespace Engage.Application.Services.CreditorBankAccounts.Commands;

public class CreditorBankAccountInsertCommand : IMapTo<CreditorBankAccount>, IRequest<CreditorBankAccount>
{
    public int BankNameId { get; set; }
    public int BankAccountTypeId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string BranchCode { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorBankAccountInsertCommand, CreditorBankAccount>();
    }
}

public class CreditorBankAccountInsertHandler : InsertHandler, IRequestHandler<CreditorBankAccountInsertCommand, CreditorBankAccount>
{
    public CreditorBankAccountInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<CreditorBankAccount> Handle(CreditorBankAccountInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreditorBankAccountInsertCommand, CreditorBankAccount>(command);
        
        _context.CreditorBankAccounts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CreditorBankAccountInsertValidator : AbstractValidator<CreditorBankAccountInsertCommand>
{
    public CreditorBankAccountInsertValidator()
    {
        RuleFor(e => e.BankNameId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.BankAccountTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.AccountNumber).NotEmpty().MaximumLength(100);
        RuleFor(e => e.BranchCode).NotEmpty().MaximumLength(15);
    }
}