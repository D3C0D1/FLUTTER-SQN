using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;
using System.ComponentModel.DataAnnotations;

namespace SQNBack.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        //Customer's name
        [Required]
        public string Name { get; set; }

        //Customer's Lastname
        [Required]
        public string Lastname { get; set; }

        //Customer's cellular phone number
        public string CellPhone { get; set; }

        //Customer's email
        public string Email { get; set; }

        //Creation date
        public DateTime CreationDate { get; set; }

        //User creator
        public string Creator { get; set; }

        //Modification date
        public DateTime UpdateDate { get; set; }

        //User creator
        public string Updater { get; set; }

        public ApiError ValidateModel()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                return new ApiError("Customer's name can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.Lastname))
                return new ApiError("Customer's lastname can't be empty", SQNErrorCode.MissingLastname);
            if (string.IsNullOrWhiteSpace(this.CellPhone))
                return new ApiError("Customer's cellular phone number can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.Email))
                return new ApiError("Customer's email can't be empty", SQNErrorCode.MissingEmail);
            return new ApiError();
        }

        public CustomerDTO ToDTO()
        {
            return new CustomerDTO
            {
                id = this.id.ToString(),
                Name = this.Name,
                Lastname = this.Lastname,
                CellPhone = this.CellPhone,
                Email = this.Email
            };
        }
    }
}