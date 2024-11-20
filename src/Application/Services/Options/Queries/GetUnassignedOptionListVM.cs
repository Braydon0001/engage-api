namespace Engage.Application.Services.Options.Queries;

public class GetUnassignedOptionListVMQuery : GetQuery, IRequest<ListResult<OptionDto>>
{
    public string Option { get; set; }
    public GetUnassignedOptionListVMQuery() { }
    public GetUnassignedOptionListVMQuery(string option)
    {
        Option = option;
    }
}

public class GetUnassignedOptionListVMHandler : IRequestHandler<GetUnassignedOptionListVMQuery, ListResult<OptionDto>>
{
    private readonly IMediator _mediator;

    public GetUnassignedOptionListVMHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<OptionDto>> Handle(GetUnassignedOptionListVMQuery request, CancellationToken cancellationToken)
    {
        var options = await _mediator.Send(new GetUnassignedOptionListQuery(request.Option));

        return new ListResult<OptionDto>
        {
            Data = options,
            Count = options.Count
        };
    }
}

