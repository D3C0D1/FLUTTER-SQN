using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IMediaCollection
    {
        Task<Media> GetMediaById(string id);
        Task<List<Media>> GetMediaByReport(string report);
        Task<List<Media>> GetMediaByVehicle(string vehicle);
        Task InsertMedia(List<Media> media);
        Task DeleteMedia(string id);
        Task InsertOne(Media mediaup);
        Task<List<Media>> GetMediaByReport();
    }
}