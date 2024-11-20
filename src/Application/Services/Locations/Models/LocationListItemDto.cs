namespace Engage.Application.Services.Locations.Models;

public class LocationListItemDto : IMapFrom<Location>
{
    public int Id { get; set; }
    public int LocationTypeId { get; set; }
    public int EngageLocationId { get; set; }
    public int EngageRegionId { get; set; }
    public string BusinessUnit { get; set; }
    public string AddressLineOne { get; set; }
    public string AddressLineTwo { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostCode { get; set; }
    public float? Lat { get; set; }
    public float? Long { get; set; }
    public bool IsPrimaryAddress { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Location, LocationListItemDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.LocationId));
    }
}
