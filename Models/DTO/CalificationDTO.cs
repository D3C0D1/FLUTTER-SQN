using SQNBack.Utils;

namespace SQNBack.Models.DTO
{
    public class CalificationDTO
    {
        //Value for the longitude ubication
        public int Value { get; set; }

        //Value for the latitude ubication
        public string Observation { get; set; }

        public ApiError ValidateDTO()
        {
            if (Value <= 0 || Value > 5)
                return new ApiError("Calification's value must be between 1 and 5", SQNErrorCode.WrongCalificationValue);
            if (Value < 3 && Observation == string.Empty)
                return new ApiError("Calification's observation must be added", SQNErrorCode.MissingObservation);
            return new ApiError();
        }
    }
}