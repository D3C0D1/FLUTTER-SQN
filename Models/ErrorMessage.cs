using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;
using System.ComponentModel.DataAnnotations;

namespace SQNBack.Models
{
    public class ErrorMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        //Mongo's ErrorMessage Id
        public ObjectId id { get; set; }

        //Error Message value
        [Required]
        public int Value { get; set; }

        //Error Message code
        public string Code { get; set; }

        //Error Message translations contains the languaje's key and message's text
        public Dictionary<string, string> Message { get; set; }

        public ApiError ValidateModel()
        {
            if (this.Value <= 0)
                return new ApiError("The Value can't be a negative number", SQNErrorCode.ValueMustBeUpper);
            if (string.IsNullOrWhiteSpace(this.Code))
                return new ApiError("The Code can't be empty", SQNErrorCode.MissingCode);
            if (this.Message == null)
                return new ApiError("The Message can't be empty", SQNErrorCode.ErrorMessageNotFound);
            return new ApiError();
        }

        public ErrorMessageDTO ToDTO()
        {
            return new ErrorMessageDTO
            {
                id = this.id.ToString(),
                Value = this.Value,
                Code = this.Code,
                Message = this.Message
            };
        }
    }
}
