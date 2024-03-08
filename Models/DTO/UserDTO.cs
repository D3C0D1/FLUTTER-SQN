using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class UserDTO
    {
        //UserId in the system
        public string id { get; set; }

        //User name for access to the system
        public string UserName { get; set; }

        //User's hasshed password 
        public string HashPassword { get; set; }

        //User's status
        public string Status { get; set; }

        //Person asociated to the user 
        public string Person { get; set; }

        //User type for accessing to the system
        public string Role { get; set; }

        //Last access date
        public DateTime LastAccess { get; set; } = DateTime.Now;

        public User ToModel()
        {
            User response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.UserName = this.UserName;
            response.Status = this.Status;
            response.Person = this.Person;
            response.Role = this.Role;
            response.LastAccess = this.LastAccess;
            return response;
        }
    }
}