using Engage.Application.Services.DCAccounts.Models;

namespace Engage.Application.Services.EmployeeWeb.Models;
public class EmployeeWebStoresVm
{
    public List<Store> Stores { get; set; }
}

public class Store
{
    public int StoreId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string AddressLineOne { get; set; }
    public string AddressLineTwo { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostCode { get; set; }
    public string Phone { get; set; }
    public string ImageUrl { get; set; }
    public bool IsHalaal { get; set; }
    public StoreCluster StoreCluster { get; set; }
    public List<DCAccountDto> DCAccounts { get; set; }
    public List<Robot> Robots { get; set; }
    public double StorePerformancePercent { get; set; }
}

public class Robot
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Target { get; set; }
    public int Actual { get; set; }
    public double Score { get; set; }
}
