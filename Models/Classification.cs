using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;
using System.ComponentModel.DataAnnotations;

namespace SQNBack.Models
{
    public class Classification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        //Classification´s name
        [Required]
        public string Name { get; set; }

        //Classification _service's status
        [Required]
        public string Status { get; set; }

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
                return new ApiError("Classification's name can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.Status))
                return new ApiError("Classification' status can't be empty", SQNErrorCode.MissingStatus);
            return new ApiError();
        }

        public ClassificationDTO ToDTO()
        {
            return new ClassificationDTO
            {
                id = this.id.ToString(),
                Name = this.Name,
                Status = this.Status
            };
        }
    }
}