using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class ErrorMessageDTO
    {
        //Mongo's ErrorMessage Id
        public string id { get; set; }

        //Error Message value
        public int Value { get; set; }

        //Error Message code
        public string Code { get; set; }

        //Error Message translations contains the languaje's key and message's text
        public Dictionary<string, string> Message { get; set; }

        public ErrorMessage ToModel()
        {
            ErrorMessage response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.Value = this.Value;
            response.Code = this.Code;
            response.Message = this.Message;
            return response;
        }
    }
}