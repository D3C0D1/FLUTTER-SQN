using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IExpertCollection
    {
        Task<Expert> GetExpertById(string id);
        Task<Expert> GetExpertById(string docTye, string docNumber);
        Task<Expert> GetExpertByCellPhone(string cellPhone);
        Task<Expert> GetExpertByEmail(string email);
        Task<Expert> GetExpertByProfCard(string profCard);
        Task<List<Expert>> GetAllExperts();
        Task InsertExpert(Expert expert);
        Task UpdateExpert(Expert expert);
        Task DeleteExpert(string id);
    }
}