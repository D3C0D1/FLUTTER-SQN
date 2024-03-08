using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;
using System.ComponentModel.DataAnnotations;

namespace SQNBack.Models
{
    public class Expert
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        //Expert's document type
        [Required]
        public string DocType { get; set; }

        //Expert's document number
        [Required]
        public string DocNumber { get; set; }

        //Expert's name
        [Required]
        public string Name { get; set; }

        //Expert's Lastname
        [Required]
        public string Lastname { get; set; }

        //Expert's cellular phone number
        [Required]
        public string CellPhone { get; set; }

        //Expert's email
        [Required]
        public string Email { get; set; }

        //Expert's address
        public string Address { get; set; }

        //Expert status
        [Required]
        public string Status { get; set; }

        //Expert's Area where he'll works
        [Required]
        public string Area { get; set; }

        //Expert's Observations about the expert's _service
        public string Observations { get; set; }

        //Expert's proffessional card
        public string ProfCard { get; set; }

        //File with the expert's document
        public string DocFile { get; set; }

        //File with the expert's tributary identification
        public string TributaryFile { get; set; }

        //File with the expert's proffessional card
        public string ProfCardFile { get; set; }

        //File with the expert's diploma
        public string DiplomaFile { get; set; }

        //File with the expert's sign
        public string SignFile { get; set; }

        //Other files from the expert
        public List<string> OtherFiles { get; set; }

        //Actual expert's ubication 
        public UbicationDTO Ubication { get; set; }

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
            if (string.IsNullOrWhiteSpace(this.DocType))
                return new ApiError("Expert's document type can't be empty", SQNErrorCode.MissingDocumentType);
            if (string.IsNullOrWhiteSpace(this.DocNumber))
                return new ApiError("Expert's document number can't be empty", SQNErrorCode.MissingDocumentType);
            if (string.IsNullOrWhiteSpace(this.Name))
                return new ApiError("Expert's name can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.Lastname))
                return new ApiError("Expert's lastname can't be empty", SQNErrorCode.MissingLastname);
            if (string.IsNullOrWhiteSpace(this.CellPhone))
                return new ApiError("Expert's cellular phone number can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.Email))
                return new ApiError("Expert's email can't be empty", SQNErrorCode.MissingEmail);
            if (string.IsNullOrWhiteSpace(this.Status))
                return new ApiError("Expert's status can't be empty", SQNErrorCode.MissingStatus);
            if (string.IsNullOrWhiteSpace(this.Area))
                return new ApiError("Expert's area can't be empty", SQNErrorCode.MissingStatus);
            return new ApiError();
        }

        public ExpertDTO ToDTO()
        {
            return new ExpertDTO
            {
                id = this.id.ToString(),
                DocType = this.DocType,
                DocNumber = this.DocNumber,
                Name = this.Name,
                Lastname = this.Lastname,
                CellPhone = this.CellPhone,
                Email = this.Email,
                Address = this.Address,
                Status = this.Status,
                Area = this.Area,
                Observations = this.Observations,
                ProfCard = this.ProfCard,
                DocFile = this.DocFile,
                TributaryFile = this.TributaryFile,
                ProfCardFile = this.ProfCardFile,
                DiplomaFile = this.DiplomaFile,
                SignFile = this.SignFile,
                OtherFiles = this.OtherFiles,
                Ubication = this.Ubication
            };
        }
    }
}