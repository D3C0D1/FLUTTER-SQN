using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IClassificationCollection
    {
        Task<Classification> GetClassificationById(string id);
        Task<Classification> GetClassificationByName(string name);
        Task<List<Classification>> GetAllClassifications();
        Task InsertClassification(Classification classification);
        Task UpdateClassification(Classification classification);
        Task DeleteClassification(string id);
    }
}