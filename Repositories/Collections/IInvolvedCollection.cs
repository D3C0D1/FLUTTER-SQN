using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IInvolvedCollection
    {
        Task<List<Involved>> GetAllInvolvedByPlateNumber(string PlateNumber);
        Task<Involved> GetInvolvedById(string id);
        Task<List<Involved>> GetAllInvolvedByReport(string report);
        Task InsertInvolved(Involved formInvolved);
        Task UpdateInvolved(Involved formInvolved);
        Task DeleteInvolved(string id);

    }
}