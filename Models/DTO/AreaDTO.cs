using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class AreaDTO
    {
        //Area's Id
        public string id { get; set; }

        //Area's name
        public string Name { get; set; }
        public List<RangeDTO> Ranges { get; set; }

        public Area ToModel()
        {
            Area response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.Name = this.Name;
            response.Ranges = this.Ranges;
            return response;
        }
    }
}