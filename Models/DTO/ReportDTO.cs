using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class ReportDTO
    {
        public string id { get; set; }

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
        
        public Report ToModel()
        {
            Report response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.Service = this.Service;
            response.Accident = this.Accident;
            response.Crash = this.Crash;
            response.FixedObject = this.FixedObject;
            response.CrashArea = this.CrashArea;
            response.CrashSector = this.CrashSector;
            response.CrashZone = this.CrashZone;
            response.CrashDessign = this.CrashDessign;
            response.CrashClimate = this.CrashClimate;
            response.CrashGeometric = this.CrashGeometric;
            response.CrashUtilization = this.CrashUtilization;
            response.CrashSidewalk = this.CrashSidewalk;
            response.CrashLane = this.CrashLane;
            response.CrashSurface = this.CrashSurface;
            response.CrashState = this.CrashState;
            response.CrashCondition = this.CrashCondition;
            response.CrashIllumination = this.CrashIllumination;
            response.CrashControls = this.CrashControls;
            response.CrashVisibility = this.CrashVisibility;
            return response;
        }
    }
}