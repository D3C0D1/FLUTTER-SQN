using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class CustomerDTO
    {
        // Customer's Id
        public string id { get; set; }

        //Customer's name
        public string Name { get; set; }

        //Customer's Lastname
        public string Lastname { get; set; }

        //Customer's cellular phone number
        public string CellPhone { get; set; }

        //Customer's email
        public string Email { get; set; }

        public Customer ToModel()
        {
            Customer response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.Name = this.Name;
            response.Lastname = this.Lastname;
            response.CellPhone = this.CellPhone;
            response.Email = this.Email;
            return response;
        }
    }
}