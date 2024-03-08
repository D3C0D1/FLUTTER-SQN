using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Models
{
    public class Media
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }
        
        [Required]
        //File name
        public string FileName { get; set; }
        //Vehicle ID
        public string Vehicle { get; set; }
        //Report ID
        public string Report { get; set; }
        //File Type
        public string FileType { get; set; }
        //File Storage Path
        public string FilePath { get; set; }
        //File Size (for validation)
        public int FileSize { get; set; }
        //Photo's location
        public UbicationDTO Location { get; set; }

        [Required]
        //File additional comments by Expert
        public string Comments { get; set; }
        //File Details (Time and Date)
        public DateTime UploadDateTime { get; set; }
        //User that upload Media
        public string Creator { get; set; }
        public ApiError ValidateModel()
        {
            if (string.IsNullOrWhiteSpace(this.FileName))
                return new ApiError("This field can't be empty", SQNErrorCode.MissingFileName);
            if (string.IsNullOrWhiteSpace(this.Comments))
                return new ApiError("This field can't be empty", SQNErrorCode.MissingComments);
            return new ApiError();
        }

        public MediaDTO ToDTO()
        {
            return new MediaDTO
            {
                id = this.id.ToString(),
                FileName = this.FileName,
                FileType = this.FileType,
                FilePath = this.FilePath,
                FileSize = this.FileSize,
                Location = this.Location,
                Comments = this.Comments,
                Report = this.Report,
                Vehicle = this.Vehicle,
            };
        }
    }
}
