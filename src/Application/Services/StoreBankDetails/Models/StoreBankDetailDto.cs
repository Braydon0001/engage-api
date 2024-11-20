namespace Engage.Application.Services.StoreBankDetails.Models
{
    public class StoreBankDetailDto : IMapFrom<StoreBankDetail>
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Bank { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public bool IsPrimary { get; set; }
        public string Note { get; set; }
        public bool Disabled { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreBankDetail, StoreBankDetailDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreBankDetailId))
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name));
        }
    }
}
