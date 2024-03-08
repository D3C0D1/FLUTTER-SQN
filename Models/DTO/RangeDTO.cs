using SQNBack.Utils;

namespace SQNBack.Models.DTO
{
    public class RangeDTO
    {
        //Area where the service is attendend
        public string Area { get; set; }

        //Value for the area order to excuete the expert seacrh
        public int Order { get; set; }

        //Area's value for the search expressed in meters
        public int Range { get; set; }

        //Search's time for the range expressed in seconds
        public int Time { get; set; }

        public ApiError ValidateModel()
        {
            if (string.IsNullOrWhiteSpace(this.Area))
                return new ApiError("Area in RangeArea can't be empty", SQNErrorCode.AreaError);
            if (this.Order <= 0)
                return new ApiError("Order in RangeArea must be upper 0", SQNErrorCode.ValueMustBeUpper);
            if (this.Range <= 0)
                return new ApiError("Range in RangeArea must be upper 0", SQNErrorCode.ValueMustBeUpper);
            if (this.Time <= 0)
                return new ApiError("Time in RangeArea must be upper 0", SQNErrorCode.ValueMustBeUpper);
            return new ApiError();
        }
    }
}