using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;
using System.ComponentModel.DataAnnotations;

namespace SQNBack.Models
{
    public class DataElement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        //Data element's Name 
        [Required]
        public string Name { get; set; }

        //Data element's Status
        [Required]
        public string Status { get; set; }

        //Data element's type  
        //AC = Accident          //AR = Area             //AI = Atificial Ilumination     //CR = Crash
        //CL = Climate           //CN = Conditions       //CT = Controls                  //DS = Dessign
        //DT = Document Type     //FO = Fixed Object     //GM = Geometric                 //GR = Classifications
        //LN = Lanes             //RS = Running surface  //SC = Sector                    //SA = State
        //SW = Sidewalk          //ST = Service Type     //TM = Transport mode            //UT = Utilization
        //VC = Vehicle class     //VS = Visibility       //ZN = Zone
        [Required]
        public string Group { get; set; }

        //Data element's icon
        public string Icon { get; set; }

        //Creation date
        public DateTime CreationDate { get; set; }

        //User creator
        public string Creator { get; set; }

        //Modification date
        public DateTime UpdateDate { get; set; }

        //User creator
        public string Updater { get; set; }

        public DataElementDTO ToDTO()
        {
            return new DataElementDTO
            {
                Id = Id.ToString(),
                Name = Name,
                Status = Status,
                Group = Group,
                Icon = Icon
            };
        }

        public ApiError ValidateModel()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return new ApiError("Data Element name can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrEmpty(Status))
                return new ApiError("Data Element status can't be empty", SQNErrorCode.MissingStatus);
            if (string.IsNullOrEmpty(Group))
                return new ApiError("Data Element group can't be empty", SQNErrorCode.MissingGroup);
            return new ApiError();
        }
    }
}
