using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;
using System.ComponentModel.DataAnnotations;

namespace SQNBack.Models
{
    public class Area
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        //Area's name
        [Required]
        public string Name { get; set; }

        public List<RangeDTO> Ranges { get; set; }

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
                return new ApiError("Area's name can't be empty", SQNErrorCode.MissingName);
            if (this.Ranges.Count <= 0) 
                return new ApiError("Ranges can't be empty", SQNErrorCode.MissingRanges);
            foreach (RangeDTO r in this.Ranges)
            {
                ApiError validaRange = r.ValidateModel();
                if (validaRange.Code != SQNErrorCode.None)
                    return validaRange;
                List<RangeDTO> count = this.Ranges.FindAll(th => th.Order == r.Order);
                if (count.Count > 1)
                    return new ApiError("Too many Orders are the same for order " + r.Order, 
                        SQNErrorCode.TooManyValues);
            }
            return new ApiError();
        }

        public AreaDTO ToDTO()
        {
            return new AreaDTO
            {
                id = this.id.ToString(),
                Name = this.Name,
                Ranges = this.Ranges
            };
        }
    }
}