namespace Engage.Application.Services.Options.Queries;

public class GetEnumOptionListQuery : OptionsQuery, IRequest<List<OptionDto>>
{
    public GetEnumOptionListQuery() { }
    public GetEnumOptionListQuery(string option) : base(option) { }
}

public class GetEnumOptionListHandler : IRequestHandler<GetEnumOptionListQuery, List<OptionDto>>
{
    public async Task<List<OptionDto>> Handle(GetEnumOptionListQuery request, CancellationToken cancellationToken)
    {
        var optionName = request.Option.ToUpper();

        var options = optionName switch
        {
            EnumOptionsDesc.CLAIMSKUSTATUSES => EnumToOptions(typeof(ClaimSkuStatusId)),
            EnumOptionsDesc.CLAIMSTATUSES => EnumToOptions(typeof(ClaimStatusId)),
            EnumOptionsDesc.CLAIMSUPPLIERSTATUSES => EnumToOptions(typeof(ClaimSupplierStatusId)),
            EnumOptionsDesc.ENTITYCONTACTTYPES => EnumToOptions(typeof(EntityContactTypeId)),
            EnumOptionsDesc.LEAVEENTRYSTATUS => EnumToOptions(typeof(LeaveEntryStatus)),
            EnumOptionsDesc.IMPORTROWTYPES => EnumToOptions(typeof(ImportRowType)),
            EnumOptionsDesc.OPTIONTYPES => EnumToOptions(typeof(OptionTypes)),
            EnumOptionsDesc.PROGRESSSTATUSES => EnumToOptions(typeof(ProgressStatus)),
            EnumOptionsDesc.VOUCHERDETAILSTATUSES => EnumToOptions(typeof(VoucherDetailStatusId)),
            EnumOptionsDesc.VOUCHERSTATUSES => EnumToOptions(typeof(VoucherStatusId)),
            _ => throw new UnknownOptionException(optionName),
        };

        return await Task.FromResult(options);
    }

    private List<OptionDto> EnumToOptions(Type enumType)
    {

        var options = new List<OptionDto>();
        foreach (var e in Enum.GetValues(enumType))
        {
            options.Add(new OptionDto { Id = (int)e, Name = e.ToString() });
        }
        return options;
    }
}

