using Engage.Application.Services.DCAccounts.Models;

namespace Engage.Application.Services.EmployeeWeb.Models;
public class EmployeeWebStore
{
    public string Name { get; set; }
    public int StoreId { get; set; }
    public string Address { get; set; }
    public string AddressLineOne { get; set; }
    public string AddressLineTwo { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostCode { get; set; }
    public string Phone { get; set; }
    public string ImageUrl { get; set; }
    public StoreCluster StoreCluster { get; set; }
    public List<DCAccountDto> DCAccounts { get; set; }
    public bool IsHalaal { get; set; }
    public List<float> Gps { get; set; }
    public List<RobotWithAttributes> Robots { get; set; }
    public List<StoreContact> Contacts { get; set; }
}

public class RobotWithAttributes : Robot
{
    public List<Attribute> Attributes { get; set; }
}

public class Attribute
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Detail { get; set; }
}

public class StoreContact
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
