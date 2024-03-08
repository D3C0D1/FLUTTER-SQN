using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class StatusDTO
    {
        //Status' Id
        public string id { get; set; }

        //Status' name
        public string Name { get; set; }

        public string Code { get; set; }

        //Define if the Statu is a creation status
        public bool Creation { get; set; }

        //Define if the Statu is a valid status
        public bool Valid { get; set; }


        public Status ToModel()
        {
            Status response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.Name = this.Name;
            response.Code = this.Code;
            response.Creation = this.Creation;
            response.Valid = this.Valid;
            return response;
        }
    }
}