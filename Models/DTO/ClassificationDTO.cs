using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class ClassificationDTO
    {
        //Classification's Id
        public string id { get; set; }

        //Classification´s name
        public string Name { get; set; }

        //Classification _service's status
        public string Status { get; set; }

        public Classification ToModel()
        {
            Classification response = new();
            if (string.IsNullOrEmpty(this.id))
                response.id = new ObjectId(this.id);
            response.Name = this.Name;
            response.Status = this.Status;
            return response;
        }
    }
}