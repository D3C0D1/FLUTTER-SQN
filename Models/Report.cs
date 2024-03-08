using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        //Service ID associated to the report<AC>
        public DataElementDTO Service { get; set; }

        //Accident Classification <AC>
        //Referenced with Data Element
        public DataElementDTO Accident { get; set; }

        //Accident's crash with Class <CR>
        //Referenced with Data Element
        public DataElementDTO Crash { get; set; }

        //Fixed object classification <FO>
        //Referenced with Data Element
        public DataElementDTO FixedObject { get; set; }

        //Area where the accident happened <AR>
        //Referenced with Data Element
        public DataElementDTO CrashArea { get; set; }

        //Sector where the accident happened <SC>
        //Referenced with Data Element
        public DataElementDTO CrashSector { get; set; }

        //Zone where the accident happened <ZN>
        //Referenced with Data Element
        public DataElementDTO CrashZone { get; set; }

        //Dessign where the accident happened <DS>
        //Referenced with Data Element
        public DataElementDTO CrashDessign { get; set; }

        //Climate when the accident happened <CL>
        //Referenced with Data Element
        public DataElementDTO CrashClimate { get; set; }

        //Geometric dessign where the accident happened <GM>
        //Referenced with Data Element
        public DataElementDTO CrashGeometric { get; set; }

        //Utilization dessign where the accident happened <UT>
        //Referenced with Data Element
        public DataElementDTO CrashUtilization { get; set; }

        //Sidewalk dessign where the accident happened <SW>
        //Referenced with Data Element
        public DataElementDTO CrashSidewalk { get; set; }

        //Lane dessign where the accident happened <LN>
        //Referenced with Data Element
        public DataElementDTO CrashLane { get; set; }

        //Surface dessign where the accident happened <RS>
        //Referenced with Data Element
        public DataElementDTO CrashSurface { get; set; }

        //Way state where the accident happened <SA>
        //Referenced with Data Element
        public DataElementDTO CrashState { get; set; }

        //Way conditions where the accident happened <CN>
        //Referenced with Data Element
        public DataElementDTO CrashCondition { get; set; }

        //Way Artifical Illumination where the accident happened <AI>
        //Referenced with Data Element
        public DataElementDTO CrashIllumination { get; set; }

        //Traffic Controls where the accident happened <CT>
        //Referenced with Data Element
        public DataElementDTO CrashControls { get; set; }

        //Place's visibility where the accident happened <VS>
        //Referenced with Data Element
        public DataElementDTO CrashVisibility { get; set; }

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
            ApiError response = new ApiError();
            /**
            if (this.Name == string.Empty)
            {
                response.Message = "Role's name can't be empty";
                response.Code = SQNErrorCode.MissingName;
                return response;
            }
            if (this.Status == string.Empty)
            {
                response.Message = "Role status can't be empty";
                response.Code = SQNErrorCode.MissingStatus;
                return response;
            }
            **/
            return response;
        }
         public ReportDTO ToDTO()
        {
            return new ReportDTO
            {
                id = this.id.ToString(),
                Service = this.Service,
                Accident = this.Accident,
                Crash = this.Crash,
                FixedObject = this.FixedObject,
                CrashArea = this.CrashArea,
                CrashSector = this.CrashSector,
                CrashZone = this.CrashZone,
                CrashDessign = this.CrashDessign,
                CrashClimate = this.CrashClimate,
                CrashGeometric = this.CrashGeometric,
                CrashUtilization = this.CrashUtilization,
                CrashSidewalk = this.CrashSidewalk,
                CrashLane = this.CrashLane,
                CrashSurface = this.CrashSurface,
                CrashState = this.CrashState,
                CrashCondition = this.CrashCondition,
                CrashIllumination = this.CrashIllumination,
                CrashControls = this.CrashControls,
                CrashVisibility = this.CrashVisibility
            };
        }

    }
}