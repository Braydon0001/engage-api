namespace Engage.Application.Services.EntityContacts.Commands
{
    public class EntityContactCommand
    {
        public int EntityContactTypeId { get; set; }
        public int? UserId { get; set; }
        public string EmailAddress1 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string MobilePhone { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }
        public List<int> CommunicationTypeIds { get; set; }
        public List<int> EngageRegionIds { get; set; }
    }
}