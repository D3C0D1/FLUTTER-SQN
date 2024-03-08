using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;
using System.ComponentModel.DataAnnotations;

namespace SQNBack.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        //User name for access to the system
        [Required]
        public string UserName { get; set; }

        //User's hasshed password 
        [Required]
        public string HashPassword { get; set; }

        //User's status
        [Required]
        public string Status { get; set; }

        //Person asociated to the user 
        public string Person { get; set; }

        //User type for accessing to the system
        public string Role { get; set; }

        //Last access date
        public DateTime LastAccess { get; set; }

        //Creation date
        public DateTime CreationDate { get; set; }

        //User creator
        public string Creator { get; set; }

        //Modification date
        public DateTime UpdateDate { get; set; }

        //User creator
        public string Updater { get; set; }

        public UserDTO ToDTO()
        {
            return new UserDTO
            {
                id = this.id.ToString(),
                UserName = this.UserName,
                Status = this.Status,
                Person = this.Person,
                Role = this.Role,
                LastAccess = this.LastAccess
            };
        }

        public ApiError ValidateModel()
        {
            if (string.IsNullOrWhiteSpace(this.UserName))
                return new ApiError("User name can't be empty", SQNErrorCode.MissingUsername);
            if (string.IsNullOrWhiteSpace(this.HashPassword))
                return new ApiError("User's password can't be empty", SQNErrorCode.MissingPassword);
            if (string.IsNullOrWhiteSpace(this.Status))
                return new ApiError("User' status can't be empty", SQNErrorCode.MissingStatus);
            return new ApiError();
        }
    }
}