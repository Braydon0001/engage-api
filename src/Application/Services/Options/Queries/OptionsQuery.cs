using Engage.Application.Services.Orders.Queries;

namespace Engage.Application.Services.Options.Queries;

public class OptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public string Option { get; set; }
    public bool IsRegional { get; set; }

    public OptionsQuery() { }
    public OptionsQuery(string option, bool isRegional = false)
    {
        Option = option;
        IsRegional = isRegional;
    }
}

public class OptionsQueryHandler : IRequestHandler<OptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public OptionsQueryHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<List<OptionDto>> Handle(OptionsQuery request, CancellationToken cancellationToken)
    {
        var optionType = request.Option.ToUpper();

        if (optionType == "ORDERSTORES")
        {
            return await _mediator.Send(new OrderStoresQuery { Search = request.Search }, cancellationToken);
        }
        else if (optionType == "ORDERSUPPLIERS")
        {
            return await _mediator.Send(new OrderSuppliersQuery { Search = request.Search }, cancellationToken);
        }

        var query = OptionUtils.Select(optionType, request.Search, _context);

        return await query.ToListAsync(cancellationToken);
    }
}

