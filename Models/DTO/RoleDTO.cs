using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class RoleDTO
    {
        //Role's Id
        public string id { get; set; }

        //Name for the role
        public string Name { get; set; }

        //Status for the role
        public string Status { get; set; }

        public Role ToModel()
        {
            Role response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.Name = this.Name;
            response.Status = this.Status;
            return response;
        }
    }
}