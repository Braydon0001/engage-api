namespace Engage.Domain.Entities
{
    public class Training : BaseAuditableEntity
    {
        public Training()
        {
            EmployeeTrainings = new HashSet<EmployeeTraining>();
            TrainingFacilitators = new HashSet<TrainingFacilitator>();
        }
        public int TrainingId { get; set; }
        public int? TrainingProviderId { get; set; }
        public int TrainingTypeId { get; set; }
        public int? EngageRegionId { get; set; }
        public int? TrainingCategoryId { get; set; }
        public int? TrainingPeriodId { get; set; }
        public int? TrainingDurationId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsInternalTraining { get; set; }
        public string Site { get; set; }
        public string Note { get; set; }
        public int NoOfParticipants { get; set; }
        public string Duration { get; set; }
        public decimal DirectCost { get; set; }
        public decimal AdditionalCost { get; set; }
        public decimal TotalCost { get; set; }

        public decimal AccommodationCost { get; set; }
        public decimal CarHireCost { get; set; }
        public decimal CateringCost { get; set; }
        public decimal FlightsCost { get; set; }
        public decimal FuelCost { get; set; }
        public decimal StationeryCost { get; set; }
        public decimal VenueCost { get; set; }
        public decimal OtherCost { get; set; }

        public List<JsonFile> Files { get; set; }
        public TrainingProvider TrainingProvider { get; set; }
        public TrainingType TrainingType { get; set; }
        public EngageRegion EngageRegion { get; set; }
        public TrainingCategory TrainingCategory { get; set; }
        public TrainingPeriod TrainingPeriod { get; set; }
        public TrainingDuration TrainingDuration { get; set; }

        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
        public ICollection<TrainingFacilitator> TrainingFacilitators { get; set; }
        public ICollection<TrainingFile> TrainingFiles { get; set; }
    }
}
