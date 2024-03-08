using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;
using System.ComponentModel.DataAnnotations;

namespace SQNBack.Models
{
    public class Status
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        //Statu's name
        [Required]
        public string Name { get; set; }
        
        //Identificador al usuario del status
        [Required]
        public string Code { get; set; }
        
        //Define if the Statu is a creation status
        public bool Creation { get; set; }

        //Define if the Statu is a valid status
        public bool Valid { get; set; }
                
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
            if (string.IsNullOrWhiteSpace(this.Code))
                return new ApiError("Customer's name can't be empty", SQNErrorCode.MissingName);
            return new ApiError();

        }

        public StatusDTO ToDTO()
        {
            return new StatusDTO
            {
                id = this.id.ToString(),
                Name = this.Name,
                Code = this.Code,
                Creation = this.Creation,
                Valid = this.Valid
            };
        }
    }
}