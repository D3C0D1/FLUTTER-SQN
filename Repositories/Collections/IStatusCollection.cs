using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IStatusCollection
    {
        Task<Status> GetStatusById(string id);
        Task<Status> GetStatusByName(string name);
        Task<Status> GetStatusByCode(string code);
        Task<List<Status>> GetAllStatus();
        Task<Status> IsForCreation();
        Task<List<Status>> ListByValid(bool valid);
        Task InsertStatus(Status status);
        Task Updatestatus(Status status);
        Task DeleteStatus(string id);
    }
}