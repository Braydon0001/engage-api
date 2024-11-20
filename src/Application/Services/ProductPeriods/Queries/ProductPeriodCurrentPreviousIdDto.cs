namespace Engage.Application.Services.ProductPeriods.Queries;

public class ProductPeriodCurrentPreviousIdDto
{
    public ProductPeriodCurrentPreviousIdDto(int previousId, int currentId)
    {
        PreviousPeriodId = previousId;
        CurrentPeriodId = currentId;
    }
    public int PreviousPeriodId { get; set; }
    public int CurrentPeriodId { get; set; }
}
