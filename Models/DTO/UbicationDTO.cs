using SQNBack.Utils;

namespace SQNBack.Models.DTO
{
    public class UbicationDTO
    {
        //Value for the longitude ubication
        public decimal Longitude { get; set; }

        //Value for the latitude ubication
        public decimal Latitude { get; set; }

        public ApiError ValidateDTO()
        {
            if (Longitude <= 0)
                return new ApiError("Longitude value can't be empty", SQNErrorCode.MissingLocation);
            if (Latitude <= 0)
                return new ApiError("Latitude value can't be empty", SQNErrorCode.MissingLocation);
            return new ApiError();
        }
    }
}