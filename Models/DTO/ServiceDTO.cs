using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class ServiceDTO
    {
        //Service's Id
        public string id { get; set; }

        //Date when the _service was attendend
        public DateOnly ServiceDate { get; set; }

        //Customer's Id asociated to the _service
        public string CustomerId { get; set; }

        //Expert's Id asociated to the _service
        public string ExpertId { get; set; }

        //Status assigned to the _service
        public string Status { get; set; }

        //Location's _service
        public UbicationDTO Location { get; set; }

        //Calification's _service
        public CalificationDTO Calification { get; set; }

        //Clasification's _service
        public List<DataElementDTO> Clasifications { get; set; }

        public Service ToModel()
        {
            Service response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.ServiceDate = this.ServiceDate;
            response.CustomerId = this.CustomerId;
            response.ExpertId = this.ExpertId;
            response.Status = this.Status;
            response.Location = this.Location;
            response.Calification = this.Calification;
            response.Classifications = this.Clasifications;
            return response;
        }
    }
}