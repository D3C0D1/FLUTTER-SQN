using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IAreaCollection
    {
        Task<Area> GetAreaById(string id);
        Task<Area> GetAreaByName(string name);
        Task<List<Area>> GetAllAreas();
        Task InsertArea(Area area);
        Task UpdateArea(Area area);
        Task DeleteArea(string id);
    }
}