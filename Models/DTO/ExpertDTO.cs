using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class ExpertDTO
    {
        //Expert's Id
        public string id { get; set; }

        //Expert's document type
        public string DocType { get; set; }

        //Expert's document number
        public string DocNumber { get; set; }

        //Expert's name
        public string Name { get; set; }

        //Expert's Lastname
        public string Lastname { get; set; }

        //Expert's cellular phone number
        public string CellPhone { get; set; }

        //Expert's email
        public string Email { get; set; }

        //Expert's address
        public string Address { get; set; }

        //Expert status
        public string Status { get; set; }

        //Expert's Area where he'll works
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

        public Expert ToModel()
        {
            Expert response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.DocType = this.DocType;
            response.DocNumber = this.DocNumber;
            response.Name = this.Name;
            response.Lastname = this.Lastname;
            response.CellPhone = this.CellPhone;
            response.Email = this.Email;
            response.Address = this.Address;
            response.Status = this.Status;
            response.Area = this.Area;
            response.Observations = this.Observations;
            response.ProfCard = this.ProfCard;
            response.DocFile = this.DocFile;
            response.TributaryFile = this.TributaryFile;
            response.ProfCardFile = this.ProfCardFile;
            response.DiplomaFile = this.DiplomaFile;
            response.SignFile = this.SignFile;
            response.OtherFiles = this.OtherFiles;
            response.Ubication = this.Ubication;
            return response;
        }
    }
}