using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IServicesCollection
    {
        Task<Service> GetServiceById(string id);
        Task<List<Service>> GetServiceByCustomerAndStatus(string customer, string status);
        Task<List<Service>> GetServiceByExpertAndStatus(string expert, string status);
        Task<List<Service>> GetServicesByCustomer(string customer);
        Task<List<Service>> GetServicesByExpert(string expert);
        Task<List<Service>> GetAllServices();
        Task InsertService(Service service);
        Task UpdateService(Service service);
        Task DeleteService(string id);
    }
}