using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Models
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        //Date when the _service was attendend
        [Required]
        public DateOnly ServiceDate { get; set; }

        //Customer's Id asociated to the _service
        [Required]
        public string CustomerId { get; set; }

        //Expert's Id asociated to the _service
        [Required]
        public string ExpertId { get; set; }

        //Status assigned to the _service
        [Required]
        public string Status { get; set; }

        //Location's _service
        [Required]
        public UbicationDTO Location { get; set; }

        //Calification's _service
        public CalificationDTO Calification { get; set; }

        //Classifications's _service
        //Referenced with Data Element
        public List<DataElementDTO> Classifications { get; set; }

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
            if (string.IsNullOrWhiteSpace(this.CustomerId))
                return new ApiError("Service's customer can't be empty", SQNErrorCode.MissingAssociatedValue);
            if (string.IsNullOrWhiteSpace(this.Status))
                return new ApiError("Status of the service can't be empty", SQNErrorCode.MissingStatus);
            ApiError response = this.Location.ValidateDTO();
            if (response.Code != SQNErrorCode.None)
                return response;
            return new ApiError();
        }

        public ServiceDTO ToDTO()
        {
            return new ServiceDTO
            {
                id = this.id.ToString(),
                ServiceDate = this.ServiceDate,
                CustomerId = this.CustomerId,
                ExpertId = this.ExpertId,
                Status = this.Status,
                Location = this.Location,
                Calification = this.Calification,
                Clasifications = this.Classifications
            };
        }
    }
}