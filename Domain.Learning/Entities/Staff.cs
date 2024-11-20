namespace Domain.Learning.Entities;

public class Staff
{
    public int StaffId { get; set; }
    public int? ApiStaffId { get; set; }
    public int? RegionId { get; set; }
    public int? StoreId { get; set; }
    public int? DepartmentId { get; set; }
    public int? DesignationId { get; set; }
    public int? DivisionId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Race { get; set; }
    public string Disability { get; set; }
    public string Gender { get; set; }
    public string IdentityNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Title { get; set; }
    public string RegionName { get; set; }
    public string StoreName { get; set; }
    public string DepartmentName { get; set; }
    public string DivisionName { get; set; }
    public string DesignationName { get; set; }
    public string Role { get; set; }
    public string StaffNumber { get; set; }
    public string Country_Code { get; set; }
    public string Cellphone { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public bool IsSetaLearner { get; set; }
    public string ExternalCode { get; set; }

    public Region Region { get; set; }
    public Store Store { get; set; }
    public Department Department { get; set; }
    public Designation Designation { get; set; }
    //public Division Division { get; set; }
}
