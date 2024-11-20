namespace Engage.Application.Services.Locations.Models
{
    public class LocationVm : IMapFrom<Location>
    {
        public int Id { get; set; }
        public OptionDto LocationTypeId { get; set; }
        public OptionDto EngageLocationId { get; set; }
        public OptionDto EngageRegionId { get; set; }
        public string BusinessUnit { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public float? Lat { get; set; }
        public float? Long { get; set; }
        public string Note { get; set; }
        public bool IsPrimaryAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Location, LocationVm>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.LocationId))
                .ForMember(d => d.LocationTypeId, opts => opts.MapFrom(s => new OptionDto { Id = s.LocationTypeId, Name = s.LocationType.Name }))
                .ForMember(d => d.EngageLocationId, opts => opts.MapFrom(s => new OptionDto { Id = s.EngageLocationId, Name = s.EngageLocation.Name }))
                .ForMember(d => d.EngageRegionId, opts => opts.MapFrom(s => new OptionDto { Id = s.EngageRegionId, Name = s.EngageRegion.Name }));


        }
    }
}