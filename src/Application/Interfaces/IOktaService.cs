namespace Engage.Application.Interfaces;

public interface IOktaService
{
    void CreateUser(string login, string firstName, string lastName, string email, string employeeNumber, string mobileNumber, string supplier);

    void UpdateUser(string login, string firstName, string lastName, string mobilePhone, string employeeNumber, string newEmail);
}
