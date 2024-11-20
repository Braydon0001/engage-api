namespace Engage.Application.Services.ClaimSkus.Commands
{
    public class ClaimSkuCommand : IMapTo<ClaimSku>
    {
        public int ClaimId { get; set; }
        public int ClaimSkuTypeId { get; set; }
        public int ClaimQuantityTypeId { get; set; }
        public int ClaimSkuStatusId { get; set; }
        public decimal Amount { get; set; }
        public decimal VatAmount { get; set; }
        public int Quantity { get; set; }
        public int DCProductId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string Note { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ClaimSkuCommand, ClaimSku>();
        }
    }
}
