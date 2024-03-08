using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface ICustomerCollection
    {
        Task<Customer> GetCustomerById(string id);
        Task<Customer> GetCustomerByCellPhone(string cellPhone);
        Task<Customer> GetCustomerByEmail(string email);
        Task<List<Customer>> GetAllCustomers();
        Task InsertCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(string id);
    }
}